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
        [HideInInspector] public Vector3 initPos;


        #region UNITY_LIFECYCLE

        private void Awake()
        {
            initPos = transform.position;
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

        #endregion
    }
}

