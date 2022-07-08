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

        
        #region UNITY_LIFCYCLE

        private void OnEnable()
        {
            player.movement.onMaxFallingTimeReached += RespawnPlayer;
        }

        private void OnDisable()
        {
            player.movement.onMaxFallingTimeReached -= RespawnPlayer;
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
    }   
}
