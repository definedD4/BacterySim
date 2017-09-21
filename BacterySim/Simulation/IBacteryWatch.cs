using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacterySim.Simulation
{
    public interface IBacteryWatch
    {
        void OnSplit(Bactery new1, Bactery new2);
    }
}
