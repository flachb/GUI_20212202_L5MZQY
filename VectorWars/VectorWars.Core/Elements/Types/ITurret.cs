using System;

namespace VectorWars.Core.Elements.Types
{
    public interface ITurret : IMapElement
    {
        TimeSpan Cooldown { get; }

        float Radius { get; }

        int BuyPrice { get; }

        int SellPrice { get; }
    }
}
