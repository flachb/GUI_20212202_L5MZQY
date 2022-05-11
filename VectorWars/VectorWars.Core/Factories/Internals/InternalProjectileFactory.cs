using VectorWars.Core.Factories.Types;

namespace VectorWars.Core.Factories.Internals
{
    internal class InternalProjectileFactory : IProjectileFactory
    {
        public CreateProjectileDelegate Create { get; }

        public InternalProjectileFactory(CreateProjectileDelegate createProjectileDelegate)
        {
            Create = createProjectileDelegate;
        }
    }
}
