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


        #region STATE_MACHINE

        public void OnStateEnteredHandler(GameObject stateEntered)
        {
            switch (stateEntered.name)
            {
                case "Authenticating":
                    player.input.EnableInput(false);
                    break;
                
                case "Exploring":
                    player.input.EnableInput(true);
                    break;
                
                case "Menu":
                    player.input.EnableInput(false);
                    break;
                
                case "ConsumingPowerUp":
                    player.input.EnableInput(false);
                    break;
                
                case "PoweredUp":
                    player.input.EnableInput(true);
                    break;
            }
        }
        
        public void OnStateExitedHandler(GameObject stateExited)
        {
            switch (stateExited.name)
            {
                case "Authenticating":
                    break;
                
                case "Exploring":
                    break;
                
                case "Menu":
                    break;
                
                case "ConsumingPowerUp":
                    break;
                
                case "PoweredUp":
                    break;
            }
        }

        #endregion
        
        
        public void StartGame()
        {
            ChangeState("Exploring");
        }
        
        public void GoToAuthenticating()
        {
            ChangeState("Authenticating");
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
