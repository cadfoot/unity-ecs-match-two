using Leopotam.Ecs;
using Leopotam.Ecs.Types;

namespace MatchTwo.Ecs
{
    sealed class BoardGroupSystem : IEcsRunSystem
    {
        private readonly Int2[] _adjacents = new[] {
            new Int2(+1, +0), new Int2(-1, +0), new Int2(+0, +1), new Int2(+0, -1)
        };

        private readonly EcsWorld _world = default;

        private readonly EcsFilter<Board> _boards = default;
        private readonly EcsFilter<Piece, Position> _allPieces = default;
        private readonly EcsFilter<Piece, Position>.Exclude<InGroup> _nonGrouped = default;
        private readonly EcsFilter<Piece, Position, GroupingTarget> _groupingTargets = default;
        private readonly EcsFilter<Group> _groups = default;

        void IEcsRunSystem.Run()
        {
            if (_nonGrouped.IsEmpty())
                return;

            ref var board = ref _boards.Get1(0);
            board.UpdateLookup(_allPieces);

            while (!_nonGrouped.IsEmpty())
            {
                var groupEntity = _world.NewEntity();
                ref var group = ref groupEntity.Get<Group>();

                var groupIndex = _groups.GetEntitiesCount() - 1;

                var nextGroupValue = _nonGrouped.Get1(0).Value;
                var nextGroupingTarget = _nonGrouped.GetEntity(0);
                nextGroupingTarget.Get<GroupingTarget>();

                while (!_groupingTargets.IsEmpty())
                {
                    foreach (var i in _groupingTargets)
                    {
                        var position = _groupingTargets.Get2(i).Value;
                        foreach (var offset in _adjacents)
                        {
                            var neighbourPosition = position + offset;
                            if (!board.PositionInBounds(neighbourPosition))
                                continue;

                            if (!board.HasPieceAt(neighbourPosition))
                                continue;
                            
                            var filterIndex = board.GetFilterIndex(neighbourPosition);
                            if (_allPieces.Get1(filterIndex).Value != nextGroupValue)
                                continue;
                            
                            var neighbourEntity = _allPieces.GetEntity(filterIndex);
                            if (neighbourEntity.Has<InGroup>())
                                continue;
                            
                            neighbourEntity.Get<GroupingTarget>();
                        }

                        var entity = _groupingTargets.GetEntity(i);
                        entity.Get<InGroup>().GroupIndex = groupIndex;
                        entity.Del<GroupingTarget>();
                        
                        group.Count++;
                        if (group.Count == 2)
                            groupEntity.Get<Match>();
                    }
                }
            }
        }
    }
}