using System;
using System.Linq;
using System.Reactive;
using System.Windows;
using System.Windows.Media;
using BacterySim.Simulation;
using ReactiveUI;
using OxyPlot;
using OxyPlot.Series;

namespace BacterySim.Features.Main
{
    public class MainViewModel : ReactiveObject
    {
        public SimulationContext Context { get; } = new SimulationContext();

        public ReactiveList<DataPoint> FoodPlot { get; } = new ReactiveList<DataPoint>();

        public ReactiveCommand<Unit, Unit> StartSimulation { get; }

        public MainViewModel()
        {
            StartSimulation = ReactiveCommand.Create(() => { });
            StartSimulation.Subscribe(_ => { Context.Start(); });

            Context.PropertiesChanged += (s, e) =>
            {
                FoodPlot.Add(new DataPoint(Context.Time.TotalSeconds, Context.Properties.Food));
            };

            Context.Bacteries.AddRange(new[]
            {
                new Bactery {Size = 1.0d}
            });
        }
    }
}