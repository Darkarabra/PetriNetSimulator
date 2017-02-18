using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetApp
{
    public partial class Controller
    {


        #region Data
        public void sampleData()
        {
            clearData();
            Machines = 3;
            ProcessesAmount = 2;
            for (int p = 0; p < ProcessesAmount; p++)
            {
                var process = new Process(p + 1);
                if (p == 0)
                {
                    process.Operations.Add(new Operation(10, 1, 1, p + 1));
                    process.Operations.Add(new Operation(20, 2, 2, p + 1));
                    process.Operations.Add(new Operation(30, 3, 3, p + 1));
                }
                if (p == 1)
                {
                    process.Operations.Add(new Operation(5, 1, 3, p + 1));
                    process.Operations.Add(new Operation(15, 2, 2, p + 1));
                    process.Operations.Add(new Operation(25, 3, 1, p + 1));
                }
                Processes.Add(process);

                Inputs.Add(new Input(p + 1, p * 2 + 2));
            }
            for (int m = 0; m < Machines; m++)
            {
                Buffers.Add(new Buffer(m + 1, 2));
            }
        }


        private void clearData()
        {
            Machines = 0;
            ProcessesAmount = 0;
            Processes.Clear();
            Buffers.Clear();
            Inputs.Clear();
            PreparedTransitions.Clear();

        }
        public void submitDefaultData(int machines, int processes, bool clear)
        {
            int p = Processes?.Count() ?? 0;
            if (clear)
            {
                clearData();
                processes = 0;
            }
                
            Machines = machines;
            ProcessesAmount = processes;
            
            while (p < processes)
            {
                var process = new Process(p + 1);
                Processes.Add(process);
                p++;
            }
        }

        #endregion

        #region Parameters
        public bool addMachineToProcess(int processId, int machineId, int time)
        {
            if (processId > ProcessesAmount || machineId > Machines)
                return false;
            var sequence = Processes.First(i => i.Number == processId);
            sequence.Operations
                .Add(new Operation(time, sequence.Operations.Count + 1, machineId, processId));
            return true;

        }

        public bool defineBufferCapacity(int buffer, int capacity)
        {
            if (buffer > Machines)
                return false;
            else
            {
                if (Buffers.Any(i => i.Number == buffer))
                    Buffers.First(i => i.Number == buffer).Capacity = capacity;
                else
                    Buffers.Add(new Buffer(buffer, capacity));
                return true;
            }
        }

        public bool defineInputValue(int input, int value)
        {
            if (input > ProcessesAmount)
                return false;
            else
            {
                if (Inputs.Any(i => i.Number == input))
                    Inputs.First(i => i.Number == input).Value = value;
                else
                    Inputs.Add(new Input(input, value));
                return true;
            }
        }

        public void clearProcessesSequence()
        {
            foreach (var seq in Processes)
            {
                seq.Operations.Clear();
            }
        }

        public bool hasErrors(out string error)
        {
            error = string.Empty;
            if (Processes.Count < this.ProcessesAmount || Processes.Any(i => i.Operations.Count == 0))
                error += "\nDefine operation sequences for all processes.";
            if (Buffers.Count < this.Machines)
                error += "\nDefine buffers.";
            if (Inputs.Count < this.ProcessesAmount)
                error += "\nDefine inputs.";

            if (string.IsNullOrEmpty(error))
                return false;
            else
            {
                error = "Input data error." + error;
                return true;
            }
        }
        #endregion

        private bool inputsHasValues()
        {
            foreach (var input in Inputs)
            {
                if (M[input.Address.GetValueOrDefault(), 0] > 0)
                {
                    return true;
                }
            }
            return false;
        }

        private int getCorrespondingOutput(int inputNumber)
        {
            int output = 0;
            foreach (var p in Processes.Where(i => i.Number < inputNumber))
            {
                output += p.Operations.Count * 3 + 2;
            }
            var process = Processes.First(i => i.Number == inputNumber);
            output += process.Operations.Count * 3 + 1;
            return output;
        }
    }
}
