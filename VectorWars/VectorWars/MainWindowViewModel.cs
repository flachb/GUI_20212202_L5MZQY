using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using VectorWars.Core;
using VectorWars.Core.Elements.Effects;
using VectorWars.Core.Elements.Enemies;
using VectorWars.Core.Elements.Projectiles;
using VectorWars.Core.Elements.Turrets;
using VectorWars.Core.Elements.Types;

namespace VectorWars
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private const string EASY_LEVEL_RESOURCE_NAME = "VectorWars.Levels.easy.lvl";
        private const string HARD_LEVEL_RESOURCE_NAME = "VectorWars.Levels.hard.lvl";

        private readonly Game _game;

        private readonly Brush _selectedBrush;
        private readonly Brush _defaultBrush;


        public event PropertyChangedEventHandler PropertyChanged;

        public Game Game => _game;
        public Player Player => _game.Player;

        //public int CurrentWave => _currentWave;

        public int CurrentHP => Player.Health;


        public ICommand PauseCommand { get; }

        private ICommand _startCommand;
        private ICommand _continueCommand;

        private ICommand _startGameCommand;
        public ICommand StartGameCommand
        {
            get => _startGameCommand;
            set => SetValue(ref _startGameCommand, value);
        }

        public ICommand ExitCommand { get; }
        public ICommand HighScoresCommand { get; }

        private Visibility _isMenuVisible = Visibility.Visible;
        public Visibility IsMenuVisible
        {
            get => _isMenuVisible;
            set => SetValue(ref _isMenuVisible, value);
        }

        private Visibility _isHardModeVisible = Visibility.Visible;
        public Visibility IsHardModeVisible
        {
            get => _isHardModeVisible;
            set => SetValue(ref _isHardModeVisible, value);
        }

        private bool _hardModeChecked = false;
        public bool HardModeChecked
        {
            get => _hardModeChecked;
            set => SetValue(ref _hardModeChecked, value);
        }

        private string _startGameButtonContent = "Start Game";
        public string StartGameButtonContent
        {
            get => _startGameButtonContent;
            set => SetValue(ref _startGameButtonContent, value);
        }

        public EmojiEnemy DefaultEmoji { get; } = EmojiEnemy.Default;
        public ManEnemy DefaultMan { get; } = ManEnemy.Default;
        public RabbitEnemy DefaultRabbit { get; } = RabbitEnemy.Default;
        public SkullEnemy DefaultSkull { get; } = SkullEnemy.Default;

        public ICommand MachineGunCommand { get; }
        public ICommand LaserGunCommand { get; }
        public ICommand RocketLauncherCommand { get; }
        public ICommand FreezerGunCommand { get; }

        private Brush _machineGunColor;
        public Brush MachineGunBackground
        {
            get => _machineGunColor;
            set => SetValue(ref _machineGunColor, value);
        }

        private Brush _laserGunColor;
        public Brush LaserGunBackground
        {
            get => _laserGunColor;
            set => SetValue(ref _laserGunColor, value);
        }

        private Brush _rocketLauncherColor;
        public Brush RocketLauncherBackground
        {
            get => _rocketLauncherColor;
            set => SetValue(ref _rocketLauncherColor, value);
        }

        private Brush _freezerGunColor;
        public Brush FreezerGunBackground
        {
            get => _freezerGunColor;
            set => SetValue(ref _freezerGunColor, value);
        }

        private Type _selectedTurretType;

        public MainWindowViewModel()
        {
            _selectedBrush = Brushes.LightGreen;
            _defaultBrush = Brushes.LightGray;

            PauseCommand = new RelayCommand(ExecutePauseCommand);
            _startCommand = new RelayCommand(ExecuteStartCommand);
            _continueCommand = new RelayCommand(ExecuteContinueCommand);
            StartGameCommand = _startCommand;
            ExitCommand = new RelayCommand(ExecuteExitCommand);
            HighScoresCommand = new RelayCommand(ExecuteHighScoreCommand);

            _machineGunColor = _defaultBrush;
            _laserGunColor = _defaultBrush;
            _rocketLauncherColor = _defaultBrush;
            _freezerGunColor = _defaultBrush;

            MachineGunCommand = new RelayCommand(ExecuteTurretSelectedCommand<MachineGunTurret>);
            LaserGunCommand = new RelayCommand(ExecuteTurretSelectedCommand<LaserGunTurret>);
            RocketLauncherCommand = new RelayCommand(ExecuteTurretSelectedCommand<RocketLauncherTurret>);
            FreezerGunCommand = new RelayCommand(ExecuteTurretSelectedCommand<FreezerGunTurret>);

            var player = new Player("Charlie");
            _game = new Game(player);
            _game.MapFinished += () =>
            {
                StartGameCommand = _startCommand;
                IsHardModeVisible = Visibility.Visible;
            };

            OnPropertyChanged(nameof(Player));
            OnPropertyChanged(nameof(Game));
        }

        public void OnRightClick(Point position)
        {
            var closestGridElement =
                _game.Map.Grid.OrderBy(
                        g => Core.Common.Point.Distance(
                            g.Center,
                            new Core.Common.Point(
                                (float)position.X,
                                (float)position.Y)))
                .First();

            if (closestGridElement.OccupiedBy is null)
                return;

            _game.RemoveTurret(closestGridElement);
        }

        public void OnLeftClick(Point position)
        {
            if (_selectedTurretType is null)
                return;

            var closestGridElement =
                _game.Map.Grid.OrderBy(
                        g => Core.Common.Point.Distance(
                            g.Center,
                            new Core.Common.Point(
                                (float)position.X,
                                (float)position.Y)))
                .First();

            if (closestGridElement.Type != Core.Common.GridElementType.Grass
                || closestGridElement.OccupiedBy is not null)
                return;

            switch (_selectedTurretType.Name)
            {
                case nameof(MachineGunTurret):
                    _game.AddTurret<MachineGunTurret, BulletProjectile, BulletEffect>(closestGridElement);
                    break;
                case nameof(LaserGunTurret):
                    _game.AddTurret<LaserGunTurret, LaserProjectile, LaserEffect>(closestGridElement);
                    break;
                case nameof(RocketLauncherTurret):
                    _game.AddTurret<RocketLauncherTurret, RocketProjectile, RocketEffect>(closestGridElement);
                    break;
                case nameof(FreezerGunTurret):
                    _game.AddTurret<FreezerGunTurret, FreezerProjectile, FreezerEffect>(closestGridElement);
                    break;
            }
        }

        private void ExecuteStartCommand()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = HardModeChecked
                ? HARD_LEVEL_RESOURCE_NAME
                : EASY_LEVEL_RESOURCE_NAME;

            string[] level;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                level = reader.ReadToEnd().Replace("\r", "").Split('\n');
            }

            var map = _game.MapBuilder.Build(level);
                _game.SetupMap(map);

            IsMenuVisible = Visibility.Collapsed;
            _game.Start();

            StartGameCommand = _continueCommand;
            IsHardModeVisible = Visibility.Collapsed;
        }

        private void ExecuteContinueCommand()
        {
            IsMenuVisible = Visibility.Collapsed;
            _game.Start();
        }

        private void ExecutePauseCommand()
        {
            if (IsMenuVisible != Visibility.Visible)
            {
                StartGameButtonContent = "Continue";
                IsHardModeVisible = Visibility.Collapsed;
                IsMenuVisible = Visibility.Visible;
                _game.Stop();
            }
            else
            {
                IsMenuVisible = Visibility.Collapsed;
                _game.Start();
            }
        }

        private void ExecuteTurretSelectedCommand<TTurret>()
            where TTurret : ITurret
        {
            MachineGunBackground = _defaultBrush;
            LaserGunBackground = _defaultBrush;
            RocketLauncherBackground = _defaultBrush;
            FreezerGunBackground = _defaultBrush;

            if (typeof(TTurret) == typeof(MachineGunTurret))
            {
                MachineGunBackground = _selectedBrush;
                _selectedTurretType = typeof(MachineGunTurret);
            }
            else if (typeof(TTurret) == typeof(LaserGunTurret))
            {
                LaserGunBackground = _selectedBrush;
                _selectedTurretType = typeof(LaserGunTurret);
            }
            else if (typeof(TTurret) == typeof(RocketLauncherTurret))
            {
                RocketLauncherBackground = _selectedBrush;
                _selectedTurretType = typeof(RocketLauncherTurret);
            }
            else if (typeof(TTurret) == typeof(FreezerGunTurret))
            {
                FreezerGunBackground = _selectedBrush;
                _selectedTurretType = typeof(FreezerGunTurret);
            }
        }

        private void ExecuteExitCommand()
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void ExecuteHighScoreCommand()
        {
            HighScoreWindow scoreWindow = new HighScoreWindow();
            scoreWindow.Show();
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetValue<T>(ref T current, T value, [CallerMemberName] string propertyName = "")
        {
            current = value;
            OnPropertyChanged(propertyName);
        }

    }
}
