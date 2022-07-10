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
        public Timer timer;
        public List<ScriptableRendererFeature> rendererFeatures;

        private GameManager _gameManager;
        private AudioSource _audioSource;

        private void Awake()
        {
            _gameManager = GetComponentInParent<GameManager>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            StartCoroutine(AutoDisable(_gameManager.currentPowerUp.boostDuration));
            
            foreach (var rendererFeature in rendererFeatures)
            {
                rendererFeature.SetActive(true);
            }
            
            _audioSource.Play();
            
            player.ActivatePowerUp(_gameManager.currentPowerUp.boostPercentage);
            timer.Init(_gameManager.currentPowerUp.boostDuration);
            player.movement.onMaxFallingTimeReached += GoToExploring;
        }

        private void OnDisable()
        {
            foreach (var rendererFeature in rendererFeatures)
            {
                rendererFeature.SetActive(false);
            }
            
            _audioSource.Stop();
            
            player.DeactivatePowerUp();
            timer.ResetTimer();
            player.movement.onMaxFallingTimeReached -= GoToExploring;
        }

        private IEnumerator AutoDisable(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            
            GoToExploring();
        }

        private void GoToExploring()
        {
            ChangeState("Exploring");
        }
    }   
}
