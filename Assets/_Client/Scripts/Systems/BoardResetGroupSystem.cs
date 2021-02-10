using Leopotam.Ecs;

namespace MatchTwo.Ecs
{
    sealed class BoardResetGroupSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Group> _groups = default;
        private readonly EcsFilter<InGroup> _inGroups = default;

        private readonly EcsFilter<PositionUpdated> _moved = default;
        private readonly EcsFilter<New> _new = default;

        void IEcsRunSystem.Run()
        {
            if (_new.IsEmpty() && _moved.IsEmpty())
                return;
            
            foreach (var i in _groups)
                _groups.GetEntity(i).Destroy();
            
            foreach (var i in _inGroups)
                _inGroups.GetEntity(i).Del<InGroup>();
        }
    }
}
