using System;
using Leopotam.Ecs;

namespace MatchTwo.Ecs
{
    sealed class BoardFillSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = default;
        private readonly Random _random = default;

        private readonly EcsFilter<Board> _boards = default;
        private readonly EcsFilter<Piece, Position> _pieces = default;

        void IEcsRunSystem.Run()
        {
            ref var board = ref _boards.Get1(0);
            board.UpdateLookup(_pieces);

            for (int x = 0; x < board.Size.X; x++)
            {
                var y = board.GetNextEmptyRow(x, board.Size.Y);
                while (y != board.Size.Y)
                {
                    _world.CreateRandomPieceAt(x, y, _random);
                    
                    board.UpdateLookup(_pieces);
                    y = board.GetNextEmptyRow(x, board.Size.Y);
                }
            }
        }
    }
}
