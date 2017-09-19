using System;
using System.Windows.Media;

namespace BacterySim.Simulation
{
    public class Bactery
    {
        public double Radius { get; set; } = 1.0d;

        public Color Color { get; set; } = Colors.Red;

        public Tuple<Bactery, Bactery> Split()
        {
            return Tuple.Create(
                new Bactery {Radius = NewRadius(Radius), Color = NewColor(Color)},
                new Bactery {Radius = NewRadius(Radius), Color = NewColor(Color)}
            );
        }

        private double NewRadius(double oldRadius)
        {
            return oldRadius + GlobalRandom.NextDouble() * 0.1d;
        }

        private Color NewColor(Color oldColor)
        {
            return Color.Add(oldColor,
                Color.Multiply(
                    Color.FromScRgb(1.0f,
                        (float) GlobalRandom.NextDouble(),
                        (float) GlobalRandom.NextDouble(),
                        (float) GlobalRandom.NextDouble()),
                    0.3f));
        }
    }
}