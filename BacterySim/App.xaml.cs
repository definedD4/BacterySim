using System.Windows;
using BacterySim.Features.Main;
using ReactiveUI;
using Splat;

namespace BacterySim
{
    /// <summary>
    ///     Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            RegisterViews();

            new MainView {ViewModel = new MainViewModel()}.Show();

            base.OnStartup(e);
        }

        private void RegisterViews()
        {
            Locator.CurrentMutable.Register<IViewFor<MainViewModel>>(() => new MainView());
        }
    }
}