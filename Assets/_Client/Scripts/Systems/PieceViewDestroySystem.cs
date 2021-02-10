using Leopotam.Ecs;

namespace MatchTwo.Ecs
{
    sealed class PieceViewDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<Destroyed, View> _pieces = default;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _pieces)
            {
                _pieces.Get2(i).Value.Release();
                _pieces.GetEntity(i).Del<View>();
            }
        }
    }
}
