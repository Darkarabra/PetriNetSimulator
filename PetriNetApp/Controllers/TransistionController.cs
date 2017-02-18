using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetApp
{
    public enum sortAlgorithm : int
    {
        FIFO = 1,
        MCF = 2,
        STF = 3
    }

    public class TransistionController
    {

        public int firstMachineAddress { get; set; }
        public int Machines { get; set; }
        public PetriNet petriNet { get; set; }
        public List<Buffer> Buffers { get; set; }
        private List<Process> Processes { get; set; }
        public List<Transition> PreparedTransitions { get; set; }
        public List<sortAlgorithm> sortList { get; set; }

        public TransistionController(PetriNet pn, List<Buffer> b,
            List<Process> p, int machines, int firstMachine,
            List<sortAlgorithm> list)
        {
            this.Buffers = b;
            this.petriNet = pn;
            this.Processes = p;
            this.Machines = machines;
            this.firstMachineAddress = firstMachine;
            PreparedTransitions = new List<Transition>();
            sortList = list;
        }

        #region IO
        public int transitionToMachine(Vector<double> T)
        {
            for (int i = 0; i < T.Count; i++)
            {
                for (int m = firstMachineAddress; m < firstMachineAddress + Machines; m++)
                {
                    if (T[i] > 0 && i == m)
                        return m - firstMachineAddress + 1;
                }
            }
            return 0;
        }

        public int transitionFromMachine(Vector<double> T)
        {
            for (int i = 0; i < T.Count; i++)
            {
                for (int m = firstMachineAddress; m < firstMachineAddress + Machines; m++)
                {
                    if (T[i] < 0 && i == m)
                        return m - firstMachineAddress + 1;
                }
            }
            return 0;
        }

        public bool machineInCapacityLimits(int machineNumber, Matrix<double> M)
        {
            if (M[firstMachineAddress + machineNumber - 1, 0] + 1 > 1)
                return false;
            return true;
        }

        public int transitionToBufferAddress(Vector<double> T)
        {
            int bufferAddress = firstMachineAddress + Machines;
            for (int i = 0; i < T.Count; i++)
            {
                for (int m = bufferAddress; m < bufferAddress + Machines; m++)
                {
                    if (T[i] > 0 && i == m)
                        return m;
                }
            }
            return 0;
        }

        public int transitionToBuffer(Vector<double> T)
        {
            int bufferAddress = firstMachineAddress + Machines;
            for (int i = 0; i < T.Count; i++)
            {
                for (int m = bufferAddress; m < bufferAddress + Machines; m++)
                {
                    if (T[i] > 0 && i == m)
                        return m - bufferAddress + 1;
                }
            }
            return 0;
        }

        public int transitionFromBuffer(Vector<double> T)
        {
            int bufferAddress = firstMachineAddress + Machines;
            for (int i = 0; i < T.Count; i++)
            {
                for (int m = bufferAddress; m < bufferAddress + Machines; m++)
                {
                    if (T[i] < 0 && i == m)
                        return m - bufferAddress + 1;
                }
            }
            return 0;
        }

        public bool bufferCapacityInLimits(int bufferAddress, Matrix<double> M)
        {
            var buffer = Buffers.First(i => i.Address == bufferAddress);
            if (M[bufferAddress, 0] + 1 > buffer.Capacity)
                return false;
            return true;

        }
        #endregion

        private Transition generateTransitionInfo(int t, int time)
        {
            var tr = petriNet.I.Column(t);
            int operationTime = 1;
            int fromMachine = transitionFromMachine(tr);
            int toMachine = transitionToMachine(tr);
            int toBuffer = transitionToBuffer(tr);
            int fromBuffer = transitionFromBuffer(tr);

            bool machineConnected = toMachine > 0 || fromMachine > 0;
            if (fromMachine > 0)
            {
                var process = getProcess(t);

                var operation = getOperation(process, t);
                operationTime = process.getOperationTime(operation.Number);
            }
            var tt = new Transition(t + 1, tr, time, operationTime, machineConnected);

            return tt;
        }

        private bool transitionPrepared(Vector<double> T, TimerController timer, Matrix<double> M)
        {
            int inputs = T.Where(i => i < 0).Count();
            int preparedInputs = 0;
            for (int i = 0; i < petriNet.Places; i++)
            {
                if (T[i] < 0 && M[i, 0] > 0)
                {
                    preparedInputs++;
                }
            }
            int input = transitionFromMachine(T);
            int output = transitionToMachine(T);
            int buffer = transitionToBufferAddress(T);
            if (inputs == preparedInputs)
            {
                if ((input > 0 && !timer.machineIsActive(input)) ||
                    (output > 0 && !timer.machineIsActive(output) && machineInCapacityLimits(output, M)))
                    return true;
                else if (input <= 0 && output <= 0 && buffer <= 0)
                    return true;
                else if (buffer > 0 && bufferCapacityInLimits(buffer, M))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }


        public Vector<double> chooseTransiztion(TimerController timer, Matrix<double> M)
        {
            var T = Vector<double>.Build.Dense(petriNet.Tranzitions, 0);
            if (PreparedTransitions.Count > 0)
            {
                //int buffersCanBeZero = 0;
                //while (buffersCanBeZero < 2)
                //{

                for (int i = 0; i < PreparedTransitions.Count; i++)
                {
                    T = Vector<double>.Build.Dense(petriNet.Tranzitions, 0);
                    var Tr = PreparedTransitions.ElementAt(i);
                    T[Tr.Number - 1] = 1;
                    var MM = M + petriNet.I * T.ToColumnMatrix();
                    if (transitionIsSafe(Tr.IO, Tr.Number, M, MM, Tr))
                    {
                        PreparedTransitions.RemoveAt(i);
                        //Console.Out.WriteLine("Transition: " + Tr.Number);
                        var machine = transitionFromMachine(Tr.IO);

                        if (machine > 0)
                        {

                            int processNumber = getProcessNumber(Tr.Number - 1);
                            var operation = Processes.First(p => p.Number == processNumber)
                                   .Operations.First(o => o.MachineNumber == machine);
                            if (processNumber > 0)
                            {
                                timer.StartOperation(operation, processNumber);
                            }
                        }
                        var tmpBuffers = Buffers;
                        updateBuffers(Tr);
                        return T;
                    }
                }
                //buffersCanBeZero++;
                //}
            }
            T = Vector<double>.Build.Dense(petriNet.Tranzitions, 0);
            return T;
        }

        public bool transitionIsSafe(Vector<double> T, int number, Matrix<double> M, Matrix<double> MM, Transition Tr)
        {

            int fromBufferNumber = transitionFromBuffer(T);
            int toBufferNumber = transitionToBuffer(T);
            updateBuffers(Tr);
            if (fromBufferNumber > 0)
            {
                int processNumber = getProcessNumber(number - 1);
                var process = getProcess(number - 1);
                var operation = getOperation(process, number - 1);
                var currentBuffer = Buffers.First(i => i.Number == fromBufferNumber);
                var prevBuffer = getOperation(process, number - 2).MachineNumber;
                //last operation in process
                if (operation.Number == process.Operations.Count)
                {
                    revertBuffers(Tr);
                    return true;
                }

                if (bufferNotFull(currentBuffer, MM))
                {
                    revertBuffers(Tr);
                    return true;
                }

                List<ProcessConnected> ProcessesToCheck = new List<ProcessConnected>();
                List<int> CheckedBuffers = new List<int>();
                CheckedBuffers.Add(currentBuffer.Number);
                //CheckedBuffers.Add(prevBuffer);
                ProcessesToCheck.Add(new ProcessConnected(process, operation.Number));
                foreach (var ap in currentBuffer.ActiveProcesses)
                {
                    foreach (var ao in ap.ActiveOperations)
                    {
                        var newcheck = new ProcessConnected(ap.process, ao);
                        if (!ProcessesToCheck.Any(c => c.fromOperationNumber == ao && c.process == ap.process))
                            ProcessesToCheck.Add(newcheck);
                    }
                }
                
                
                for (int i = 0; i < ProcessesToCheck.Count; i++)
                {
                    var proc = ProcessesToCheck.ElementAt(i);

                    var tmpCurrentBuff = Buffers.First(l => l.Number == proc.process.Operations
                        .First(o => o.Number == proc.fromOperationNumber).MachineNumber);

                    //CheckedBuffers.Add(tmpCurrentBuff.Number);

                    //if (proc.process.Operations.Where(j => j.Number > proc.fromOperationNumber).ToList().Count == 0)
                    //{
                    //    revertBuffers(Tr);
                    //    return true;
                    //}


                    foreach (var op in proc.process.Operations.Where(j => j.Number > proc.fromOperationNumber).OrderBy(j => j.Number))
                    {
                        var buffer = Buffers.First(l => l.Number == op.MachineNumber);

                        //if (op.Number == proc.process.Operations.Count())
                        //{
                        //    revertBuffers(Tr);
                        //    return true;
                        //}

                        //if(CheckedBuffers.Any(b => b == buffer.Number) && ProcessesToCheck.Count == i+1)
                        //{
                        //    revertBuffers(Tr);
                        //    return false;
                        //}

                        if (CheckedBuffers.Any(b => b == buffer.Number))
                        {
                            break;
                        }
                            

                        if (bufferNotFull(buffer, MM))
                        {
                            revertBuffers(Tr);
                            return true;
                        }


                        CheckedBuffers.Add(buffer.Number);

                        foreach (var ap in buffer.ActiveProcesses)
                        {
                            foreach (var ao in ap.ActiveOperations)
                            {
                                var newcheck = new ProcessConnected(ap.process, ao);
                                if (!ProcessesToCheck.Any(c => c.fromOperationNumber == ao && c.process == ap.process))
                                    ProcessesToCheck.Add(newcheck);
                            }
                        }

                        //ProcessesToCheck.AddRange(buffer.Connected);
                    }
                }
                revertBuffers(Tr);
                return false;
            }
            revertBuffers(Tr);
            return true;
        }

        private void SortTransitions()
        {
            if (sortList.Count == 3)
                PreparedTransitions = PreparedTransitions
                    .OrderBy(i => i.ProcessTime)
                    .ThenBy(i => i.arrivalTime)
                    .ThenBy(i => i.MachineConnected)
                    .ToList();

            else if (sortList.Any(i => i == sortAlgorithm.MCF))
                PreparedTransitions = PreparedTransitions.OrderByDescending(i => i.MachineConnected == true).ToList();

            else if (sortList.Any(i => i == sortAlgorithm.STF))
                PreparedTransitions = PreparedTransitions
                    .OrderBy(i => i.ProcessTime).ToList();
            else if (sortList.Any(i => i == sortAlgorithm.FIFO))
                PreparedTransitions = PreparedTransitions.
                    OrderBy(i => i.arrivalTime).ToList();
        }

        #region Get
        private int getProcessNumber(int transitionNumber)
        {
            return getProcess(transitionNumber).Number;
        }

        private Process getProcess(int transitionNumber)
        {
            var transitions = 0;
            foreach (var process in Processes)
            {
                transitions += process.Operations.Count * 3 + 1;
                if (transitionNumber < transitions)
                    return process;
            }
            return null;
        }

        private Operation getOperation(Process process, int transitionNumber)
        {
            var transitions = 0;
            if (process.Number > 1)
            {
                foreach (var p in Processes.Where(i => i.Number < process.Number))
                {
                    transitions += p.Operations.Count * 3 + 1;
                }
            }
            foreach (var o in process.Operations)
            {
                transitions += 3;
                if (transitionNumber < transitions)
                {
                    return o;
                }
            }
            return null;
        }


        public List<Transition> getPreparedTransitions(int time,
            TimerController timer, Matrix<double> M)
        {
            List<Transition> CurrentTransitions = new List<Transition>();
            List<Transition> tmpPrepared = new List<Transition>();
            PreparedTransitions.ForEach(i => tmpPrepared.Add(i));
            for (int t = 0; t < petriNet.Tranzitions; t++)
            {
                if (transitionPrepared(petriNet.I.Column(t), timer, M))
                {
                    var newTransition = generateTransitionInfo(t, time);

                    CurrentTransitions.Add(newTransition);
                }

            }
            foreach (var transition in PreparedTransitions)
            {
                if (CurrentTransitions.All(i => i.Number != transition.Number))
                    tmpPrepared.Remove(transition);

            }
            foreach (var transition in CurrentTransitions)
            {
                if (PreparedTransitions.All(i => i.Number != transition.Number)
                    || PreparedTransitions.Count == 0)
                    tmpPrepared.Add(transition);
            }
            PreparedTransitions = tmpPrepared;

            SortTransitions();

            return PreparedTransitions;
        }

        public int getMachineAddress(int number)
        {
            return firstMachineAddress + number - 1;
        }
        #endregion

        public bool bufferNotFull(Buffer buffer, Matrix<double> MM)
        {
            if (MM[buffer.Address.GetValueOrDefault(), 0] >= 1 /*|| MM[getMachineAddress(buffer.Number), 0] > 0*/)
                return true;
            return false;


        }


        public void updateBuffers(Transition Tr)
        {

            var fromBuffer = transitionFromBuffer(Tr.IO);
            if (fromBuffer > 0)
            {
                Process process = getProcess(Tr.Number - 1);
                var operation = Processes.First(p => p.Number == process.Number)
                       .Operations.First(o => o.MachineNumber == fromBuffer);
                Buffers.FirstOrDefault(b => b.Number == fromBuffer).takePlace(process, operation.Number);
            }
            var toBuffer = transitionToBuffer(Tr.IO);
            if (toBuffer > 0)
            {
                Process process = getProcess(Tr.Number - 1);
                var operation = Processes.First(p => p.Number == process.Number)
                       .Operations.First(o => o.MachineNumber == toBuffer);
                Buffers.FirstOrDefault(b => b.Number == toBuffer).emptyPlace(process, operation.Number);
            }
        }

        public void revertBuffers(Transition Tr)
        {

            var fromBuffer = transitionFromBuffer(Tr.IO);
            if (fromBuffer > 0)
            {
                Process process = getProcess(Tr.Number - 1);
                var operation = Processes.First(p => p.Number == process.Number)
                       .Operations.First(o => o.MachineNumber == fromBuffer);
                Buffers.FirstOrDefault(b => b.Number == fromBuffer).emptyPlace(process, operation.Number);
            }
            var toBuffer = transitionToBuffer(Tr.IO);
            if (toBuffer > 0)
            {
                Process process = getProcess(Tr.Number - 1);
                var operation = Processes.First(p => p.Number == process.Number)
                       .Operations.First(o => o.MachineNumber == toBuffer);
                Buffers.FirstOrDefault(b => b.Number == toBuffer).takePlace(process, operation.Number);
            }
        }

    }
}
