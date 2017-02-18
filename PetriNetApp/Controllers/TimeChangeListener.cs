using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetApp
{
    public interface TimeChangeListener
    {
        void onTimeChanged(int t, int tr, string machines);
    }
}
