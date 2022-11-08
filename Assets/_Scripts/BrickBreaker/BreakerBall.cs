using RitimUS.BrickBreaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RitimUS.BrickBreaker
{
    [RequireComponent(typeof(Rigidbody),typeof(SphereCollider))]
    public class BreakerBall : MonoBehaviour, IBallHit
    {
        private readonly float _deathCheckInterval = 2;
        private float _deathCounter;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            _rigidbody.velocity = new Vector3(5, 5, 0);
        }
        private void Update()
        {
            CheckIfAlive();
        }
        public void HitAction()
        {
            Debug.Log("ball hit");
        }
        private void CheckIfAlive()
        {
            _deathCounter += Time.deltaTime;
            if (_deathCounter >= _deathCheckInterval)
            {
                _deathCounter = 0;
                if (transform.position.y < -5)
                {
                    Debug.Log("died");
                }
            }



        }
    }
}
