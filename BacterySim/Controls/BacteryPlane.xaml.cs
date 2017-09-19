using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using BacterySim.Simulation;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using ReactiveUI;

namespace BacterySim.Controls
{
    /// <summary>
    ///     Логика взаимодействия для BacteryPlane.xaml
    /// </summary>
    public partial class BacteryPlane : UserControl
    {
        private IReactiveCollection<BacteryPhysicalProxy> _bacterySource;

        public IReactiveCollection<BacteryPhysicalProxy> BacterySource
        {
            get => _bacterySource;
            set
            {
                _bacterySource = value;
                BacteriesList.ItemsSource = _bacterySource.CreateDerivedCollection(
                    bactery => new BacteryDisplay(bactery)
                );
            }
        }

        public BacteryPlane()
        {
            InitializeComponent();
        }
    }
}