using UnityEngine;

namespace Player
{
    public class PlayerRotationController : MonoBehaviour
    {
        [Range(1f, 10f)] [SerializeField] private float rotationSpeed = 1f;
    
        private void Update()
        {
            RotatePlayerHorizontally();
        }

        private void RotatePlayerHorizontally()
        {
            var horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
            transform.Rotate(Vector3.up, horizontalRotation);
        }
    }

}