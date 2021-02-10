using System;
using Leopotam.Ecs;
using Leopotam.Ecs.Types;

namespace MatchTwo.Ecs
{
    sealed class BoardInitSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = default;
        private readonly Random _random = default;

        private readonly Int2 _size;

        public BoardInitSystem(int width, int height)
        {
            _size = new Int2(width, height);
        }

        void IEcsInitSystem.Init()
        {
            ref var board = ref _world.NewEntity().Get<Board>();
            board.Size = _size;
            board.InitLookup();

            for (int x = 0; x < _size.X; x++)
                for (int y = 0; y < _size.Y; y++)
                    _world.CreateRandomPieceAt(x, y, _random);
        }
    }
}
