using Leopotam.Ecs;

namespace MatchTwo.Ecs
{
    sealed class PieceViewCreateSystem : IEcsRunSystem
    {
        private readonly IPieceViewService _viewService = default;

        private readonly EcsFilter<Board> _boards = default;
        private readonly EcsFilter<Piece, Position>.Exclude<View> _pieces = default;



        void IEcsRunSystem.Run()
        {
            if (_pieces.IsEmpty())
                return;
            
            var board = _boards.Get1(0);

            var offsets = new int[board.Size.X];

            foreach (var i in _pieces)
                offsets[_pieces.Get2(i).Value.X]++;

            foreach (var i in _pieces)
            {
                var value = _pieces.Get1(i).Value;
                var position = _pieces.Get2(i).Value;

                var view = _viewService.CreatePieceView(value, position.X, position.Y + offsets[position.X]);
                view.UpdatePosition(position.X, position.Y);

                _pieces.GetEntity(i).Get<View>().Value = view;
            }
        }
    }
}
