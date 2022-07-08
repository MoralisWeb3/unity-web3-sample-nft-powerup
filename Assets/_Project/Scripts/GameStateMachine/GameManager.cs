using Pixelplacement;

namespace NFT_PowerUp
{
    public class GameManager : StateMachine
    {
        public void StartGame()
        {
            ChangeState("Exploring");
        }
    }   
}
