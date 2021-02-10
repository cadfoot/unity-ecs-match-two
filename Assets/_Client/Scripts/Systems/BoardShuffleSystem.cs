using Leopotam.Ecs;
using System;

namespace MatchTwo.Ecs
{
    sealed class BoardShuffleSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Board> _boards = default;
        private readonly EcsFilter<Piece, Position>.Exclude<Shuffled> _pieces = default;
        private readonly EcsFilter<Group, Match> _matches = default;

        private readonly Random _random = new Random();

        void IEcsRunSystem.Run()
        {
            if (!_matches.IsEmpty())
                return;
            
            ref var board = ref _boards.Get1(0);
            board.UpdateLookup(_pieces);

            while (_pieces.GetEntitiesCount() >= 2)
            {
                var first = _pieces.GetEntity(_random.Next(0, _pieces.GetEntitiesCount()));
                var second = _pieces.GetEntity(_random.Next(0, _pieces.GetEntitiesCount()));

                ref var firstPos = ref first.Get<Position>().Value;
                ref var secondPos = ref second.Get<Position>().Value;

                (firstPos, secondPos) = (secondPos, firstPos);                
                
                first.Get<Shuffled>();
                first.Get<PositionUpdated>();
                
                second.Get<Shuffled>();
                second.Get<PositionUpdated>();
            }
        }
    }
}
