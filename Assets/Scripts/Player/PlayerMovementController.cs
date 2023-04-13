using System;
using UnityEngine;

namespace Player
{

    public class PlayerMovementController : MonoBehaviour
    {
        private MovementDirections movementDirections;
        
        [Range(1f, 10f)] [SerializeField] private float moveSpeed = 1f;
        [Range(1f, 10f)] [SerializeField] private float jumpForce = 1f;
        private Rigidbody _rbPlayer;
        
        private Vector3 moveDirection;

        private float X_movementUnitVector;
        private float Z_movementUnitVector;
        private bool _isJumpAvailable;

        public LayerMask groundMask;
        private float _groundCheckDistance = 0.5f;

        private void Start()
        {
            _rbPlayer = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            X_movementUnitVector = Input.GetAxis("Horizontal");
            Z_movementUnitVector = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.Space))
                _isJumpAvailable = true;
            
            print(IsGrounded());
        }

        private void FixedUpdate()
        {
            var currentVelocity = new Vector3(X_movementUnitVector * moveSpeed, _rbPlayer.velocity.y,
                Z_movementUnitVector * moveSpeed);
            _rbPlayer.velocity = currentVelocity;

            if (_isJumpAvailable && IsGrounded())
            {
                _rbPlayer.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                _isJumpAvailable = false;
            }
        }

        private bool IsGrounded()
        {
            // RaycastHit hit;
            return Physics.Raycast(transform.position, Vector3.down, /*out hit,*/ 
                _groundCheckDistance, groundMask);
        }
    }
}
