using UnityEngine;
using RitimUS.WheelGame.Wheel;

namespace RitimUS.WheelGame.Managers
{
    [RequireComponent(typeof(UIManager))]
    public class GameManager : Singleton<GameManager>
    {
        #region Wheel Related
        [SerializeField] private GameObject wheelObject;
        private WheelControl _wheelControl;
        public int RewardCount { get { return rewards.Length; } }

        [SerializeField] private RewardType[] rewards = new RewardType[7];

        #endregion

        private UIManager _uiManager;

        protected override void Awake()
        {
            base.Awake();
            _uiManager = GetComponent<UIManager>();
            _wheelControl = wheelObject.GetComponent<WheelControl>();
        }
        public void SpinWheel()
        {
            _wheelControl.StartSpinning();
            _uiManager.SpinningPhaseStarted();
        }
        public void RestartWheel()
        {
            _wheelControl.RestartWheel();
            _uiManager.RestartGame();
        }
        public void ResultPhase(int rewardIndex)
        {
            _uiManager.ResultPhase(rewards[rewardIndex]);
        }
    }
    [System.Serializable]
    public struct RewardType
    {
        public Sprite RewardImage;
        public string RewardName;
        public float RewardValue;
        public int RewardAmount;
    }

}
