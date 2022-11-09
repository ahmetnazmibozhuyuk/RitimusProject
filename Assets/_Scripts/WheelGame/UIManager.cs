using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RitimUS.WheelGame.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject spinButtonObject;
        private Button _spinButton;

        [SerializeField] private GameObject restartButtonObject;
        private Button _restartButton;

        private void Awake()
        {
            _spinButton = spinButtonObject.GetComponent<Button>();
            _restartButton = restartButtonObject.GetComponent<Button>();
        }
        public void SpinningPhaseStarted()
        {
            spinButtonObject.SetActive(false);
        }
        public void ResultPhase()
        {
            restartButtonObject.SetActive(true);
        }
        public void RestartGame()
        {
            restartButtonObject.SetActive(false);
            spinButtonObject.SetActive(true);
        }
    }
}
