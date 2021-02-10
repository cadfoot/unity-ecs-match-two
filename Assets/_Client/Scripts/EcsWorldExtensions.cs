using System;
using Leopotam.Ecs;
using Leopotam.Ecs.Types;

namespace MatchTwo.Ecs
{
    static class EcsWorldExtensions
    {
        public static EcsEntity CreateRandomPieceAt(this EcsWorld world, int x, int y, Random random, int maxValue = 4)
        {
            var entity = world.NewEntity();
            entity.Get<Piece>().Value = random.Next(maxValue);
            entity.Get<Position>().Value = new Int2 (x, y);
            entity.Get<New>();

            return entity;
        }
    }
}
