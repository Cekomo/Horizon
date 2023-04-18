using UnityEngine;

namespace Player
{
    public class PlayerRotationController : MonoBehaviour
    {
        // [Range(1f, 10f)] [SerializeField] private float rotationSpeed = 1f;
        
        private bool isRotationLeft;
        private bool isDirectionChangeable;
        
        private void Update()
        {
            GetPlayerRotation();
            if (!Input.GetKey(KeyCode.W)) return;
            RotatePlayerHorizontally();
        }

        private void GetPlayerRotation()
        {
            if (!(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))) return;
                
            isDirectionChangeable = true;
            
            if (Input.GetKey(KeyCode.D))
                isRotationLeft = false;
            else if (Input.GetKey(KeyCode.A))
                isRotationLeft = true;
        }
        
        private void RotatePlayerHorizontally()
        {
            if (!isDirectionChangeable) return;

            if (isRotationLeft)
                transform.Rotate(Vector3.up, -90.0f, Space.Self);
            else
                transform.Rotate(Vector3.up, 90.0f, Space.Self);

            isDirectionChangeable = false;

            // var horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
            // transform.Rotate(Vector3.up, horizontalRotation);
        }
    }
}