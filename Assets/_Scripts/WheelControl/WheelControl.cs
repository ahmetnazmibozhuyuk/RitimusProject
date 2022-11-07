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
            DOTween.To(() => _currentTurnAngle, x => _currentTurnAngle = x, 2000, 6).
                SetEase(Ease.InOutCubic).
                OnUpdate(() => transform.rotation = Quaternion.Euler(0, 0, _currentTurnAngle)).OnComplete(()=>GameManager.Instance.GetResult(_finalScore));
        }
    }
}
