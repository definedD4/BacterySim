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
using Microsoft.Xna.Framework;

namespace BacterySim.Controls
{
    /// <summary>
    /// Логика взаимодействия для BacteryDisplay.xaml
    /// </summary>
    public partial class BacteryDisplay : UserControl, INotifyPropertyChanged
    {
        private readonly BacteryPhysicalProxy _bactery;

        public BacteryDisplay(BacteryPhysicalProxy bactery)
        {
            if (bactery == null) throw new ArgumentNullException(nameof(bactery));

            _bactery = bactery;
            _bactery.StateChanged += (s, e) => UpdateDisplay();

            InitializeComponent();

            UpdateDisplay();
        }

        public Vector Position => _bactery.Position * 100d;

        public double Radius => _bactery.Radius * 100d;

        public Brush Fill => new SolidColorBrush(_bactery.Color);

        public void UpdateDisplay()
        {
            this.SetValue(Canvas.LeftProperty, Position.X - Radius);
            this.SetValue(Canvas.TopProperty, Position.Y - Radius);

            NotifyPropertyChanged(null);
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
