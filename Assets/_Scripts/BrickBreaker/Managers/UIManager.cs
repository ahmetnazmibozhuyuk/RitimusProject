using TMPro;
using UnityEngine;

namespace RitimUS.BrickBreaker.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject playAgainButton;
        [SerializeField] private GameObject quitButton;

        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI livesText;

        [SerializeField] private GameObject wonText;
        [SerializeField] private GameObject lostText;
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
            wonText.SetActive(false);
            lostText.SetActive(false);
            playAgainButton.SetActive(false);
            quitButton.SetActive(false);
        }
        public void GameWon()
        {
            wonText.SetActive(true);
            playAgainButton.SetActive(true);
            quitButton.SetActive(true);
        }
        public void GameLost()
        {
            lostText.SetActive(true);
            playAgainButton.SetActive(true);
            quitButton.SetActive(true);
        }
        public void SetScore(int score)
        {
            scoreText.SetText("Score: " + score);
        }
        public void SetLives(int livesCount)
        {
            livesText.SetText("Lives: " + livesCount);
        }
    }
}
