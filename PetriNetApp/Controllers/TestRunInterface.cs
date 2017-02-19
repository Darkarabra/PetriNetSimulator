using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetApp
{
    public interface TestRunListener
    {
        void onTestRun(int MIN, int MAX, string File);
    }
}
