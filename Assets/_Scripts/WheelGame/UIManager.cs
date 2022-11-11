using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RitimUS.WheelGame.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject spinButtonObject;

        [SerializeField] private GameObject restartButtonObject;

        [SerializeField] private GameObject rewardParent;
        [SerializeField] private Image rewardImage;
        [SerializeField] private TextMeshProUGUI rewardText;
        [SerializeField] private Image backgroundImage;

        public void SpinningPhaseStarted()
        {
            spinButtonObject.SetActive(false);
        }
        public void ResultPhase(RewardType receivedReward)
        {
            ;
            GiveRewardCard(receivedReward);
        }
        public void RestartGame()
        {
            restartButtonObject.SetActive(false);
            spinButtonObject.SetActive(true);
            RestartRewardCard();
        }
        
        private void GiveRewardCard(RewardType receivedReward)
        {
            rewardImage.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBounce);
            rewardImage.sprite = receivedReward.RewardImage;

            backgroundImage.DOFade(0.5f, 1);
            rewardText.DOFade(1, 1).OnComplete(() => restartButtonObject.SetActive(true));
            rewardText.SetText("You earned:\n" + receivedReward.RewardAmount.ToString() + " " + receivedReward.RewardName.ToString());

        }
        private void RestartRewardCard()
        {
            rewardImage.GetComponent<RectTransform>().localScale = Vector3.zero;
            backgroundImage.DOFade(0, 0f);
            rewardText.DOFade(0, 0f);
        }
    }
}
