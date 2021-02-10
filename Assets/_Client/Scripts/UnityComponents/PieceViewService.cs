using UnityEngine;

namespace MatchTwo
{
    class PieceViewService : MonoBehaviour, IPieceViewService
    {
        [SerializeField] private Transform _piecesRoot;

        [SerializeField] private PieceView _pieceViewPrefab;
        [SerializeField] private Sprite[] _sprites;

        public IPieceView CreatePieceView(int value, int x, int y)
        {
            var position = new Vector2(x, y);
            var rotation = Quaternion.identity;

            var pieceView = Instantiate(_pieceViewPrefab, position, rotation, _piecesRoot);
            pieceView.SpriteRenderer.sprite = _sprites[value];

            return pieceView;
        }   
    }
}
