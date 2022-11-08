using RitimUS.Managers;
using UnityEngine;

namespace RitimUS.BrickBreaker
{
    [RequireComponent(typeof(Rigidbody),typeof(SphereCollider))]
    public class BreakerBall : MonoBehaviour, IPaddleHit
    {
        private readonly float _deathCheckInterval = 2;
        private float _deathCounter;

        private Rigidbody _rigidbody;

        private Vector3 _initialPosition;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _initialPosition = transform.position;
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
            GameStateHandler.OnGameAwaitingStartState += RepositionBall;
            GameStateHandler.OnGameWonState+= ()=>_rigidbody.velocity = Vector3.zero;
        }
        private void OnDisable()
        {
            GameStateHandler.OnGameStartedState -= StartBall;
            GameStateHandler.OnGameAwaitingStartState -= RepositionBall;
        }
        private void Update()
        {
            CheckIfAlive();
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<IBrickHit>() == null) return;
            IBrickHit brickHit = collision.gameObject.GetComponent<IBrickHit>();
            brickHit.HitAction();

        }
        private void CheckIfAlive()
        {
            _deathCounter += Time.deltaTime;
            if (_deathCounter >= _deathCheckInterval)
            {
                _rigidbody.AddForce(Vector2.down * 0.05f);

                _deathCounter = 0;
                if (transform.position.y < -5)
                {
                    GameStateHandler.ChangeState(GameState.GameLost);
                    _rigidbody.velocity = Vector3.zero;
                }
            }
        }

        public void HitAction(Vector3 direction)
        {
            _rigidbody.velocity = direction;
        }
    }
}
