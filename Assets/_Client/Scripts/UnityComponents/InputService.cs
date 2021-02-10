using UnityEngine;
using Leopotam.Ecs.Types;

namespace MatchTwo
{
    public sealed class InputService : MonoBehaviour, IInputService
    {
        [SerializeField] private Camera _camera;

        bool IInputService.GetClickedCoordinate(out Int2 coord)
        {
            coord = new Int2();
            if (!Input.GetMouseButtonUp(0))
                return false;
            var worldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            coord.Set(
                Mathf.FloorToInt(worldPosition.x),
                Mathf.FloorToInt(worldPosition.y)
            );
            return true;
        }
    }
}
