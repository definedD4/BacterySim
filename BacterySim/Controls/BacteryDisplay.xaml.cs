using BacterySim.Simulation;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BacterySim.Controls
{
    /// <summary>
    /// Логика взаимодействия для BacteryDisplay.xaml
    /// </summary>
    public partial class BacteryDisplay : UserControl, INotifyPropertyChanged
    {
        private readonly World _world;
        private readonly Fixture _fixture;
        private readonly Body _body;
        private readonly Bactery _bactery;

        public BacteryDisplay(World world, Bactery bactery)
        {
            _world = world;
            _bactery = bactery;

            _body = new Body(world);
            _body.BodyType = BodyType.Dynamic;
            _body.UserData = this;

            var shape = new CircleShape((float)bactery.Radius, 1.0f);
            _fixture = _body.CreateFixture(shape);

            InitializeComponent();
        }

        public Vector Position { get; private set; }

        public double Radius => _bactery.Radius;

        public Color Color => _bactery.Color;

        public void UpdateDisplay()
        {
            var pos = _body.Position;
            Position = new Vector(pos.X, pos.Y);

            this.SetValue(Canvas.LeftProperty, Position.X - Radius);
            this.SetValue(Canvas.TopProperty, Position.Y - Radius);

            NotifyPropertyChanged(null);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
