using RitimUS.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RitimUS
{
    public class BreakerUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject playAgainButton;
        [SerializeField] private GameObject quitButton;
        private void OnEnable()
        {
            GameStateHandler.OnGameAwaitingStartState += GameAwaitingStart;
            GameStateHandler.OnGameWonState += GameWon;
            GameStateHandler.OnGameLostState+=GameLost;
        }
        private void OnDisable()
        {
            GameStateHandler.OnGameAwaitingStartState -= GameAwaitingStart;
            GameStateHandler.OnGameWonState -= GameWon;
            GameStateHandler.OnGameLostState -= GameLost;
        }
        public void GameAwaitingStart()
        {
            playAgainButton.SetActive(false);
            quitButton.SetActive(false);
        }
        public void GameWon()
        {
            playAgainButton.SetActive(true);
            quitButton.SetActive(true);
        }
        public void GameLost()
        {
            playAgainButton.SetActive(true);
            quitButton.SetActive(true);
        }
        public void RetryGame()
        {

        }
    }
}
