using System;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Types;
using VectorWars.Core.Handlers;

namespace VectorWars.Core.Elements.Bases
{
    public abstract class ProjectileBase : IProjectile
    {
        private readonly IHandler<IEffect> _effectHandler;

        public abstract float Speed { get; }
        public abstract IMapElement Target { get; }
        public Point Position { get; protected set; }
        public Vector Rotation { get; protected set; }
        public abstract float Radius { get;}

        public event Action<IMapElement> Destroyed;

        public ProjectileBase(
            IHandler<IEffect> effectHandler)
        {
            _effectHandler = effectHandler;
        }

        public void Tick(TimeSpan elapsed)
        {
            Vector distance = Target.Position - Position;
            var direction = distance.Normalize();
            Rotation = direction;

            var movement = direction * Speed * (float)elapsed.TotalSeconds;

            if (movement.Magnitude + Radius + Target.Radius >= distance.Magnitude)
            {
                OnDestroyed();
                var effect = CreateEffect();
                _effectHandler.Add(effect);

                return;
            }

            Position += movement;
        }

        protected void OnDestroyed()
        {
            Destroyed?.Invoke(this);
        }

        protected abstract IEffect CreateEffect();
    }
}
