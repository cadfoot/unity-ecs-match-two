using Leopotam.Ecs;

namespace MatchTwo.Ecs
{
    sealed class BoardFallSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Board> _board = default;
        private readonly EcsFilter<Piece, Position> _pieces = default; 

        void IEcsRunSystem.Run()
        {
            ref var board = ref _board.Get1(0);
            board.UpdateLookup(_pieces);
            
            for (int x = 0; x < board.Size.X; x++)
            {
                for (int y = 1; y < board.Size.Y; y++)
                {
                    if (!board.HasPieceAt(x, y))
                        continue;

                    var newY = board.GetNextEmptyRow(x, y);

                    if (newY == y)
                        continue;

                    var filterIndex = board.GetFilterIndex(x, y);
                    ref var position = ref _pieces.Get2(filterIndex).Value;
                    position.Y = newY;

                    _pieces.GetEntity(filterIndex).Get<PositionUpdated>();

                    // partially update the lookup
                    board.Lookup[board.PositionToLookupIndex(x, y)] = -1;
                    board.Lookup[board.PositionToLookupIndex(x, newY)] = filterIndex;
                }
            }
        }
    }
}
