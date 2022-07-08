using UnityEngine;

namespace NFT_PowerUp
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
        
        //Control vars
        private Vector3 _initPos;

        private void Awake()
        {
            _initPos = transform.position;
        }

        private void OnEnable()
        {
            movement.OnMaxFallingTimeReached += Respawn;
        }

        private void OnDisable()
        {
            movement.OnMaxFallingTimeReached -= Respawn;
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
        
        public void Respawn()
        {
            input.EnableInput(false);
            movement.enabled = false;
            transform.SetPositionAndRotation(_initPos, Quaternion.identity);
            //movement.enabled = true;
        }

        public void Death()
        {
            movement.Deactivate();
            input.EnableInput(false);
            
            animator.SetTrigger("Death");
        }
    }
}

