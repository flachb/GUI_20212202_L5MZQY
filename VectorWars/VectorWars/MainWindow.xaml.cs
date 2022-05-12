using System;
using System.Collections.Generic;
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
        private List<HighScores> _highScores;

        public MainWindow()
        {
            InitializeComponent();
        }

        public int CurrentWave = 1;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = new MainWindowViewModel();
            _highScores = new List<HighScores>();

            DataContext = _viewModel;
            display.SetupModel(_viewModel.Game);
            label_cw.Content = CurrentWave;
            if(!File.Exists("highscores.txt"))
            {
                File.Create("highscores.txt").Close();
            }
            else
            {
                string[] line = new string[2];
                StreamReader streamReader = new StreamReader("highscores.txt");
                while(!streamReader.EndOfStream)
                {
                    line = streamReader.ReadLine().Split(';');
                    _highScores.Add(new HighScores() { Name = line[0], Score = int.Parse(line[1]) });
                }
                streamReader.Close();
            }
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
            if (_viewModel.CurrentHP <= 0)
            {
                _viewModel.Game.Stop();
                var result = MessageBox.Show("Elfogyott az életerőd! :( A játéknak vége!", "GAME OVER", MessageBoxButton.OK, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    HighScoreCheckAndSave(_highScores, new HighScores() { Name = _viewModel.Player.Name, Score = Convert.ToInt32(_viewModel.Player.Score) });
                    this.Close();
                }
            }
            if(_viewModel.Game.Map != null)
            {
                if(_viewModel.Game.Map.CurrentWave == _viewModel.Game.Map.Waves.Count && _viewModel.Game.Map.EnemyHandler.Elements.Count == 0)
                {
                    _viewModel.Game.Stop();
                    var result = MessageBox.Show("Gratulálunk! Nyertél!", "Győzelem!", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (result == MessageBoxResult.OK)
                    {
                        HighScoreCheckAndSave(_highScores, new HighScores() { Name = _viewModel.Player.Name, Score = Convert.ToInt32(_viewModel.Player.Score) });
                        this.Close();
                    }
                }
                if(CurrentWave-1 < _viewModel.Game.Map.CurrentWave && _viewModel.Game.Map.EnemyHandler.Elements.Count == 0)
                {
                    CurrentWave++;
                    label_cw.Content = CurrentWave;
                }
            }
        }

        private void tb_username_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _viewModel.username = tb_username.Text;
        }

        private void HighScoreCheckAndSave(IList<HighScores> _highScores ,HighScores _currentScore)
        {
            int _counter = 0;
            bool _foundend = false;
            foreach (var item in _highScores)
            {
                if(item.Name == _currentScore.Name)
                {
                    if(item.Score < _currentScore.Score)
                    {
                        _highScores[_counter].Score = _currentScore.Score;
                        _foundend = true;
                    }
                }
                _counter++;
            }
            if(!_foundend)
            {
                _highScores.Add(_currentScore);
            }
            StreamWriter streamWriter = new StreamWriter("highscores.txt");
            foreach (var item in _highScores)
            {
                streamWriter.WriteLine(item.Name + ";" + item.Score);
            }
            streamWriter.Close();
        }
    }

    public class HighScores
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _score;

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }


    }
}
