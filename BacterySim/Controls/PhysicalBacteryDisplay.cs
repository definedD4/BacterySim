using BacterySim.Simulation;
using BacterySim.Simulation.Physical;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;

namespace BacterySim.Controls
{
    public class PhysicalBacteryDisplay : FrameworkElement
    {
        private double scaleFactor = 0.01;

        private bool _dragging = false;
        private Point _startPoint;
        private double _startX, _startY;
        private TransformGroup _transfrom;
        private TranslateTransform _translateTransform;
        private ScaleTransform _scaleTransform;
        private double scale = 0;

        public PhysicalBacteryDisplay()
        {
            _translateTransform = new TranslateTransform();
            _scaleTransform = new ScaleTransform();

            _transfrom = new TransformGroup();
            _transfrom.Children.Add(_scaleTransform);
            _transfrom.Children.Add(_translateTransform);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (ItemsSource == null) return;

            SolidColorBrush brush = new SolidColorBrush();
            Pen pen = new Pen();

            drawingContext.PushTransform(_transfrom);

            foreach (var bactery in ItemsSource)
            {
                brush.Color = Color.Multiply(Colors.Red, (float)Math.Min(1.0d, bactery.Energy / 10d));
                var radius = bactery.Radius * 100d;
                var pos = bactery.Position * 100d;

                drawingContext.DrawEllipse(brush, pen, new Point(pos.X, pos.Y), radius, radius);
            }
        }

        public IReactiveCollection<BacteryPhysicalProxy> ItemsSource
        {
            get { return (IReactiveCollection<BacteryPhysicalProxy>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IReactiveCollection<BacteryPhysicalProxy>), typeof(PhysicalBacteryDisplay), new PropertyMetadata(null));

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_dragging)
            {
                var mousePos = e.GetPosition(App.Current.MainWindow);
                var offset = mousePos - _startPoint;

                _translateTransform.X = _startX + offset.X;
                _translateTransform.Y = _startY + offset.Y;
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            _dragging = false;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            _dragging = true;
            _startPoint = e.GetPosition(App.Current.MainWindow);
            _startX = _translateTransform.X;
            _startY = _translateTransform.Y;
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            scale += e.Delta * scaleFactor;
            Console.WriteLine($"Scale {scale}");
            _scaleTransform.ScaleX = Math.Pow(2, scale);
            _scaleTransform.ScaleY = Math.Pow(2, scale);
        }
    }
}
