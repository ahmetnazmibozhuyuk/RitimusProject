using UnityEngine;
using DG.Tweening;
using RitimUS.Managers;

namespace RitimUS.Wheel
{
    public class WheelControl : MonoBehaviour
    {
        private float _currentTurnAngle;
        private float _finalScore { get
            {
                 return transform.rotation.eulerAngles.z % 360;
            } }
        public void StartSpinning()
        {
            DOTween.To(() => _currentTurnAngle, x => _currentTurnAngle = x, 2000+Random.Range(0f,360f), 6+Random.Range(0f,5f)).
                SetEase(Ease.InOutCubic).
                OnUpdate(() => transform.rotation = Quaternion.Euler(0, 0, _currentTurnAngle)).OnComplete(()=>GameManager.Instance.GetResult(_finalScore));
        }
        public void RestartWheel()
        {
            transform.rotation = Quaternion.identity;
            _currentTurnAngle = 0;
        }
    }
}
