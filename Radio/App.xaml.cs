using Radio.Model;
using Radio.View;
using Radio.ViewModel;
using System.Windows;

namespace Radio
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly RadioModel m_RadioModel = new RadioModel(50d, 87.5d);
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainView()
            {
                DataContext = new RadioViewModel(m_RadioModel)
            };
            MainWindow.Show();
            Dispatcher.InvokeAsync(TryPlayMusic, System.Windows.Threading.DispatcherPriority.Background);
            base.OnStartup(e);
        }

        /// <summary>
        /// Main function for playing music loop with UI being unlocked. 
        /// Could block UI with too high of a priority, while too low of a priority would interrupt the potential music. 
        /// </summary>
        protected void TryPlayMusic()
        {
            m_RadioModel.TryPlayMusic();
            Dispatcher.InvokeAsync(TryPlayMusic, System.Windows.Threading.DispatcherPriority.Background);
        }

    }

}
