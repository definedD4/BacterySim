using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace BacterySim.Simulation.Physical
{
    public class BacteryPhysicalProxy : IBacteryWatch
    {
        private readonly PhysicalSimulationContext _context;
        private readonly Fixture _fixture;

        public Body Body { get; }

        public Bactery Bactery { get; }

        public Vector Position => new Vector(Body.Position.X, Body.Position.Y);

        public double Radius => Bactery.Size;

        public double Energy => Bactery.Energy;

        public BacteryPhysicalProxy(Bactery bactery, PhysicalSimulationContext context, Vector position)
        {
            if (bactery == null) throw new ArgumentNullException(nameof(bactery));
            if (context == null) throw new ArgumentNullException(nameof(context));

            Bactery = bactery;
            _context = context;

            Body = new Body(_context.World, new Vector2((float) position.X, (float) position.Y))
            {
                BodyType = BodyType.Dynamic,
                UserData = this
            };

            var shape = new CircleShape((float)bactery.Size, 1.0f);
            _fixture = Body.CreateFixture(shape);
        }

        public void OnSplit(Bactery new1, Bactery new2)
        {
            _context.OnSplit(this, new1, new2);
        }
    }
}
