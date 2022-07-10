using System;
using Cysharp.Threading.Tasks;
using MoralisUnity;
using Nethereum.Hex.HexTypes;
using Pixelplacement;
using TMPro;
using UnityEngine;

namespace NFT_PowerUp
{
    public class ConsumingPowerUp : State
    {
        public Inventory inventory;
        
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI statusText;

        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GetComponentInParent<GameManager>();
        }

        private void OnEnable()
        {
            ConsumePowerUp(_gameManager.currentPowerUp.myTokenId);
        }

        private async void ConsumePowerUp(string tokenId)
        {
            statusText.text = "Please confirm transaction in your wallet";
    
            var result = await ExecuteConsuming(tokenId);

            if (result is null)
            {
                statusText.text = "Transaction failed";
                
                ChangeState("Exploring");
                return;
            }

            // We tell the GameManager what we minted the item successfully
            statusText.text = "Transaction completed!";
            
            // We delete the power up that we just consumed from the inventory :)
            inventory.DeleteCurrentSelectedItem();
            
            // We go to "PoweredUp" state
            ChangeState("PoweredUp");
        }

        private async UniTask<string> ExecuteConsuming(string tokenId)
        {
            // I assume tokenId is a string just made of numbers
            var longTokenId = Convert.ToInt64(tokenId);
            
            object[] parameters = {
                longTokenId.ToString("x") // This is what the contract expects
            };

            // Set gas estimate
            HexBigInteger value = new HexBigInteger(0);
            HexBigInteger gas = new HexBigInteger(0);
            HexBigInteger gasPrice = new HexBigInteger(0);

            string resp = await Moralis.ExecuteContractFunction(_gameManager.contractAddress, _gameManager.contractAbi, "consume", parameters, value, gas, gasPrice);

            return resp;
        }
    }   
}
