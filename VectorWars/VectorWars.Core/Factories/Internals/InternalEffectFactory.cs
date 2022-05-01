using VectorWars.Core.Factories.Types;

namespace VectorWars.Core.Factories.Internals
{
    internal class InternalEffectFactory : IEffectFactory
    {
        public CreateEffectDelegate Create { get; }

        public InternalEffectFactory(CreateEffectDelegate createEffectDelegate)
        {
            Create = createEffectDelegate;
        }
    }
}
