using System;
using UnityEngine;

namespace Player
{
    public class PlayerControllerCC : MonoBehaviour
    {
        public float speed = 6.0f;
        public float jumpForce = 8.0f;  
        public float gravity = 20.0f; 
        private Vector3 playerVelocity;
        
        private CharacterController controller;
        [SerializeField] private Transform playerCamera;
        [SerializeField] private Animator playerAnimator;
        
        private static readonly int JumpTrigger = Animator.StringToHash("Jump");
        private static readonly int IsRunningForward = Animator.StringToHash("IsRunningForward");
        private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
        
        private float horizontalMovement;
        private float verticalMovement;

        private float _groundCheckDistance = 0.1f;

        private float _turnSmoothTime = 0.1f;
        private float turnSmoothVelocity;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            var isPlayerGrounded = controller.isGrounded;
            print(isPlayerGrounded);
            playerAnimator.SetBool(IsGrounded, isPlayerGrounded);
            
            if (isPlayerGrounded && playerVelocity.y < 0)
                playerVelocity.y = 0f;
            
            horizontalMovement = Input.GetAxis("Horizontal");
            verticalMovement = Input.GetAxis("Vertical");

            var isPlayerMoving = Mathf.Abs(verticalMovement) > 0.1f || Mathf.Abs(horizontalMovement) > 0.1f;
            playerAnimator.SetBool(IsRunningForward, isPlayerMoving);
            
            var direction = new Vector3(horizontalMovement, 0f, verticalMovement).normalized;

            if (isPlayerGrounded && Input.GetButtonDown("Jump"))
            {
                playerVelocity.y = jumpForce;
                playerAnimator.SetTrigger(JumpTrigger);
            }
            
            if (direction.magnitude >= 0.1f)
            {
                var targetAngle = Mathf.Atan2(direction.x, direction.z) * 
                    Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,
                    ref turnSmoothVelocity, _turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                var moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                
                playerVelocity.y += -gravity * Time.deltaTime;
                
                controller.Move(moveDir.normalized * (speed * Time.deltaTime));
            }
            
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
}