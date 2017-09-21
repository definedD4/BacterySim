using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BacterySim.Simulation
{
    public class TickEventArgs : EventArgs
    {
        public TimeSpan Elapsed { get; }

        public TickEventArgs(TimeSpan elapsed)
        {
            Elapsed = elapsed;
        }
    }

    public class SimulationClock
    {
        private readonly DispatcherTimer _timer;
        private readonly Stopwatch _stopwatch;

        // test
        private TimeSpan totalTime = TimeSpan.Zero;
        private int ticks = 0;

        public SimulationClock()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(1d / 30d);
            _timer.Tick += OnTick;

            _stopwatch = new Stopwatch();
        }

        private void OnTick(object sender, EventArgs e)
        {
            var elapsed = TimeSpan.FromMilliseconds(_stopwatch.ElapsedMilliseconds * Timescale);
            _stopwatch.Restart();

            Time += elapsed;

            totalTime += elapsed;
            ticks++;
            if(ticks % 100 == 0)
            {
                Console.WriteLine($"Avg frame time: {totalTime.TotalMilliseconds / ticks}");
                totalTime = TimeSpan.Zero;
                ticks = 0;
            }


            Tick?.Invoke(this, new TickEventArgs(elapsed));
        }

        public TimeSpan UpdateInterval
        {
            get { return _timer.Interval; }
            set { _timer.Interval = value; }
        }

        public double Timescale { get; set; } = 1d;

        public TimeSpan Time { get; private set; }

        public event EventHandler<TickEventArgs> Tick;

        public void Start()
        {
            Time = TimeSpan.Zero;
            _timer.Start();
            _stopwatch.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            _stopwatch.Stop();
        }
    }
}
