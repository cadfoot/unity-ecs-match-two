using Leopotam.Ecs.Types;

namespace MatchTwo.Ecs
{
    struct Board
    {
        public Int2 Size;
        // having any game state in components is bad, but we need this for faster entity access later.
        // don't want to create an injectable service just for this
        internal int[] Lookup;
    }
}
