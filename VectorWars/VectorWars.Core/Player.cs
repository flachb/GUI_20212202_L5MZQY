using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using VectorWars.Core.Elements.Types;

namespace VectorWars.Core
{
    public class Player : INotifyPropertyChanged
    {
        private const double ENEMY_REWARD_MODIFIER = 2.4;
        private const double TURRET_REWARD_MODIFIER = 0.8;
        private const int START_HEALTH = 100;
        private const int START_MONEY = 100;

        public event Action ZeroHealth;

        public string Name { get; }

        private int _health;
        public int Health
        {
            get => _health;
            private set
            {
                _health = value;
                OnNotifyPropertyChanged();
            }
        }

        private int _money;
        public int Money
        {
            get => _money;
            private set
            {
                _money = value;
                OnNotifyPropertyChanged();
            }
        }

        private double _score;
        public double Score
        {
            get => _score;
            private set
            {
                _score = value;
                OnNotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Player(string name)
        {
            Name = name;
            Health = START_HEALTH;
            Money = START_MONEY;
            Score = 0;
        }

        public void EnemyKilled(IEnemy enemy)
        {
            Money += enemy.Reward;
            Score += enemy.Reward * ENEMY_REWARD_MODIFIER;
        }

        public void EnemyReachedBase(IEnemy enemy)
        {
            Health -= enemy.Damage;
            if (Health <= 0)
                ZeroHealth?.Invoke();
        }

        public void BoughtTurret(ITurret turret)
        {
            Money -= turret.BuyPrice;
            Score += turret.BuyPrice * TURRET_REWARD_MODIFIER;
        }

        public void SoldTurret(ITurret turret)
        {
            Money += turret.SellPrice;
        }

        private void OnNotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
