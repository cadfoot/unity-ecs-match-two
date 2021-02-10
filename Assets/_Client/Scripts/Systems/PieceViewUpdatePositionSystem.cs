using Leopotam.Ecs;

namespace MatchTwo.Ecs
{
    sealed class PieceViewUpdatePositionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<View, Position, PositionUpdated> _pieces = default;
        private readonly EcsFilter<View, Position> _pieces2 = default;


        void IEcsRunSystem.Run()
        {
            foreach (var i in _pieces)
            {
                var view = _pieces.Get1(i).Value;
                var position = _pieces.Get2(i).Value;

                view.UpdatePosition(position.X, position.Y);
            }

            foreach (var i in _pieces2)
            {
                var view = _pieces2.Get1(i).Value;
                var entity = _pieces2.GetEntity(i);
                var group = entity.Has<InGroup>() ? entity.Get<InGroup>().GroupIndex : -1;

                view.UpdateGroup(group);
            }
        }
    }
}
