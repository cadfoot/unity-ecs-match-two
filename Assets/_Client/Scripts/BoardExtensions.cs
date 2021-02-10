using Leopotam.Ecs;
using Leopotam.Ecs.Types;

namespace MatchTwo.Ecs
{
    static class BoardExtensions
    {
        public static void InitLookup(ref this Board board)
        {
            board.Lookup = new int[board.Size.X * board.Size.Y];
        }

        public static void UpdateLookup(this Board board, EcsFilter<Piece, Position> pieces)
        {
            for (int i = 0; i < board.Lookup.Length; i++)
                board.Lookup[i] = -1;

            foreach (var i in pieces)
            {
                var position = pieces.Get2(i).Value;
                board.Lookup[board.PositionToLookupIndex(position.X, position.Y)] = i;
            }
        }

        public static bool HasPieceAt(this Board board, int x, int y)
        {
            return board.GetFilterIndex(x, y) != -1;
        }

        public static bool HasPieceAt(this Board board, Int2 position)
        {
            return board.HasPieceAt(position.X, position.Y);
        }

        public static int GetFilterIndex(this Board board, int x, int y)
        {
            return board.Lookup[board.PositionToLookupIndex(x, y)];
        }

        public static int GetFilterIndex(this Board board, Int2 position)
        {
            return board.GetFilterIndex(position.X, position.Y);
        }

        public static int PositionToLookupIndex(this Board board, int x, int y)
        {
            return board.Size.Y * x + y;
        }

        public static int PositionToLookupIndex(this Board board, Int2 position)
        {
            return board.PositionToLookupIndex(position);
        }

        public static bool PositionInBounds(this Board board, int x, int y)
        {
            return x >= 0 && x < board.Size.X && y >= 0 && y < board.Size.Y;
        }

        public static bool PositionInBounds(this Board board, Int2 position)
        {
            return board.PositionInBounds(position.X, position.Y);
        }

        public static int GetNextEmptyRow(this Board board, int x, int y)
        {
            y--;

            while (y >= 0 && !board.HasPieceAt(x, y))
                y--;

            return y + 1;
        }
    }
}
