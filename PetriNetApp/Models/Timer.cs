using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetApp
{
    public class Timer
    {
        public int MachineNumber { get; set; }

        public int ProcessNumber { get; set; }

        /// <summary>
        /// odlicza czas w dół
        /// </summary>
        public int Time { get; set; }

        public Timer(int machineNumber)
        {
            Time = 0;
            MachineNumber = machineNumber;
            ProcessNumber = 0;
        }

        public void SetTimer(int time, int processNumber)
        {
            Time = time;
            ProcessNumber = processNumber;
        }
        public void countTime()
        {
            if(Time>0)
                Time--;
            if(Time == 0)
                ProcessNumber = 0;
        }

        public void countTime(int time)
        {
            if (Time > 0)
                Time -= time;
            if (Time == 0)
                ProcessNumber = 0;
        }

        public bool isActive()
        {
            if (Time > 0)
                return true;
            return false;
        }

    }
}
