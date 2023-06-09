using Cinemachine;
using UnityEngine;

namespace Player
{
    public class PlayerRotationController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        
        private Vector3 _cameraRotationVector;
        public static Transform cameraTransform;

        private void Start()
        {
            cameraTransform = virtualCamera.transform;
        }

        private void FixedUpdate()
        {
            if (!PlayerMovementController.IsPlayerMovable) return;
            
            var zMovementUnitVector = PlayerMovementController.Z_movementUnitVector;
            var xMovementUnitVector = PlayerMovementController.X_movementUnitVector;
            
            if (zMovementUnitVector != 0f || xMovementUnitVector != 0f)
            {
                // Calculate the desired player rotation based on camera rotation and input
                var cameraRotation = Quaternion.Euler(0, _cameraRotationVector.y, 0);
                var playerRotationVector = cameraRotation * new Vector3(xMovementUnitVector, 0, zMovementUnitVector);
                var playerRotation = Quaternion.LookRotation(playerRotationVector);

                // Apply the desired player rotation
                transform.rotation = playerRotation;
            }
        }

        private void LateUpdate()
        {
            cameraTransform = virtualCamera.transform;
            _cameraRotationVector = cameraTransform.eulerAngles;
        }
    }
}