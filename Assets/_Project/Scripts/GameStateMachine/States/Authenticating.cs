using Pixelplacement;

namespace NFT_PowerUp
{
    public class Authenticating : State
    {
        public Player player;

        private void OnEnable()
        {
            player.input.EnableInput(false);
        }
    }   
}
