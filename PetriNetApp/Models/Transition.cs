using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetApp
{
    public class Transition
    {
        public int Number { get; set; }

        public Vector<double> IO { get; set; }

        public int arrivalTime { get; set; }

        public int ProcessTime { get; set; }

        public bool MachineConnected { get; set; }

        public Transition() { }

        public Transition(int number, Vector<double> T, int time, int processTime, bool machineConnected)
        {
            Number = number;
            IO = T;
            arrivalTime = time;
            ProcessTime = processTime;
            MachineConnected = machineConnected;
        }

        public bool isConflicted(Transition T)
        {
            for(int i = 0; i < IO.Count; i ++)
            {
                if (IO[i] < 0 && IO[i] == T.IO[i])
                    return true;
            }
            return false;
        }
    }
}
