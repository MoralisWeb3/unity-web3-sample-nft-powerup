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
            
            
            // TODO ChangeState("Exploring"); // POWERED UP
        }

        private async UniTask<string> ExecuteConsuming(string tokenId)
        {
            var longTokenId = Convert.ToInt64(tokenId);
            
            object[] parameters = {
                longTokenId.ToString("x") // This is the format the contract expects //TODOOOOOO
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
