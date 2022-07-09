using System.Collections.Generic;
using Pixelplacement;
using UnityEngine.Rendering.Universal;

namespace NFT_PowerUp
{
    public class PoweredUp : State
    {
        public Player player;
        public List<ScriptableRendererFeature> rendererFeatures;

        private void OnEnable()
        {
            foreach (var rendererFeature in rendererFeatures)
            {
                rendererFeature.SetActive(true);
            }
            
            player.ActivatePowerUp();
        }

        private void OnDisable()
        {
            foreach (var rendererFeature in rendererFeatures)
            {
                rendererFeature.SetActive(false);
            }
            
            player.DeactivatePowerUp();
        }
    }   
}
