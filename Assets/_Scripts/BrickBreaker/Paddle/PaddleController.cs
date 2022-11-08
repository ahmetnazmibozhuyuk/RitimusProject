using RitimUS.Managers;
using UnityEngine;

namespace RitimUS.BrickBreaker
{
    public class PaddleController : MonoBehaviour
    {
        [SerializeField] private float maxControlSpeed;
        [SerializeField] private float horizontalClampLimit;


        private float _hitDownPositionx;
        private float _offsetx;


        private void Update()
        {
            SetControl();
        }
        private void FixedUpdate()
        {
            AssignMovement();
        }

        private void SetControl()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartGame();
                _hitDownPositionx = Input.mousePosition.x;
            }
            else if (Input.GetMouseButton(0))
            {
                _offsetx = Input.mousePosition.x - _hitDownPositionx;
                _hitDownPositionx = Input.mousePosition.x;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _offsetx = 0;

            }
        }
        private void AssignMovement()
        {
            transform.position = (new Vector3(
                Mathf.Clamp(transform.position.x + _offsetx * Time.deltaTime * maxControlSpeed, -horizontalClampLimit, horizontalClampLimit),
                transform.position.y,
                transform.position.z));
        }
        private void StartGame()
        {
            if(GameStateHandler.CurrentState == GameState.GameAwaitingStart)
            {
                GameStateHandler.ChangeState(GameState.GameStarted);
            }
        }

    }
}
