using UnityEngine;
using Leopotam.Ecs;

namespace MatchTwo.Ecs
{
    sealed class CameraFocusOnBoardSystem : IEcsInitSystem
    {
        private readonly EcsFilter<Board> _boards = default;

        private readonly Camera _camera;

        public CameraFocusOnBoardSystem(Camera camera)
        {
            _camera = camera;
        }

        void IEcsInitSystem.Init()
        {
            var board = _boards.Get1(0);
            
            var position = _camera.transform.position;
            position.x = board.Size.X * .5f;
            position.y = board.Size.Y * .5f;
            _camera.transform.position = position;

            _camera.orthographicSize = Mathf.Max(board.Size.X, board.Size.Y);
        }
    }
}
