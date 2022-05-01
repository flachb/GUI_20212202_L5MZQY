using System;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Types;
using VectorWars.Core.Factories.Types;
using VectorWars.Core.Handlers;

namespace VectorWars.Core.Elements.Bases
{
    public abstract class ProjectileBase : IProjectile
    {
        private readonly IHandler<IEffect> _effectHandler;
        private readonly IEffectFactory _effectFactory;

        public abstract float Speed { get; }
        public IMapElement Target { get; }
        public Point Position { get; private set; }
        public Vector Rotation { get; private set; }
        public abstract float Radius { get; }

        public event Action<IMapElement> Destroyed;

        public ProjectileBase(
            IHandler<IEffect> effectHandler,
            IEffectFactory effectFactory,
            Point position,
            IMapElement target)
        {
            _effectHandler = effectHandler;
            _effectFactory = effectFactory;

            Position = position;
            Target = target;
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
                var effect = _effectFactory.Create(Target.Position);
                _effectHandler.Add(effect);

                return;
            }

            Position += movement;
        }

        protected void OnDestroyed()
        {
            Destroyed?.Invoke(this);
        }
    }
}
