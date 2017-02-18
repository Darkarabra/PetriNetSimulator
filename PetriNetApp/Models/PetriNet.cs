using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetApp
{
    public class PetriNet
    {
        static int INPUT = 1;
        static int OUTPUT = -1;
        static int MIDDLE_STATES = 3;
        static int IO_STATES = 2;
        static int OUT_TRANSITION = 1;
        static int MACHINE_COMPONENTS = 2;

        public int Places { get; private set; }
        public int Tranzitions { get; private set; }
        int Machines { get; set; }
        int ProcessesAmount { get; set; }

        int firstMachineAddress { get; set; }
        List<Process> Processes { get; set; }

        /// <summary>
        /// macierz incydencji
        /// </summary>
        public Matrix<double> I;

        public PetriNet()
        {
            Processes = new List<Process>();

        }

        public PetriNet(List<Process> processes, List<Buffer> buffers)
        {
            Processes = processes;
            Places = 0;
            Tranzitions = 0;
            Machines = buffers.Count;
            ProcessesAmount = processes.Count;


            foreach (var seq in processes)
            {
                Places += seq.Operations.Count * MIDDLE_STATES + IO_STATES;
                Tranzitions += seq.Operations.Count * MIDDLE_STATES + OUT_TRANSITION;
            }
            Places += Machines * MACHINE_COMPONENTS;
            buildIncidenceMatrix();
        }

        public int getFirstMachineId()
        {
            var result = 0;
            foreach (var process in Processes)
            {
                result += (3 * process.Operations.Count) + 2;
            }
            return result;
        }

        private void buildIncidenceMatrix()
        {
            I = Matrix<double>.Build.Random(Places, Tranzitions);
            I.Clear();
            int p = 0;
            int t = 0;
            firstMachineAddress = getFirstMachineId();

            foreach (var process in Processes)
            {
                foreach (var operation in process.Operations)
                {
                    int machine = firstMachineAddress + operation.MachineNumber - 1;
                    int buffer = firstMachineAddress + Machines + operation.MachineNumber - 1;
                    if (operation.Number == 1)
                    {
                        //in
                        I[p++, t] = OUTPUT;
                        I[p, t] = INPUT;
                        I[buffer, t++] = OUTPUT;
                    }
                    //1
                    I[p++, t] = OUTPUT;
                    I[p, t] = INPUT;
                    I[machine, t++] = OUTPUT;

                    //2
                    I[p++, t] = OUTPUT;
                    I[p, t] = INPUT;
                    I[machine, t++] = INPUT;

                    //out

                    I[p++, t] = OUTPUT;
                    I[p, t] = INPUT;
                    I[buffer, t] = INPUT;

                    var nextOperation = process.Operations.FirstOrDefault(i =>
                    i.Number == operation.Number + 1);
                    if (nextOperation != null)
                    {
                        var nextBuffer = firstMachineAddress + Machines + nextOperation.MachineNumber - 1;
                        I[nextBuffer, t++] = OUTPUT;
                    }
                    else
                    {
                        p++;
                        t++;
                    }
                }
            }
        }
    }
}
