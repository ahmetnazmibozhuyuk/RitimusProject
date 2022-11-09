using TMPro;
using UnityEngine;

namespace RitimUS.BrickBreaker.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject playAgainButton;
        [SerializeField] private GameObject quitButton;

        [SerializeField] private TextMeshProUGUI scoreText;
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
        public void SetScore(int score)
        {
            scoreText.SetText("Score: " + score);
        }
        public void RetryGame()
        {

        }
    }
}
