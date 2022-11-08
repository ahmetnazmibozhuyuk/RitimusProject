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
        public void HitAction()
        {
            Debug.Log("ball hit");
        }
        private void Update()
        {
            CheckIfAlive();
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
