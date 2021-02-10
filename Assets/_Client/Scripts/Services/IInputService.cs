using Leopotam.Ecs.Types;

namespace MatchTwo
{
    interface IInputService
    {
        bool GetClickedCoordinate(out Int2 coord);
    }
}
