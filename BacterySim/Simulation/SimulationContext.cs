using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BacterySim.Controls;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using ReactiveUI;
using BacterySim.Simulation.Physical;

namespace BacterySim.Simulation
{
    public class SimulationContext
    {
        private readonly SimulationClock _simulationClock;

        public SimulationProperties Properties { get; }

        public ReactiveList<Bactery> Bacteries { get; } = new ReactiveList<Bactery>();

        public TimeSpan Time => _simulationClock.Time;

        public SimulationContext()
        {
            _simulationClock = new SimulationClock();
            _simulationClock.Tick += (s, e) => Step(e.Elapsed);          

            Properties = new SimulationProperties()
            {
                Food = 50,
                FoodRate = 10d,
            };
        }

        private void Step(TimeSpan delta)
        {
            Properties.Step(delta);

            var toAdd = new List<Bactery>();
            var toRemove = new List<Bactery>();

            foreach(var bactery in Bacteries)
            {
                bactery.Update(delta, Properties);

                if(bactery.CanSplit)
                {
                    var split = bactery.Split();
                    toRemove.Add(bactery);
                    toAdd.Add(split.Item1);
                    toAdd.Add(split.Item2);

                    bactery.Watch?.OnSplit(split.Item1, split.Item2);
                }
            }

            toAdd.ForEach(Bacteries.Add);
            toRemove.ForEach(b => Bacteries.Remove(b));

            PropertiesChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Start()
        {
            _simulationClock.Start();
        }

        public void Stop()
        {
            _simulationClock.Stop();
        }

        public event EventHandler PropertiesChanged;
    }
}
