using UnityEngine;

namespace RitimUS.BrickBreaker
{
    [RequireComponent(typeof(BoxCollider))]
    public class PaddleInteraction : MonoBehaviour
    {
        [SerializeField] private float paddleBounceBackMultiplier = 7.07f;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<IPaddleHit>() == null) return;

            IPaddleHit paddleHit = collision.gameObject.GetComponent<IPaddleHit>();
            paddleHit.HitAction((transform.position-collision.transform.position).normalized*paddleBounceBackMultiplier);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<IPaddleHit>() == null) return;

            IPaddleHit paddleHit = other.gameObject.GetComponent<IPaddleHit>();
            paddleHit.HitAction((transform.position - other.transform.position).normalized * paddleBounceBackMultiplier);
        }
    }

    public interface IPaddleHit
    {
        public void HitAction(Vector3 direction);
    }
}
