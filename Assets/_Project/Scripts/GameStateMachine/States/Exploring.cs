using Pixelplacement;
using UnityEngine.InputSystem;

namespace NFT_PowerUp
{
    public class Exploring : State
    {
        private GameInput _gameInput;
        
        private void OnEnable()
        {
            _gameInput = new GameInput();
            _gameInput.Exploring.Enable();
            _gameInput.Exploring.OpenMenu.performed += OnOpenMenu;
        }

        private void OnDisable()
        {
            _gameInput.Exploring.Disable();
            _gameInput.Exploring.OpenMenu.performed -= OnOpenMenu;
        }

        private void OnOpenMenu(InputAction.CallbackContext obj)
        {
            ChangeState("Menu");
        }
    }   
}
