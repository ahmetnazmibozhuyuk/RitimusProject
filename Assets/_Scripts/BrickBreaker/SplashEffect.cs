using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RitimUS
{
    public class SplashEffect : MonoBehaviour
    {
        private ParticleSystem.MainModule mainModule;
        private ParticleSystem.MainModule childMainModule;
        private ParticleSystem.MainModule grandChildMainModule;
        private void Awake()
        {
            mainModule= GetComponent<ParticleSystem>().main;
            childMainModule = transform.GetChild(0).GetComponent<ParticleSystem>().main;
            grandChildMainModule = transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().main;
        }
        public void SetColor(Color newColor)
        {
            mainModule.startColor = newColor;
            childMainModule.startColor = newColor;
            grandChildMainModule.startColor = newColor;
        }


    }
}
