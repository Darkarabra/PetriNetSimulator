using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetApp
{
    public partial class TransistionController
    {


        public bool bufferNotFull(Buffer buffer, Matrix<double> MM)
        {
            if (MM[buffer.Address.GetValueOrDefault(), 0] >= 1)
                return true;
            return false;
        }

        public bool bufferFull(Buffer buffer, Matrix<double> MM)
        {
            if (MM[buffer.Address.GetValueOrDefault(), 0] == 0)
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
    }
}
