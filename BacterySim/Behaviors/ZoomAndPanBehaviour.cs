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
    public class ZoomAndPanBehavior : Behavior<UIElement>
    {
        private double scaleFactor = 0.01;

        private bool _dragging = false;
        private Point _startPoint;
        private double _startX, _startY;
        private TranslateTransform _translateTransform;
        private ScaleTransform _scaleTransform;
        private double scale = 0;

        protected override void OnAttached()
        {
            _translateTransform = new TranslateTransform();
            _scaleTransform = new ScaleTransform();

            var transformGroup = new TransformGroup();
            transformGroup.Children.Add(_scaleTransform);
            transformGroup.Children.Add(_translateTransform);
            AssociatedObject.RenderTransform = transformGroup;

            AssociatedObject.MouseDown += MouseDown;
            AssociatedObject.MouseUp += MouseUp;
            AssociatedObject.MouseMove += MouseMove;
            AssociatedObject.MouseWheel += MouseWheel;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseDown -= MouseDown;
            AssociatedObject.MouseUp -= MouseUp;
            AssociatedObject.MouseMove -= MouseMove;
            AssociatedObject.MouseWheel -= MouseWheel;
        }

        private void MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(_dragging)
            {
                var mousePos = e.GetPosition(App.Current.MainWindow);
                var offset = mousePos - _startPoint;

                _translateTransform.X = _startX + offset.X;
                _translateTransform.Y = _startY + offset.Y;
            }
        }

        private void MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _dragging = false;
        }

        private void MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _dragging = true;
            _startPoint = e.GetPosition(App.Current.MainWindow);
            _startX = _translateTransform.X;
            _startY = _translateTransform.Y;
        }

        private void MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            scale += e.Delta * scaleFactor;
            Console.WriteLine($"Scale {scale}");
            _scaleTransform.ScaleX = Math.Pow(2, scale);
            _scaleTransform.ScaleY = Math.Pow(2, scale);
        }
    }
}

