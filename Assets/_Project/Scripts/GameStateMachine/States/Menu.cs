using MoralisUnity;
using MoralisUnity.Kits.AuthenticationKit;
using Pixelplacement;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using MoralisTools;

namespace NFT_PowerUp
{
    public class Menu : State
    {
        [Header("Components")]
        [SerializeField] private PlayerInputController playerInputController;
        [SerializeField] private AuthenticationKit authenticationKit;

        [Header("UI")] 
        [SerializeField] private Inventory inventory;
        [SerializeField] private TextMeshProUGUI runeAmountText;

        private string _walletAddress;
        private GameInput _gameInput;

        private async void Awake()
        {
            _walletAddress = await Web3Tools.GetWalletAddress();
        }

        private void OnEnable()
        {
            playerInputController.EnableInput(false);
            
            _gameInput = new GameInput();
            _gameInput.Menu.Enable();
            _gameInput.Menu.CloseMenu.performed += OnCloseMenu;

            if (_walletAddress is null)
            {
                Debug.Log("We need the wallet address to continue");
                return;
            }

            // TODO!!!!!!!!
            // We load all the items that we loot
            //inventory.LoadItems(_walletAddress, GameManager.GameItemContractAddress, Moralis.CurrentChain.EnumValue);
        }

        private void OnDisable()
        {
            _gameInput.Menu.CloseMenu.performed -= OnCloseMenu;
            _gameInput.Menu.Disable();
        }

        private void OnCloseMenu(InputAction.CallbackContext obj)
        {
            LastActiveState();
        }

        public void OnDisconnectPressed()
        {
            authenticationKit.Disconnect();
        }
    }   
}
