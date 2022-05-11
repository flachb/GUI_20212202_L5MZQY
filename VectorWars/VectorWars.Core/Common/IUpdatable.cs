using System;

namespace VectorWars.Core.Common
{
    public interface IUpdatable
    {
        void Tick(TimeSpan elapsed);
    }
}
