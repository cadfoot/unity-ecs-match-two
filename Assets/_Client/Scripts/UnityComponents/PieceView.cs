using UnityEngine;
using DG.Tweening;

namespace MatchTwo
{
    sealed class PieceView : MonoBehaviour, IPieceView
    {
        public SpriteRenderer SpriteRenderer;
        private int _group = -1;

        public bool IsAnimating => DOTween.IsTweening(transform);

        public void UpdateGroup(int group)
        {
            _group = group;
        }

        public void UpdateValue(int value)
        {   
        }

        public void UpdatePosition(int x, int y)
        {
            DOTween.Kill(transform);
            transform.DOLocalMove(new Vector2(x, y), 5f)
                .SetSpeedBased(true)
                .SetEase(Ease.OutBounce);
        }

        public void Release()
        {
            SpriteRenderer.DOFade(0f, .3f);
            SpriteRenderer.transform.DOScale(Vector3.one * 1.5f, .3f)
                .OnComplete(() => Destroy(gameObject));
        }

        private void OnDrawGizmos()
        {
            UnityEditor.Handles.color = Color.black;
            UnityEditor.Handles.Label(transform.position + Vector3.up * .5f, _group.ToString());
        }
    }
}
