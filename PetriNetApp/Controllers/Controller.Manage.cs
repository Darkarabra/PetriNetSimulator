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

        #region print data

        public string printSequence()
        {
            string result = string.Empty;
            foreach (var list in Processes)
            {
                result += "P" + list.Number + ": ";
                result += string.Join(", ", list.Operations.Select(i => i.MachineNumber + " (" + i.Time + ")"));
                result += "; \n";
            }

            return result;
        }

        public string printMachinesPercentage()
        {
            string result = "Machines percentage usage: ";
            int sum = 0;
            foreach (var p in Processes)
            {
                var tmpsum = p.Operations.Sum(i => i.Time);
                sum += Inputs.First(i => i.Number == p.Number).Value * tmpsum;
            }
            foreach (var m in Buffers)
            {
                int timeM = 0;
                foreach (var p in Processes)
                {
                    var tmp = 0;
                    foreach (var o in p.Operations.Where(i => i.MachineNumber == m.Number))
                    {
                        tmp += o.Time;
                    }

                    timeM += Inputs.First(i => i.Number == p.Number).Value * tmp;
                }
                decimal calculated = decimal.Round((decimal)timeM * (decimal)100 / (decimal)sum);
                result += " M" + m.Number + ": " + calculated + "% |";

            }
            return result;

        }

        public string printCurrentBufferProcesses(int number)
        {
            var buffer = Buffers.First(i => i.Number == number);
            string result = "B" + number + ": ";
            if (buffer.ActiveProcesses.Count == 0)
                result += "0";
            else
            {
                foreach(var p in buffer.ActiveProcesses)
                {
                    result += "P" + p.process.Number + ": " + p.Count + " ";
                }
            }
            return result;
        }

        public string printBuffers()
        {
            string result = string.Empty;
            foreach (var b in Buffers)
            {
                result += "B" + b.Number + ": ";
                result += string.Join(", ", b.Capacity);
                result += "; ";
            }
            return result;
        }

        public string printInputs()
        {
            string result = string.Empty;
            foreach (var i in Inputs)
            {
                result += "In" + i.Number + ": ";
                result += string.Join(", ", i.Value);
                result += "; ";
            }
            return result;
        }

        public string printPreparedTranzitions()
        {
            string result = string.Empty;
            result = string.Join(", ", PreparedTransitions.Select(i => i.Number));
            return result;
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

        public int BuffersCount()
        {
            return Buffers.Count();
        }

        public int getBufferCapacity(int buffer)
        {
            return Buffers.First(i => i.Number == buffer).Capacity;
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
