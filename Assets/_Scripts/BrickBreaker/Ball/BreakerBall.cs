using RitimUS.BrickBreaker.Managers;
using UnityEngine;

namespace RitimUS.BrickBreaker
{
    [RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
    public class BreakerBall : MonoBehaviour, IPaddleHit
    {
        private readonly float _deathCheckInterval = 2;
        private float _deathCounter;

        private Rigidbody _rigidbody;

        private Vector3 _initialPosition;

        private SpriteRenderer _spriteRenderer;
        private Color _ballColor;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _initialPosition = transform.position;
        }
        private void Start()
        {
            AssignBallColor(GameManager.Instance.SwitchColors[Random.Range(0, GameManager.Instance.SwitchColors.Length)]);
        }
        private void AssignBallColor(Color newColor)
        {
            _ballColor = newColor;
            _spriteRenderer.color = _ballColor;
        }
        private void StartBall()
        {
            _rigidbody.velocity = new Vector3(5, 5, 0);
        }
        private void RepositionBall()
        {
            transform.position = _initialPosition;
        }
        private void OnEnable()
        {
            GameStateHandler.OnGameStartedState += StartBall;
            GameStateHandler.OnNewLifeStartedState += ResetBall;
            GameStateHandler.OnGameLostState += ResetBall;
            GameStateHandler.OnGameAwaitingStartState += RepositionBall;
            GameStateHandler.OnGameWonState += () => _rigidbody.velocity = Vector3.zero;
        }
        private void OnDisable()
        {
            GameStateHandler.OnGameStartedState -= StartBall;
            GameStateHandler.OnNewLifeStartedState -= ResetBall;
            GameStateHandler.OnGameLostState -= ResetBall;
            GameStateHandler.OnGameAwaitingStartState -= RepositionBall;
        }
        private void ResetBall()
        {
            transform.position = _initialPosition;
            _rigidbody.velocity = Vector3.zero;
        }
        private void Update()
        {
            if (GameStateHandler.CurrentState != GameState.GameStarted) return;
            CheckIfAlive();
            AdditiveVelocity();
        }
        private void AdditiveVelocity()
        {
            if (_rigidbody.velocity.y > 0)
            {
                _rigidbody.AddForce(Vector3.up * 0.2f);
            }
            else
            {
                _rigidbody.AddForce(Vector3.down * 0.2f);
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            GameManager.Instance.SpawnParticle(GameManager.Instance.SparkParticle, transform.position);
            if (collision.gameObject.GetComponent<IBrickHit>() == null) return;
            IBrickHit brickHit = collision.gameObject.GetComponent<IBrickHit>();


            switch (GameManager.Instance.MainGameMode)
            {
                case GameMode.BrickChange:
                    brickHit.HitAction(_ballColor);
                    break;
                case GameMode.BallChange:
                    BallChangeHit(brickHit, collision.transform.position);
                    break;
            }
        }

        private void BallChangeHit(IBrickHit brickHit, Vector3 particleSpawnPosition)
        {
            if (brickHit.ColorType != _ballColor)
            {
                AssignBallColor(brickHit.ColorType);
                GameManager.Instance.SpawnSplash(_ballColor, transform.position);
                return;
            }

            brickHit.HitAction(_ballColor);
        }
        private void CheckIfAlive()
        {
            _deathCounter += Time.deltaTime;
            if (_deathCounter >= _deathCheckInterval)
            {
                _deathCounter = 0;
                if (transform.position.y < -5)
                {
                    GameManager.Instance.LivesLost();
                }
            }
        }
        public void PaddleHitAction(Vector3 direction)
        {
            _rigidbody.velocity = direction;
        }

    }
}
