using RitimUS.BrickBreaker.Managers;
using System.Collections;
using UnityEngine;
using ObjectPooling;
using RitimUS.BrickBreaker.Effects;

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

        private WaitForSeconds _particleDespawnDelay = new WaitForSeconds(1);

        private Vector3 _particleAwaitPosition = new Vector3(0, -5, 0);
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
            StartCoroutine(Co_SpawnParticle(GameManager.Instance.SparkParticle, transform.position));
            if (collision.gameObject.GetComponent<IBrickHit>() == null) return;
            IBrickHit brickHit = collision.gameObject.GetComponent<IBrickHit>();
            if (brickHit.ColorType != _ballColor)
            {
                AssignBallColor(brickHit.ColorType);
                StartCoroutine(Co_SpawnSplashParticle());
                return;
            }
            StartCoroutine(Co_SpawnParticle(GameManager.Instance.BrickBreakParticle, collision.transform.position));
            brickHit.HitAction();
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
        public void HitAction(Vector3 direction)
        {
            _rigidbody.velocity = direction;
        }
        #region Particle Methods
        private IEnumerator Co_SpawnSplashParticle()
        {
            GameObject spawnedParticleObject = ObjectPool.Spawn(GameManager.Instance.SplashParticle, transform.position, Quaternion.identity);
            spawnedParticleObject.GetComponent<SplashEffect>().SetColor(_ballColor);

            yield return _particleDespawnDelay;
            spawnedParticleObject.transform.position = _particleAwaitPosition;
            ObjectPool.Despawn(spawnedParticleObject);
        }
        private IEnumerator Co_SpawnParticle(GameObject particleToSpawn, Vector3 spawnPosition)
        {
            GameObject spawnedParticleObject = ObjectPool.Spawn(particleToSpawn, spawnPosition, Quaternion.identity);

            yield return _particleDespawnDelay;
            spawnedParticleObject.transform.position = _particleAwaitPosition;
            ObjectPool.Despawn(spawnedParticleObject);
        }
        #endregion
    }
}
