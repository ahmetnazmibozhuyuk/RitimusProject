using UnityEngine;
using DG.Tweening;

namespace RitimUS.UI
{
    public class PopOpen : MonoBehaviour
    {
        [SerializeField] private Vector3 enlargedSize = Vector3.one;
        [SerializeField] private float openDelay = 0.5f;

        private Tweener _popTween;
        private void Awake()
        {
            transform.localScale = Vector3.zero;
        }
        private void OnEnable()
        {
            _popTween = transform.DOScale(enlargedSize, 0.5f).SetEase(Ease.OutBounce);
        }
        private void OnDisable()
        {
            _popTween.Kill();
            transform.localScale = Vector3.zero;
        }
    }
}
