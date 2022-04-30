using System;

namespace VectorWars.Core.Elements.Types
{
    public interface IEffect : IMapElement
    {
        TimeSpan Cooldown { get; }

        TimeSpan Lifespan { get; }

        int Damage { get; }

        float SpeedModifier { get; }
    }
}
