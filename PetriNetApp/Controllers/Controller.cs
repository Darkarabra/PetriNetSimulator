﻿using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetApp
{
    public partial class Controller : BreakEventListener
    {
        public Form1 chartForm { get; set; }

        PetriNet petriNet { get; set; }
        /// <summary>
        /// wektor markowania
        /// </summary>
        Matrix<double> M { get; set; }

        /// <summary>
        /// wektor tranzycji
        /// </summary>
        Vector<double> T { get; set; }

        public int Machines { get; set; }
        public int ProcessesAmount { get; set; }
        List<Process> Processes { get; set; }
        List<Buffer> Buffers { get; set; }
        List<Input> Inputs { get; set; }
        DataTable dt { get; set; }

        public TimerController timer { get; set; }

        List<Transition> PreparedTransitions { get; set; }
        int firstMachineAddress { get; set; }
        TransistionController transitionController { get; set; }

        public DataController data { get; set; }
        int t;
        public IList tab = null;
        string machinesPercentage;

        public bool stopped { get; private set; }

        public Controller()
        {
            Processes = new List<Process>();
            Inputs = new List<Input>();
            Buffers = new List<Buffer>();
            PreparedTransitions = new List<Transition>();
            dt = new DataTable();
            sampleData();
            data = new DataController();
            tab = (data.table as IListSource).GetList();
            stopped = false;
        }


        #region Build
        void buildPetriNet()
        {
            petriNet = new PetriNet(Processes, Buffers);
            firstMachineAddress = petriNet.getFirstMachineId();
        }

        void buildMark()
        {
            M = Matrix<double>.Build.Dense(petriNet.Places, 1);
            Buffers.ForEach(b => b.clearConnected());
            //machines and buffers
            for (int m = 0; m < Machines; m++)
            {
                M[firstMachineAddress + m, 0] = 1;
                M[firstMachineAddress + Machines + m, 0] = Buffers.First(i => i.Number == m + 1).Capacity;
                Buffers.First(b => b.Number == m + 1).Address = firstMachineAddress + Machines + m;
            }
            //inputs
            int place = 0;
            foreach (var process in Processes)
            {
                var output = getCorrespondingOutput(process.Number);
                M[place, 0] = Inputs.First(i => i.Number == process.Number).Value;
                Inputs.First(i => i.Number == process.Number).Address = place;
                Inputs.First(i => i.Number == process.Number).CorrespondingOutput = output;
                place += process.Operations.Count * 3 + 2;
                foreach (var o in process.Operations)
                {
                    Buffers.First(i => i.Number == o.MachineNumber).Connect(process, o.Number);
                }

            }
        }
        #endregion

        #region Init

        private void Init(List<sortAlgorithm> sort)
        {
            Buffers.ForEach(b => b.clerarActive());
            buildPetriNet();
            buildMark();
            timer = new TimerController(Machines);
            transitionController = new TransistionController(petriNet, Buffers, Processes, Machines, firstMachineAddress, sort);
            t = 0;
            PreparedTransitions = transitionController.getPreparedTransitions(t, timer, M);
            stopped = false;
            machinesPercentage = printMachinesPercentage();


        }
        #endregion

        public bool successResult()
        {
            int count = 0;
            foreach (var input in Inputs)
            {
                if (M[input.CorrespondingOutput.GetValueOrDefault(), 0] == input.Value)
                    count++;
            }
            return count == Inputs.Count();
        }

        private void fireTransition()
        {
            M = M + petriNet.I * T.ToColumnMatrix();
            t++;
        }

        #region Simulation

        public int Simulate(sortAlgorithm alg, bool test = false)
        {
            var list = new List<sortAlgorithm>();
            list.Add(alg);
            var result = Simulate(list, test);
            if (result == true)
                return t;
            else return -1;
        }

        public bool Simulate(List<sortAlgorithm> sortList, bool test = false)
        {
            Init(sortList);

            while (PreparedTransitions.Count > 0 || timer.AnyOperationActive())
            {
                if (stopped)
                    return false;

                T = transitionController.chooseTransiztion(timer, M);
                if (T.Any(i => i > 0))
                {

                    data.time_tick(t, T.AbsoluteMaximumIndex());
                    fireTransition();
                    timer.countTime();
                }
                else if (timer.AnyOperationActive())
                {
                    t += timer.countDownClosestTimer();

                }
                else
                {
                    break;
                }
                PreparedTransitions = transitionController.getPreparedTransitions(t, timer, M);
                //Console.Out.WriteLine("t: " + t);
                tab = (data.table as IListSource).GetList();
                var tr = T.AbsoluteMaximumIndex() + 1;
                if (!T.Any(i => i > 0))
                    tr = 0;
                if (!test)
                    chartForm.onTimeChanged(t, tr, machinesPercentage);


            }
            Console.Out.WriteLine("Simulation ended.");
            if (successResult())
                Console.Out.WriteLine("Simulation completed.");
            else
                Console.Out.WriteLine("Simulation failed.");
            //Console.Out.WriteLine(M.Column(0).ToString());
            return successResult();
        }

        public void onBreakEvent()
        {
            stopped = true;
        }


        #endregion
    }
}
