using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace NFT_PowerUp
{
    public class PoweredUp : State
    {
        public Player player;
        public List<ScriptableRendererFeature> rendererFeatures;

        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GetComponentInParent<GameManager>();
        }

        private void OnEnable()
        {
            foreach (var rendererFeature in rendererFeatures)
            {
                rendererFeature.SetActive(true);
            }
            
            player.ActivatePowerUp(_gameManager.currentPowerUp.boostPercentage);
            
            StartCoroutine(AutoDisable(_gameManager.currentPowerUp.boostDuration));
        }

        private void OnDisable()
        {
            foreach (var rendererFeature in rendererFeatures)
            {
                rendererFeature.SetActive(false);
            }
            
            player.DeactivatePowerUp();
        }

        private IEnumerator AutoDisable(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            
            ChangeState("Exploring");
        }
    }   
}
