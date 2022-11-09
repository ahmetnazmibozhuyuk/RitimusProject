using System;
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

        [SerializeField] private int maxLives = 10;
        private int _currentLives;

        private int _currentBrickCount;

        [SerializeField] private Color[] switchColors;
        private UIManager _uiManager;
        public Color[] SwitchColors { get { return switchColors; } }


        #region Particles
        public GameObject SplashParticle { get { return splashParticle; } }
        public GameObject BrickBreakParticle { get { return brickBreakParticle; } }
        public GameObject SparkParticle { get { return sparkParticle; } }
        [SerializeField] private GameObject splashParticle;
        [SerializeField] private GameObject brickBreakParticle;
        [SerializeField] private GameObject sparkParticle;
        #endregion
        protected override void Awake()
        {
            base.Awake();
            _uiManager = GetComponent<UIManager>();

        }
        private void Start()
        {
            GameStateHandler.ChangeState(GameState.GameAwaitingStart);
            _currentLives = maxLives;
        }
        public void InitializeGame(int totalBrickCount)
        {
            _currentBrickCount = totalBrickCount;
        }

        public void BrickHit()
        {
            _currentBrickCount--;
            _currentScore += singleBrickPoint;
            UpdateScore(singleBrickPoint);
            CheckIfWon();
        }
        private void UpdateScore(int scoreToAdd)
        {
            _currentScore += scoreToAdd;
            _uiManager.SetScore(_currentScore);
        }
        public void LivesLost()
        {
            if (_currentLives <= 0)
            {
                GameStateHandler.ChangeState(GameState.GameLost);
            }
            else
            {
                _currentLives--;
                _uiManager.SetLives(_currentLives);
                GameStateHandler.ChangeState(GameState.NewLifeStarted);
                Debug.Log("live lost, current live = " + _currentLives);
            }
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
            UpdateScore(0);
            GameStateHandler.ChangeState(GameState.GameAwaitingStart);
        }
        public void QuitGame()
        {
            Application.Quit();
        }

    }
}
