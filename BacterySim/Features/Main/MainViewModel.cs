using System.Windows.Media;
using BacterySim.Simulation;
using ReactiveUI;

namespace BacterySim.Features.Main
{
    public class MainViewModel : ReactiveObject
    {
        public ReactiveList<Bactery> Bacteries { get; } = new ReactiveList<Bactery>();

        public MainViewModel()
        {
            Bacteries.AddRange(new[]
            {
                new Bactery {Radius = 1.0d, Color = Colors.Yellow}
            });
        }
    }
}