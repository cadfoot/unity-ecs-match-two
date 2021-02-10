using Leopotam.Ecs;

namespace MatchTwo.Ecs
{
    sealed class DestroyEntitiesWithDestroyedSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Destroyed> _destroyed = default;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _destroyed)
                _destroyed.GetEntity(i).Destroy();
        }
    }
}
