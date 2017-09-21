using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacterySim.Simulation.Physical
{
    public class PhysicalSimulationContext
    {
        public World World { get; }

        public PhysicalSimulationContext()
        {
            World = new World(Vector2.Zero);
        }

        public void Step(TimeSpan delta)
        {
            World.Step((float)delta.TotalMilliseconds);

            foreach (var body in World.BodyList)
            {
                var proxy = body.UserData as BacteryPhysicalProxy;

                Debug.Assert(proxy != null, "BacteryPlane: display != null");

                if (proxy.Bactery.Energy <= 0)
                {
                    World.RemoveBody(body);
                }
            }
        }

        public void OnSplit(BacteryPhysicalProxy oldProxy, Bactery newBactery1, Bactery newBactery2)
        {
            World.RemoveBody(oldProxy.Body);

            var direction = GlobalRandom.NextDirection();

            var pos1 = oldProxy.Position + direction * newBactery1.Size;
            var pos2 = oldProxy.Position + direction * newBactery2.Size;

            var p1 = new BacteryPhysicalProxy(newBactery1, this, pos1);
            var p2 = new BacteryPhysicalProxy(newBactery2, this, pos2);

            newBactery1.Watch = p1;
            newBactery2.Watch = p2;
        }
    }
}
