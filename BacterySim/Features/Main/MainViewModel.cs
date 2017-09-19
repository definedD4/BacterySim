using System;
using System.Linq;
using System.Reactive;
using System.Windows;
using System.Windows.Media;
using BacterySim.Simulation;
using ReactiveUI;

namespace BacterySim.Features.Main
{
    public class MainViewModel : ReactiveObject
    {
        public SimulationContext Context { get; } = new SimulationContext();

        public ReactiveCommand<Unit, Unit> StartSimulation { get; }

        public MainViewModel()
        {
            StartSimulation = ReactiveCommand.Create(() => { });
            StartSimulation.Subscribe(_ => { Context.Start(); });

            Context.Bacteries.AddRange(new[]
            {
                new Bactery {Radius = 1.0d, Color = Colors.Yellow}
            }.Select(b => new BacteryPhysicalProxy(b, Context, new Vector(5, 5))));
        }
    }
}