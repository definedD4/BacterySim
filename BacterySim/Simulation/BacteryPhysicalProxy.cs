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

namespace BacterySim.Simulation
{
    public class BacteryPhysicalProxy
    {
        private readonly SimulationContext _context;
        private readonly Fixture _fixture;
        private readonly Body _body;

        public Bactery Bactery { get; }

        public Vector Position { get; private set; }

        public double Radius => Bactery.Size;

        public Color Color => Color.Multiply(Colors.Red, (float)Math.Min(1.0d, Bactery.Energy / 10d));

        public BacteryPhysicalProxy(Bactery bactery, SimulationContext context, Vector position)
        {
            if (bactery == null) throw new ArgumentNullException(nameof(bactery));
            if (context == null) throw new ArgumentNullException(nameof(context));

            Bactery = bactery;
            _context = context;
            Position = position;

            _body = new Body(_context.World, new Vector2((float) position.X, (float) position.Y))
            {
                BodyType = BodyType.Dynamic,
                UserData = this
            };

            var shape = new CircleShape((float)bactery.Size, 1.0f);
            _fixture = _body.CreateFixture(shape);
        }

        public void UpdatePosition()
        {
            var pos = _body.Position;
            Position = new Vector(pos.X, pos.Y);

            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Split()
        {
            var bacteries = Bactery.Split();
            _context.World.RemoveBody(_body);
            _context.OnSplit(this, bacteries.Item1, bacteries.Item2);
        }

        public event EventHandler StateChanged;
    }
}
