using System;
using UnityEngine;

namespace Web3_Elden_Ring
{
    public class Player : MonoBehaviour
    {
        [Header("Main Custom Components")]
        public PlayerInputController input;
        public PlayerMovement movement;
        public PlayerWalletAddress walletAddress;

        [Header("Unity Native Components")]
        public Animator animator;
        public CharacterController characterController;

        [Header("VFX")]
        public GameObject boostVFX;

        public void Death()
        {
            movement.Deactivate();
            input.EnableInput(false);
            
            animator.SetTrigger("Death");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (movement.boosted)
                {
                    movement.ReturnMovementToDefault();
                    boostVFX.SetActive(false);
                }
                else
                {
                    movement.BoostMovementByPercentage(60);
                    boostVFX.SetActive(true);
                }
            }
        }
    }
}

