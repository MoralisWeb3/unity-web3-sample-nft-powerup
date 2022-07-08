using Pixelplacement;

namespace NFT_PowerUp
{
    public class Exploring : State
    {
        public PlayerInputController playerInputController;
    
        private void OnEnable()
        {
            playerInputController.EnableInput(true);    
        }
    }   
}
