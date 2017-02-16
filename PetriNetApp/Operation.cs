using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetApp
{
    public class Operation
    {
        public int Time { get; set; }
        public int Number { get; set; }
        public int MachineNumber { get; set; }
        public int ProcessNumber { get; set; }

        public Operation(int time, int number, int machineNumber, int processNumber)
        {
            Time = time;
            MachineNumber = machineNumber;
            Number = number;
            ProcessNumber = processNumber;
        }
    }

    public class Buffer
    {
        public int Number { get; set; }

        public int Capacity { get; set; }

        public List<ActiveProcess> ActiveProcesses { get; set; }

        public List<ProcessConnected> Connected { get; set; } 

        /// <summary>
        /// adres w tabeli markowań
        /// </summary>
        public int? Address { get; set; }

        public Buffer() {
            ActiveProcesses = new List<ActiveProcess>();
            Connected = new List<ProcessConnected>();
        }

        public Buffer(int number, int capacity)
        {
            Number = number;
            Capacity = capacity;
            ActiveProcesses = new List<ActiveProcess>();
            Connected = new List<ProcessConnected>();
        }


        
        public void clearConnected()
        {
            Connected.Clear();
        }

        public void Connect(Process p, int fromOperationNumber)
        {
            Connected.Add(new ProcessConnected(p, fromOperationNumber));
        }
        public void takePlace(Process p, int operationNumber)
        {
            if (this.ActiveProcesses.Any(i => i.process == p))
            {
                var active = ActiveProcesses.FirstOrDefault(i => i.process == p);
                active.Count++;
                active.ActiveOperations.Add(operationNumber);
            }
            else
                ActiveProcesses.Add(new ActiveProcess(p, operationNumber));
        }

        public void emptyPlace(Process p, int operationNumber)
        {
            var active = this.ActiveProcesses.FirstOrDefault(i => i.process == p);
            if (active?.Count > 1)
            {
                active.Count--;
                active.ActiveOperations.Remove(operationNumber);
            }
            else
            {
                if(active != null)
                    ActiveProcesses.Remove(active);
            }
                
        }
    }

    public class Input
    {
        public int Number { get; set; }

        public int Value { get; set; }

        public int? CorrespondingOutput { get; set; }

        /// <summary>
        /// adres w tabeli markowań. Uzupełniane przy ładowaniu markowania
        /// </summary>
        public int? Address { get; set; }

        public Input() { }

        public Input(int number, int value)
        {
            Number = number;
            Value = value;
        }

    }
}
