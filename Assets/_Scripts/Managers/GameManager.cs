using UnityEngine;
using RitimUS.Wheel;
using UnityEngine.UI;

namespace RitimUS.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private GameObject wheelObject;
        private WheelControl _wheelControl;
        public GameObject WheelObject { get { return wheelObject; } }
        public WheelControl WheelControl { get { return _wheelControl; } }

        private float _finalScore;

        private UIManager _uiManager;

        [SerializeField] private RewardType[] rewards = new RewardType[7];
        [SerializeField]private AngleLimit[] _angleLimits;

        private readonly int _segmentCount = 7;
        private readonly float _startAngle = 0;


        protected override void Awake()
        {
            base.Awake();
            _uiManager = GetComponent<UIManager>();
            _wheelControl = wheelObject.GetComponent<WheelControl>();
        }
        private void Start()
        {
            InitializeAngles();
        }
        private void InitializeAngles()
        {
            _angleLimits = new AngleLimit[rewards.Length];
            for (int i = 0; i < _angleLimits.Length; i++)
            {
                _angleLimits[i].minimumAngle = ((360 / _segmentCount) * i + _startAngle) % 360;
                _angleLimits[i].maximumAngle = ((360 / _segmentCount) * (i + 1) + _startAngle) % 360;
            }
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
        public void GetResult(float finalScore)
        {
            _finalScore = finalScore;

            for(int i = 0; i < _angleLimits.Length; i++)
            {
                if(finalScore >= _angleLimits[i].minimumAngle && finalScore <= _angleLimits[i].maximumAngle)
                {
                    Debug.Log("current angle %360 is " + _finalScore);
                    Debug.Log("reward name = " + rewards[i].RewardName);
                    _uiManager.ResultPhase();
                    return;
                }
            }



        }
    }
    [System.Serializable]
    public struct RewardType
    {
        public Image RewardImage;
        public string RewardName;
        public float RewardValue;
        [HideInInspector]public int RewardAmount;
    }
    [System.Serializable]
    public struct AngleLimit
    {
        public float minimumAngle;
        public float maximumAngle;
    }
}
