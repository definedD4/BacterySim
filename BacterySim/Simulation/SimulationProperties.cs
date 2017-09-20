using System;

namespace BacterySim.Simulation
{
    public class SimulationProperties
    {
        public double Food { get; set; }

        public double FoodRate { get; set; } = 1d;

        public void Step(TimeSpan delta)
        {
            double sec = delta.TotalSeconds;

            Food += FoodRate * sec;
        }
    }
}