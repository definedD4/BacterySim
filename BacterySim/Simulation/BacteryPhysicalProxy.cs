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
        private readonly Bactery _bactery;
        private readonly SimulationContext _context;
        private readonly Fixture _fixture;
        private readonly Body _body;

        public Vector Position { get; private set; }

        public double Radius => _bactery.Radius;

        public Color Color => _bactery.Color;

        public BacteryPhysicalProxy(Bactery bactery, SimulationContext context, Vector position)
        {
            if (bactery == null) throw new ArgumentNullException(nameof(bactery));
            if (context == null) throw new ArgumentNullException(nameof(context));

            _bactery = bactery;
            _context = context;
            Position = position;

            _body = new Body(_context.World, new Vector2((float) position.X, (float) position.Y))
            {
                BodyType = BodyType.Dynamic,
                UserData = this
            };

            var shape = new CircleShape((float)bactery.Radius, 1.0f);
            _fixture = _body.CreateFixture(shape);
        }

        public void Update()
        {
            var pos = _body.Position;
            Position = new Vector(pos.X, pos.Y);

            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Split()
        {
            var bacteries = _bactery.Split();
            _context.OnSplit(this, bacteries.Item1, bacteries.Item2);
        }

        public event EventHandler StateChanged;
    }
}
