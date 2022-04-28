using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Types;

namespace VectorWars.Core.Elements.Bases
{
    public abstract class EnemyBase : IEnemy
    {
        private readonly Path _path;
        private int _pathTargetPoint;

        public abstract int Health { get; }
        public abstract float Speed { get; }
        public abstract int Damage { get; }
        public abstract int Reward { get; }
        public abstract Point Position { get; protected set; }
        public abstract Vector Rotation { get; protected set; }
        public abstract float Size { get; }

        public event Action<IMapElement> Destroyed;

        public EnemyBase(Path path)
        {
            _path = path;
            _pathTargetPoint = 1;
        }

        public void Tick(TimeSpan elapsed)
        {
            if(Position == _path[_pathTargetPoint])
                ++_pathTargetPoint;
            Vector distance = _path[_pathTargetPoint] - Position;
            var direction = distance.Normalize();
            Rotation = direction;

            var movement = direction * Speed * (float)elapsed.TotalSeconds;

            if(_path.Count == _pathTargetPoint)
            {
                OnDestroyed();
                var effect = CreateEffect();
            }
        }

        protected void OnDestroyed()
        {
            Destroyed?.Invoke(this);
        }

        protected abstract IEffect CreateEffect();
    }
}
