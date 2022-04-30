using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Types;
using VectorWars.Core.Handlers;

namespace VectorWars.Core.Elements.Bases
{
    public abstract class EffectBase : IEffect
    {
        private readonly IEnemyFinder _enemyFinder;

        public abstract TimeSpan Cooldown { get; }
        public abstract TimeSpan Lifespan { get; }
        public abstract int Damage { get; }
        public abstract float SpeedModifier { get; }
        public abstract float Range { get; } 
        public abstract Point Position { get; } 
        public abstract Vector Rotation { get; } 
        public abstract float Size { get; } 

        public event Action<IMapElement> Destroyed;

        private TimeSpan _currentCooldown = TimeSpan.Zero;
        private TimeSpan _currentLifeSpan = TimeSpan.Zero;

        public EffectBase(IEnemyFinder enemyFinder)
        {
            _enemyFinder = enemyFinder;
        }

        public void Tick(TimeSpan elapsed)
        {
            _currentCooldown -= elapsed;
            _currentLifeSpan -= elapsed;

            if (_currentCooldown > TimeSpan.Zero)
                return;

            var effectedEnemies = _enemyFinder.GetEnemies(Position, Range);
            if (!effectedEnemies.Any())
                return;

            if (_currentLifeSpan > TimeSpan.Zero)
            {
                OnDestroyed();
                return;
            }

            foreach (var item in effectedEnemies)
            {
                if (SpeedModifier > 0)
                    FreezerHitEffect(item);
                else if (Range > 0)
                    RocketHitEffect(item);
                else if (Damage < 0 && Range == 0)
                    MachineGunHitEffect(item);
                else if (Damage > 1)
                    LaserHitEffect(item);
                else
                    BaseHitByEnemyEffect(item);
            }

            _currentCooldown = Cooldown;
            _currentLifeSpan = Lifespan;

        }

        private void MachineGunHitEffect(IEnemy effectedEnemy)
        {
            effectedEnemy.Health -= Damage;
        }
        private void LaserHitEffect(IEnemy effectedEnemy)
        {
            effectedEnemy.Health -= Damage;
        }
        private void RocketHitEffect(IEnemy effectedEnemy)
        {
            effectedEnemy.Health -= Damage;
        }
        private void FreezerHitEffect(IEnemy effectedEnemy)
        {
            effectedEnemy.Health -= Damage;
            effectedEnemy.Speed += SpeedModifier;
        }
        private void BaseHitByEnemyEffect(IEnemy effectedEnemy)
        {
            //base.Health -= effectedEnemy.Damage;
        }

        protected void OnDestroyed()
        {
            Destroyed?.Invoke(this);
        }
    }
}
