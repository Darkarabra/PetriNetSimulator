using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetApp
{
    public class TimerController
    {

        public List<Timer> Timers;

        public int time;

        public TimerController()
        {
            Timers = new List<Timer>();
            time = 0;
        }
        public TimerController(int machines)
        {
            Timers = new List<Timer>();
            time = 0;
            for (int m = 0; m < machines; m++)
            {
                Timers.Add(new Timer(m+1));
            }
        }

        public void StartOperation(Operation operation, int processNumber)
        {
            //Console.Out.WriteLine("start machine: " + operation.MachineNumber);
            Timers.First(i => i.MachineNumber == operation.MachineNumber).SetTimer(operation.Time, processNumber);
        }

        public bool AnyOperationActive()
        {
            return Timers.Any(t => t.Time > 0);
        }

        public void countTime()
        {
            foreach(var timer in Timers.Where(i => i.Time > 0))
            {
                timer.countTime();
            }
            time++;
        }
        public int countDownClosestTimer()
        {
            int minTime = 0;
            if(Timers.Where(i => i.Time > 0).Count() > 0)
            {
                minTime = Timers.Where(i => i.Time > 0).Min(i => i.Time);
                foreach (var t in Timers.Where(i => i.Time > 0))
                {
                    t.countTime(minTime);
                }
            }
            time += minTime;
            return minTime;
        }
        
        public bool machineIsActive(int machineNumber)
        {
            return Timers.Where(i => i.Time > 0).Any(i => i.MachineNumber == machineNumber);
        }
    }
}
