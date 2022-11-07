using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RitimUS.Wheel
{
    public class WheelControl : MonoBehaviour
    {
        [SerializeField] private RewardType[] rewards = new RewardType[7];



    }
    public struct RewardType
    {
        public Image RewardImage;
        public string RewardName;
        public float RewardValue;
        public int RewardAmount;
    }
}
