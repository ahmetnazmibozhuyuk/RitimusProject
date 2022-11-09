using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RitimUS.BrickBreaker.Managers
{
    [RequireComponent(typeof(UIManager))]
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private int singleBrickPoint = 10;
        private int _currentScore;

        private int _currentBrickCount;

        [SerializeField] private Color[] switchColors;
        public Color[] SwitchColors { get { return switchColors; } }

        public GameObject SplashParticle { get { return splashParticle; } }
        public GameObject BrickBreakParticle { get { return brickBreakParticle; } }
        public GameObject SparkParticle { get { return sparkParticle; } }
        [SerializeField] private GameObject splashParticle;
        [SerializeField] private GameObject brickBreakParticle;
        [SerializeField] private GameObject sparkParticle;
        protected override void Awake()
        {
            base.Awake();
        }
        private void Start()
        {
            GameStateHandler.ChangeState(GameState.GameAwaitingStart);
        }
        public void InitializeGame(int totalBrickCount)
        {
            _currentBrickCount = totalBrickCount;

        }
        private void OnEnable()
        {
            GameStateHandler.OnGameStartedState += StartGame;
            GameStateHandler.OnGameWonState += GameWon;
            GameStateHandler.OnGameLostState += GameLost;
        }
        private void OnDisable()
        {
            GameStateHandler.OnGameStartedState -= StartGame;
            GameStateHandler.OnGameWonState -= GameWon;
            GameStateHandler.OnGameLostState -= GameLost;
        }
        public void StartGame()
        {
            Debug.Log("game started");
        }
        public void GameWon()
        {
            Debug.Log("you won");
        }
        public void GameLost()
        {
            Debug.Log("you lost");
        }

        public void BrickHit()
        {
            _currentBrickCount--;
            _currentScore += singleBrickPoint;
            CheckIfWon();
        }
        private void CheckIfWon()
        {
            if( _currentBrickCount <= 0)
            {
                GameStateHandler.ChangeState(GameState.GameWon);
            }
        }
        public void RestartGame()
        {
            GameStateHandler.ChangeState(GameState.GameAwaitingStart);
        }

    }
}