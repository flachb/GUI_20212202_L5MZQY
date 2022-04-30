using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorWars.Core.Elements.Types;

namespace VectorWars.Core
{
    public class Player
    {
        private const double ENEMY_REWARD_MODIFIER = 2.4;
        private const double TURRET_REWARD_MODIFIER = 0.8;

        public string Name { get; }
        public int Health { get; private set; }
        public int Money { get; private set; }
        public double Score { get; private set; }

        public Player(string name)
        {
            Name = name;
            Health = 100;
            Money = 100;
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
    }
}
