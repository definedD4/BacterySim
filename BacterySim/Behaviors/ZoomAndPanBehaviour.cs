using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace BacterySim.Behaviors
{
    public class ZoomAndPanBehavior : Behavior<Canvas>
    {
        private bool _dragging = false;
        private Point _startPoint;
        private double _startX, _startY;
        private TranslateTransform _translateTransform;

        protected override void OnAttached()
        {
            _translateTransform = new TranslateTransform();
            AssociatedObject.RenderTransform = _translateTransform;

            AssociatedObject.MouseDown += MouseDown;
            AssociatedObject.MouseUp += MouseUp;
            AssociatedObject.MouseMove += MouseMove;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseDown -= MouseDown;
            AssociatedObject.MouseUp -= MouseUp;
            AssociatedObject.MouseMove -= MouseMove;
        }

        private void MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(_dragging)
            {
                var mousePos = e.GetPosition(App.Current.MainWindow);
                var offset = mousePos - _startPoint;

                _translateTransform.X = _startX + offset.X;
                _translateTransform.Y = _startY + offset.Y;

                Console.WriteLine($"Pan offset {offset}");
            }
        }

        private void MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _dragging = false;
            Console.WriteLine("Pan stopped");
        }

        private void MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _dragging = true;
            _startPoint = e.GetPosition(App.Current.MainWindow);
            _startX = _translateTransform.X;
            _startY = _translateTransform.Y;
            Console.WriteLine($"Pan started on {_startPoint}");
        }
    }
}
