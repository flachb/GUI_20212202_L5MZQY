using System;

namespace VectorWars.Core.Elements.Types
{
    public interface ITurret : IMapElement
    {
        TimeSpan Cooldown { get; }

        float Range { get; }

        int BuyPrice { get; }

        int SellPrice { get; }
    }
}
