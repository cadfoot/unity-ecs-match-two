using Leopotam.Ecs;

namespace MatchTwo.Ecs
{
    sealed class PieceDestroySelectedSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Selected, InGroup> _selected = default;
        private readonly EcsFilter<InGroup, Piece> _pieces = default;
        private readonly EcsFilter<Group> _groups = default;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _selected)
            {
                var groupIndex = _selected.Get2(i).GroupIndex;
                if (!_groups.GetEntity(groupIndex).Has<Match>())
                    continue;
                DestroyPiecesOfGroup(groupIndex);
            }
        }

        private void DestroyPiecesOfGroup(int groupIndex)
        {
            foreach (var i in _pieces)
            {
                if (_pieces.Get1(i).GroupIndex != groupIndex)
                    continue;
                var entity = _pieces.GetEntity(i);
                entity.Get<Destroyed>();
            }
        }
    }
}
