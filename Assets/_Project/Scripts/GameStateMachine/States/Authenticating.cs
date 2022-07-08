using Pixelplacement;
using UnityEngine;

namespace NFT_PowerUp
{
    public class Authenticating : State
    {
        [SerializeField] private PlayerInputController playerInputController;

        private void OnEnable()
        {
            playerInputController.EnableInput(false);
        }
    }   
}
