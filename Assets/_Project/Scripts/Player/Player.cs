using UnityEngine;

namespace NFT_PowerUp
{
    public class Player : MonoBehaviour
    {
        [Header("Main Custom Components")]
        public PlayerInputController input;
        public PlayerMovement movement;
        public PlayerWalletAddress walletAddress;

        [Header("VFX")]
        public GameObject boostVFX;
        
        //Control vars
        [HideInInspector] public Vector3 initPos;


        #region UNITY_LIFECYCLE

        private void Awake()
        {
            initPos = transform.position;
        }

        private void OnEnable()
        {
            movement.onMaxFallingTimeReached += DeactivatePowerUp;
        }

        private void OnDisable()
        {
            movement.onMaxFallingTimeReached -= DeactivatePowerUp;
        }

        #endregion


        #region PRIVATE_METHODS

        public void ActivatePowerUp(float percentage)
        {
            movement.BoostMovementByPercentage(percentage);
            boostVFX.SetActive(true);
        }

        public void DeactivatePowerUp()
        {
            movement.ReturnMovementToDefault();
            boostVFX.SetActive(false);
        }

        #endregion
    }
}

