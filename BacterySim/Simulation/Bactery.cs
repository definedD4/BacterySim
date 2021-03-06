﻿using System;
using System.Windows.Media;

namespace BacterySim.Simulation
{
    public class Bactery
    {
        public double Size { get; set; } = 1.0d;

        public double Energy { get; set; } = 0.0d;

        public double FoodAbsorbtionRatePerSize { get; set; } = 0.05d;

        public double EnergyUsageRatePerSize { get; set; } = 0.005d;

        public IBacteryWatch Watch { get; set; }

        public void Update(TimeSpan delta, SimulationProperties properties)
        {
            double sec = delta.TotalSeconds;

            double deltaFood = Math.Min(FoodAbsorbtionRatePerSize * Size * properties.Food * sec, properties.Food);

            Energy += deltaFood;
            Energy -= EnergyUsageRatePerSize * Size;

            properties.Food -= deltaFood;
        }

        public bool CanSplit => Energy > Size * 5d;

        public Tuple<Bactery, Bactery> Split()
        {
            return Tuple.Create(
                new Bactery {Size = NewRadius(Size), Energy = NewEnergy(Energy)},
                new Bactery {Size = NewRadius(Size), Energy = NewEnergy(Energy) }
            );
        }

        private double NewRadius(double oldRadius)
        {
            return oldRadius + GlobalRandom.NextDouble() * 0.7d;
        }

        private double NewEnergy(double oldEnergy)
        {
            return oldEnergy / 2.0d - Size * 0.05d;
        }
    }
}