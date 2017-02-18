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

    public partial class TransistionController
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

                    foreach (var op in proc.process.Operations.Where(j => j.Number > proc.fromOperationNumber).OrderBy(j => j.Number))
                    {
                        var buffer = Buffers.First(l => l.Number == op.MachineNumber);

                        if (bufferFull(buffer, MM) && !buffer.ActiveProcesses.Any(a => a.process.Number == proc.process.Number))
                        {
                            if (ProcessesToCheck.Count == i + 1)
                            {
                                revertBuffers(Tr);
                                return false;
                            }
                            break;
                        }

                        if (CheckedBuffers.Any(b => b == buffer.Number) && ProcessesToCheck.Count == i + 1)
                        {
                            revertBuffers(Tr);
                            return false;
                        }

                        if (CheckedBuffers.Any(b => b == buffer.Number))
                        {
                            break;
                            //revertBuffers(Tr);
                            //return false;
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
    }
}
