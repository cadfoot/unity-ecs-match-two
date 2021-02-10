using UnityEngine;
using Leopotam.Ecs;
using MatchTwo.Ecs;
using Random = System.Random;

namespace MatchTwo
{
    [RequireComponent(typeof(PieceViewService))]
    [RequireComponent(typeof(InputService))]
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private Vector2Int _boardSize;

        [SerializeField] private bool _useCustomSeed;
        [SerializeField] private int _seed;

        private EcsWorld _world;
        private EcsSystems _systems;

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            var boardSystems = new EcsSystems(_world, "board_systems")
                .Add(new BoardInitSystem(_boardSize.x, _boardSize.y))
                .Add(new BoardFallSystem())
                .Add(new BoardFillSystem())
                .Add(new BoardShuffleSystem())
                .Add(new BoardResetGroupSystem())
                .Add(new BoardGroupSystem())
                .Add(new PieceSelectSystem())
                .Add(new PieceDestroySelectedSystem());

            _systems
                .Add(new InputSystem())

                .Add(new BoardSystemsDisableIfAnimatingSystem(_systems, "board_systems"))

                .Add(boardSystems)

                .Add(new CameraFocusOnBoardSystem(Camera.main))
                .Add(new PieceViewCreateSystem())
                .Add(new PieceViewUpdatePositionSystem())
                .Add(new PieceViewUpdateAnimatingSystem())
                .Add(new PieceViewDestroySystem())

                .Add(new DestroyEntitiesWithDestroyedSystem())

                .Inject(GetComponent<IPieceViewService>())
                .Inject(GetComponent<IInputService>())
                .Inject(_useCustomSeed ? new Random(_seed) : new Random())

                .OneFrame<New>()
                .OneFrame<Selected>()
                .OneFrame<PositionUpdated>()
                .OneFrame<Shuffled>()
                .OneFrame<Destroyed>()
                .OneFrame<Ecs.Input>()

                .Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }

        private void OnValidate()
        {
            _boardSize.x = Mathf.Max(2, _boardSize.x);
            _boardSize.y = Mathf.Max(2, _boardSize.y);
        }
    }
}
