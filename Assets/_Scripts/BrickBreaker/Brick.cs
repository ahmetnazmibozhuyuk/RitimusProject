using RitimUS.Managers;
using UnityEngine;

namespace RitimUS.BrickBreaker
{
    public class Brick : MonoBehaviour, IBrickHit
    {
        // o an ne renkse o renk particle oluþturacak
        public void HitAction()
        {
            Invoke(nameof(DestroyBrick), 0.05f);
        }
        private void DestroyBrick()
        {
            BreakerManager.Instance.BrickHit();
            Destroy(gameObject);
        }
    }
    public interface IBrickHit
    {
        public void HitAction();
    }
}
