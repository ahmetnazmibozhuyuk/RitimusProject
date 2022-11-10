using RitimUS.BrickBreaker.Managers;
using UnityEngine;

namespace RitimUS.BrickBreaker
{
    public class Brick : MonoBehaviour, IBrickHit
    {
        public Color ColorType { get; set; }
        private SpriteRenderer _spriteRenderer;

        private WaitForSeconds _particleDespawnDelay = new WaitForSeconds(1);
        private void Awake()
        {
            _spriteRenderer= GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
            AssignBrickColor(GameManager.Instance.SwitchColors[Random.Range(0, GameManager.Instance.SwitchColors.Length)]);
        }
        private void AssignBrickColor(Color newColor)
        {
            ColorType = newColor;
            _spriteRenderer.color = ColorType;
        }

        public void HitAction(Color hitColor)
        {
            switch (GameManager.Instance.MainGameMode)
            {
                case GameMode.BrickChange:
                    ColorGetHit(hitColor);
                    break;
                case GameMode.BallChange:
                    Invoke(nameof(DestroyBrick), 0.05f);
                    break;
            }
        }
        private void ColorGetHit(Color hitColor)
        {
            if (ColorType != hitColor)
            {
                AssignBrickColor(hitColor);
                GameManager.Instance.SpawnSplash(ColorType, transform.position);
            }
            else
            {
                Invoke(nameof(DestroyBrick), 0.05f);
            }
        }
        private void DestroyBrick()
        {
            GameManager.Instance.BrickHit();
            GameManager.Instance.SpawnParticle(GameManager.Instance.BrickBreakParticle, transform.position);
            Destroy(gameObject);
        }

    }
    public interface IBrickHit
    {
        public Color ColorType { get; set; }
        public void HitAction(Color hitColor);
    }

    
}
