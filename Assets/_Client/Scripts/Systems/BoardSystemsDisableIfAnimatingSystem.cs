using Leopotam.Ecs;

namespace MatchTwo.Ecs
{
    sealed class BoardSystemsDisableIfAnimatingSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<Animating> _animating = default;

        private readonly EcsSystems _rootSystems;
        private readonly string _boardSystemsName;

        private int _boardSystemsIndex;

        public BoardSystemsDisableIfAnimatingSystem(EcsSystems rootSystems, string boardSystemsName)
        {
            _rootSystems = rootSystems;
            _boardSystemsName = boardSystemsName;       
        }

        void IEcsPreInitSystem.PreInit()
        {
            _boardSystemsIndex = _rootSystems.GetNamedRunSystem(_boardSystemsName);
        }

        void IEcsRunSystem.Run()
        {
            _rootSystems.SetRunSystemState(_boardSystemsIndex, _animating.IsEmpty());
        }
    }
}
