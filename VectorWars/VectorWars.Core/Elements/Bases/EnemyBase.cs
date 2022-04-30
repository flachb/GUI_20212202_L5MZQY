using System;
using System.Collections.Generic;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Types;

namespace VectorWars.Core.Elements.Bases
{
    public abstract class EnemyBase : IEnemy
    {
        private readonly ICollection<IEffect> _appliedEffects;
        private readonly Path _path;
        private int _pathTargetPoint;

        public abstract int Health { get; protected set; }
        public abstract float Speed { get; protected set; }
        public abstract int Damage { get; protected set; }
        public abstract int Reward { get; }
        public abstract Point Position { get; protected set; }
        public abstract Vector Rotation { get; protected set; }
        public abstract float Radius { get; }

        public event Action<IMapElement> Destroyed;

        public EnemyBase(Path path)
        {
            _appliedEffects = new List<IEffect>();
            _path = path;
            _pathTargetPoint = 0;
            Position = _path[_pathTargetPoint];
        }

        public void AddEffect(IEffect effect)
        {
            _appliedEffects.Add(effect);
        }

        public void Tick(TimeSpan elapsed)
        {
            if(Point.Distance(Position, _path[_pathTargetPoint]) <= Radius)
            {
                ++_pathTargetPoint;

                if (_pathTargetPoint == _path.Count)
                {
                    OnDestroyed();
                    return;
                }
            }

            Vector distance = _path[_pathTargetPoint] - Position;
            var direction = distance.Normalize();
            Rotation = direction;

            var movement = direction * Speed * (float)elapsed.TotalSeconds;

            Position += movement;
        }

        protected void OnDestroyed()
        {
            Destroyed?.Invoke(this);
        }
    }
}
