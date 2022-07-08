using Pixelplacement;
using UnityEngine;

namespace NFT_PowerUp
{
    public class GameManager : StateMachine
    {
        [Header("Main Components")]
        public Player player;
        
        public void StartGame()
        {
            ChangeState("Exploring");
        }
    }   
}
