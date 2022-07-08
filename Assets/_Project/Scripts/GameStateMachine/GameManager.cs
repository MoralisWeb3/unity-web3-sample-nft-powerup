using Pixelplacement;
using UnityEngine;

namespace NFT_PowerUp
{
    public class GameManager : StateMachine
    {
        [Header("Smart Contract Data")] 
        public string contractAddress;
        public string contractAbi;
        
        [Header("Main Components")]
        public Player player;

        [HideInInspector] public InventoryItem currentPowerUp;

        
        #region UNITY_LIFCYCLE

        private void OnEnable()
        {
            player.movement.onMaxFallingTimeReached += RespawnPlayer;

            InventoryItem.onSelected += GoToConsumingPowerUp;
        }

        private void OnDisable()
        {
            player.movement.onMaxFallingTimeReached -= RespawnPlayer;
            
            InventoryItem.onSelected -= GoToConsumingPowerUp;
        }

        #endregion
        
        
        public void StartGame()
        {
            ChangeState("Exploring");
        }

        private void RespawnPlayer()
        {
            player.gameObject.SetActive(false);
            player.transform.position = player.initPos;
            player.gameObject.SetActive(true);
        }
        
        private void GoToConsumingPowerUp(InventoryItem selectedPowerUp)
        {
            // We save the current selected item (NFT PowerUp)
            currentPowerUp = selectedPowerUp;

            // We change to the state that will take care of the burning transaction
            ChangeState("ConsumingPowerUp");
        }
    }   
}
