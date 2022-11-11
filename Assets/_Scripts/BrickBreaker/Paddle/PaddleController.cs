using UnityEngine;

namespace RitimUS.BrickBreaker
{
    public class PaddleController : MonoBehaviour
    {
        [SerializeField] private float maxControlSpeed;
        [SerializeField] private float horizontalClampLimit;

        [SerializeField]private float keyboardSpeed = 12;

        private float _hitDownPositionx;
        private float _offsetx;

        #region Keyboard Inputs
        private bool _leftInput { get { return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A); } }
        private bool _rightInput { get { return Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D); ; } }
        #endregion


        private void Update()
        {
            SetTouchControl();

#if UNITY_EDITOR
            SetKeyboardControl();
#endif
        }
        private void FixedUpdate()
        {
            AssignMovement(_offsetx * Time.deltaTime * maxControlSpeed);
        }

        private void SetTouchControl()
        {
            if (Input.touchCount <= 0) return;

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                StartGame();
                _hitDownPositionx = touch.position.x;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                _offsetx = touch.position.x - _hitDownPositionx;
                _hitDownPositionx = touch.position.x;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                _offsetx = 0;
            }
        }
#if UNITY_EDITOR
        private void SetKeyboardControl()
        {
            if (_leftInput)
            {
                StartGame();
                AssignMovement( -Time.deltaTime * keyboardSpeed);
                return;
            }
            if (_rightInput)
            {
                StartGame();
                AssignMovement(Time.deltaTime * keyboardSpeed);
                return;
            }
        }
#endif
        private void AssignMovement(float xDisplacement)
        {
            transform.position = (new Vector3(
                Mathf.Clamp(transform.position.x + xDisplacement, -horizontalClampLimit, horizontalClampLimit),
                transform.position.y,
                transform.position.z));
        }
        private void StartGame()
        {
            if (GameStateHandler.CurrentState == GameState.GameAwaitingStart || GameStateHandler.CurrentState == GameState.NewLifeStarted)
            {
                GameStateHandler.ChangeState(GameState.GameStarted);
            }
        }

    }
}
