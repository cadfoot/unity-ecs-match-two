namespace MatchTwo
{
    interface IPieceView
    {
        bool IsAnimating { get; }
        void UpdateGroup(int group);
        void UpdateValue(int value);
        void UpdatePosition(int x, int y);
        void Release();
    }
}
