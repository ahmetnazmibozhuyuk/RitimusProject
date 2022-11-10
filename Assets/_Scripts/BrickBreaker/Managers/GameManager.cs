using ObjectPooling;
using RitimUS.BrickBreaker.Effects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RitimUS.BrickBreaker.Managers
{
    [RequireComponent(typeof(UIManager))]
    public class GameManager : Singleton<GameManager>
    {
        public GameMode MainGameMode { get { return mainGameMode; } }
        [SerializeField] private GameMode mainGameMode;
        [SerializeField] private int singleBrickPoint = 10;
        private int _currentScore;

        [SerializeField] private int maxLives = 10;
        private int _currentLives;

        private int _currentBrickCount;

        [SerializeField] private Color[] switchColors;
        private UIManager _uiManager;
        public Color[] SwitchColors { get { return switchColors; } }
        private WaitForSeconds _particleDespawnDelay = new WaitForSeconds(1);

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

        }
        private void OnEnable()
        {
            GameStateHandler.OnGameAwaitingStartState += GameAwaitingStart;
        }
        private void OnDisable()
        {
            GameStateHandler.OnGameAwaitingStartState -= GameAwaitingStart;
        }
        private void GameAwaitingStart()
        {
            RestoreLives();
            ResetScore();
        }
        private void RestoreLives()
        {
            _currentLives = maxLives;
            _uiManager.SetLives(_currentLives);
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
        private void ResetScore()
        {
            _currentScore = 0;
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
            }
        }
        private void CheckIfWon()
        {
            if( _currentBrickCount <= 0)
            {
                GameStateHandler.ChangeState(GameState.GameWon);
            }
        }

        #region Particle Methods
        private IEnumerator Co_SpawnSplashParticle(Color colorToChange, Vector3 spawnPosition)
        {
            GameObject spawnedParticleObject = ObjectPool.Spawn(SplashParticle, spawnPosition, Quaternion.identity);
            spawnedParticleObject.GetComponent<SplashEffect>().SetColor(colorToChange);

            yield return _particleDespawnDelay;

            ObjectPool.Despawn(spawnedParticleObject);
        }
        private IEnumerator Co_SpawnParticle(GameObject particleToSpawn, Vector3 spawnPosition)
        {
            GameObject spawnedParticleObject = ObjectPool.Spawn(particleToSpawn, spawnPosition, Quaternion.identity);

            yield return _particleDespawnDelay;

            ObjectPool.Despawn(spawnedParticleObject);
        }
        public void SpawnSplash(Color colorToChange, Vector3 spawnPosition)
        {
            StartCoroutine(Co_SpawnSplashParticle(colorToChange,spawnPosition));
        }
        public void SpawnParticle(GameObject particleToSpawn, Vector3 spawnPosition)
        {
            StartCoroutine(Co_SpawnParticle(particleToSpawn, spawnPosition));
        }
        #endregion


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
    public enum GameMode { BallChange = 0, BrickChange = 1}
}
