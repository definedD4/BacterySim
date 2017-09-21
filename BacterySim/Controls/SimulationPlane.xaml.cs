using BacterySim.Simulation;
using BacterySim.Simulation.Physical;
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

namespace BacterySim.Controls
{
    /// <summary>
    /// Логика взаимодействия для SimulationPlane.xaml
    /// </summary>
    public partial class SimulationPlane : UserControl
    {
        private 
            PhysicalSimulationContext _physicalContext;

        public SimulationPlane()
        {
            InitializeComponent();
        }
        
        public SimulationContext SimulationContext
        {
            get { return (SimulationContext)GetValue(SimulationContextProperty); }
            set { SetValue(SimulationContextProperty, value); }
        }

        public static readonly DependencyProperty SimulationContextProperty =
            DependencyProperty.Register("SimulationContext", typeof(SimulationContext), typeof(SimulationPlane),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback((d, e) => (d as SimulationPlane).OnContextChanged()))
            );

        private void OnContextChanged()
        {
            _physicalContext = new PhysicalSimulationContext();
            foreach(var bactery in SimulationContext.Bacteries)
            {
                var proxy = new BacteryPhysicalProxy(bactery, _physicalContext, GlobalRandom.NextDirection() * 5d);
                bactery.Watch = proxy;
            }
            Content.ItemsSource = SimulationContext.Bacteries.CreateDerivedCollection(b => b.Watch as BacteryPhysicalProxy);
        }
    }
}
