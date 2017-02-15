using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetApp
{
    public class Process
    {
        public int Number { get; set; }


        public List<Operation> Operations { get; set; }

        public Process()
        {
            Operations = new List<Operation>();
        }

        public Process(int number)
        {
            Operations = new List<Operation>();
            Number = number;
        }

        public int getOperationsTime()
        {
            return Operations.Sum(i => i.Time);
        }

        public int getOperationTime(int number)
        {
            return Operations.First(i => i.Number == number).Time;
        }
    }

    public class ActiveProcess
    {
        public Process process { get; set; }

        public int Count { get; set; }

        public List<int> ActiveOperations { get; set; }

        public ActiveProcess(PetriNetApp.Process p, int operationNumber)
        {
            ActiveOperations = new List<int>();
            ActiveOperations.Add(operationNumber);
            process = p;
            Count = 1;
        }

        public int getBufferNumber(int operationNumber)
        {
            return process.Operations.First(i => i.Number == operationNumber).MachineNumber;
        }
    }

    public class ProcessCheck
    {
        public Process process { get; set; }

        public int fromOperationNumber { get; set; }

        public ProcessCheck(Process p, int number)
        {
            process = p;
            fromOperationNumber = number;
        }
    }
}
