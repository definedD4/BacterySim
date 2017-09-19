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

namespace BacterySim.Simulation
{
    public class SimulationContext
    {
        private readonly SimulationClock _simulationClock;

        public World World { get; }

        public ReactiveList<BacteryPhysicalProxy> Bacteries { get; } = new ReactiveList<BacteryPhysicalProxy>();

        public SimulationContext()
        {
            _simulationClock = new SimulationClock();
            _simulationClock.Tick += (s, e) => Step(e.Elapsed);

            World = new World(Vector2.Zero);
        }

        private void Step(TimeSpan delta)
        {
            World.Step((float)delta.TotalMilliseconds);

            foreach (var body in World.BodyList)
            {
                var proxy = body.UserData as BacteryPhysicalProxy;

                Debug.Assert(proxy != null, "BacteryPlane: display != null");
                if (GlobalRandom.NextDouble() < 1d * delta.TotalSeconds)
                {
                    proxy.Split();
                }

                proxy.Update();
            }
        }

        public void Start()
        {
            _simulationClock.Start();
        }

        public void Stop()
        {
            _simulationClock.Stop();
        }

        public void OnSplit(BacteryPhysicalProxy oldProxy, Bactery newBactery1, Bactery newBactery2)
        {
            Bacteries.Remove(oldProxy);

            var direction = GlobalRandom.NextDirection();

            var pos1 = oldProxy.Position + direction * newBactery1.Radius;
            var pos2 = oldProxy.Position + direction * newBactery2.Radius;

            Bacteries.Add(new BacteryPhysicalProxy(newBactery1, this, pos1));
            Bacteries.Add(new BacteryPhysicalProxy(newBactery2, this, pos2));
        }
    }
}
