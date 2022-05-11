using System.IO;
using System.Windows;
using System.Windows.Input;

namespace VectorWars
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = new MainWindowViewModel();

            DataContext = _viewModel;
            display.SetupModel(_viewModel.Game);

            //if(!File.Exists("highscores.txt"))
            //{
            //    File.Create("highscores.txt").Close();
            //}

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                _viewModel.PauseCommand.Execute(null);
                e.Handled = true;
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            var mousePoint = Mouse.GetPosition(window);

            display.MousePositioin = new Core.Common.Point((float)mousePoint.X, (float)mousePoint.Y);
        }

        private void display_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                _viewModel.OnLeftClick(Mouse.GetPosition(window));
            else if (e.ChangedButton == MouseButton.Right)
                _viewModel.OnRightClick(Mouse.GetPosition(window));
        }

        private void Label_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            if(_viewModel.CurrentHP <= 0)
            {
                _viewModel.Game.Stop();
                var result = MessageBox.Show("Elfogyott az életerőd! :( A játéknak vége!", "GAME OVER", MessageBoxButton.OK, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                    this.Close();
            }
            if(_viewModel.Game.Map != null)
            {
                if(_viewModel.Game.Map.CurrentWave == 2 && _viewModel.Game.Map.EnemyHandler.Elements.Count == 0)
                {
                    _viewModel.Game.Stop();
                    var result = MessageBox.Show("Gratulálunk! Nyertél!", "Győzelem!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                        this.Close();
                }
            }
        }
    }
}
