using BacterySim.Simulation;
using FarseerPhysics.Dynamics;
using ReactiveUI;
using System;
using System.Collections.Generic;
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
using Microsoft.Xna.Framework;

namespace BacterySim.Controls
{
    /// <summary>
    /// Логика взаимодействия для BacteryPlane.xaml
    /// </summary>
    public partial class BacteryPlane : UserControl
    {
        private readonly SimulationClock _simulationClock;
        private readonly World _world;
        private IReactiveCollection<Bactery> _bacterySource;

        public BacteryPlane()
        {
            InitializeComponent();

            _simulationClock = new SimulationClock();
            _simulationClock.Tick += (s, e) => Step(e.Elapsed);

            _world = new World(Vector2.Zero);
        }

        public IReactiveCollection<Bactery> BacterySource
        {
            get { return _bacterySource; }
            set
            {
                _bacterySource = value;
                BacteriesList.ItemsSource = _bacterySource.CreateDerivedCollection(
                    bactery => new BacteryDisplay(_world, bactery)
                );
            }
        }

        private void Step(TimeSpan delta)
        {
            _world.Step((float)delta.TotalMilliseconds);

            foreach(var body in _world.BodyList)
            {
                var display = body.UserData as BacteryDisplay;

                display.UpdateDisplay();
            }
        }


        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _simulationClock.Start();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            _simulationClock.Stop();
        }
    }
}
