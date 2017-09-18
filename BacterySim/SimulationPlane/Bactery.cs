using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BacterySim.SimulationPlane
{
    public class Bactery
    {
        public Point Position { get; set; }

        public double Size => 1.0d;

        public Color Color => Colors.Orange;
    }
}
