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
            SetControl();
#if UNITY_EDITOR
            SetKeyboardControl();
#endif
        }
        private void FixedUpdate()
        {
            AssignMovement(_offsetx * Time.deltaTime * maxControlSpeed);
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
