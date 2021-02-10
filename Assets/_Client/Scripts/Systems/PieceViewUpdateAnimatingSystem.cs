using Leopotam.Ecs;

namespace MatchTwo.Ecs
{
    sealed class PieceViewUpdateAnimatingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<View>.Exclude<Destroyed> _pieces = default;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _pieces)
            {
                var view = _pieces.Get1(i).Value;
                var entity = _pieces.GetEntity(i);
                if (view.IsAnimating)
                    entity.Get<Animating>();
                else
                    entity.Del<Animating>();
            }
        }
    }
}
