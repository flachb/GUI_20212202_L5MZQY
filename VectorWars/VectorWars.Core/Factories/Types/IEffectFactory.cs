using VectorWars.Core.Common;
using VectorWars.Core.Elements.Types;

namespace VectorWars.Core.Factories.Types
{
    public delegate IEffect CreateEffectDelegate(Point position);

    public interface IEffectFactory
    {
        CreateEffectDelegate Create { get; }
    }
}
