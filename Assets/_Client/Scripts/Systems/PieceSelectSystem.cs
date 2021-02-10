using Leopotam.Ecs;

namespace MatchTwo.Ecs
{
    sealed class PieceSelectSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Board> _boards = default;
        private readonly EcsFilter<Input> _inputs = default;
        private readonly EcsFilter<Piece, Position> _pieces = default;

        void IEcsRunSystem.Run()
        {
            ref var board = ref _boards.Get1(0);
            board.UpdateLookup(_pieces);

            foreach (var i in _inputs)
            {
                var coordinate = _inputs.Get1(i).Coordinate;
                if (!board.PositionInBounds(coordinate) || !board.HasPieceAt(coordinate))
                    continue;
                var filterIndex = board.GetFilterIndex(coordinate);
                _pieces.GetEntity(filterIndex).Get<Selected>();
            }
        }
    }
}
