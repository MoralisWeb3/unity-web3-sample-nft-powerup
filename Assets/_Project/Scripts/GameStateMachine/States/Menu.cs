using MoralisUnity;
using MoralisUnity.Kits.AuthenticationKit;
using Pixelplacement;
using UnityEngine;
using UnityEngine.InputSystem;
using MoralisTools;

namespace NFT_PowerUp
{
    public class Menu : State
    {
        [Header("Components")]
        [SerializeField] private Inventory inventory;
        [SerializeField] private AuthenticationKit authenticationKit;

        private GameManager _gameManager;
        private GameInput _gameInput;

        private string _walletAddress;

        private async void Awake()
        {
            _gameManager = GetComponentInParent<GameManager>();
            _walletAddress = await Web3Tools.GetWalletAddress();
        }

        private void OnEnable()
        {
            _gameInput = new GameInput();
            _gameInput.Menu.Enable();
            _gameInput.Menu.CloseMenu.performed += OnCloseMenu;

            if (_walletAddress is null)
            {
                Debug.Log("We need the wallet address to continue");
                return;
            }
            
            // We load all the items that we loot
            inventory.LoadItems(_walletAddress, _gameManager.contractAddress, Moralis.CurrentChain.EnumValue);
        }

        private void OnDisable()
        {
            _gameInput.Menu.CloseMenu.performed -= OnCloseMenu;
            _gameInput.Menu.Disable();
        }

        private void OnCloseMenu(InputAction.CallbackContext obj)
        {
            Previous();
        }

        public void OnDisconnectPressed()
        {
            authenticationKit.Disconnect();
        }
    }   
}
