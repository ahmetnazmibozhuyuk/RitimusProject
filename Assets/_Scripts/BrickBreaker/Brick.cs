using RitimUS.Managers;
using UnityEngine;

namespace RitimUS.BrickBreaker
{
    public class Brick : MonoBehaviour, IBrickHit
    {
        public Color ColorType { get; set; }
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer= GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
            AssignBallColor(BreakerManager.Instance.SwitchColors[Random.Range(0, BreakerManager.Instance.SwitchColors.Length)]);
        }
        private void AssignBallColor(Color newColor)
        {
            ColorType = newColor;
            _spriteRenderer.color = ColorType;
        }

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
        public Color ColorType { get; set; }
        public void HitAction();
    }

    
}
