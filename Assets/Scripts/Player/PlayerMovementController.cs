using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Animator PlayerAnimator;
        
        private MovementDirections movementDirections;
        
        [Range(1f, 10f)] [SerializeField] private float moveSpeed = 1f;
        [Range(1f, 20f)] [SerializeField] private float jumpForce = 1f;
        private Rigidbody _rbPlayer;
        
        private static readonly int JumpTrigger = Animator.StringToHash("Jump");
        private static readonly int IsRunningForward = Animator.StringToHash("IsRunningForward");
        private static readonly int IsRunningSide = Animator.StringToHash("IsRunningSide");
        private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
        
        public static bool IsPlayerMovable { get; set; }
        
        private Vector3 moveDirection;
        public static float X_movementUnitVector;
        public static float Z_movementUnitVector;
        private bool _isJumpAvailable;

        public LayerMask groundMask;
        // [Range(0.05f, 3f)] [SerializeField] 
        private float _groundCheckDistance = 0.1f;

        private void Start()
        {
            IsPlayerMovable = true;
            _rbPlayer = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            X_movementUnitVector = Input.GetAxis("Horizontal");
            Z_movementUnitVector = Input.GetAxis("Vertical");

            var isPlayerMoving = Mathf.Abs(Z_movementUnitVector) > 0.1f || Mathf.Abs(X_movementUnitVector) > 0.1f;
            PlayerAnimator.SetBool(IsRunningForward, isPlayerMoving);
            // PlayerAnimator.SetBool(IsRunningForward, Mathf.Abs(X_movementUnitVector) > 0.1f);
            PlayerAnimator.SetBool(IsGrounded, IsPlayerGrounded());

            if (Input.GetKeyDown(KeyCode.Space))
                _isJumpAvailable = true;
        }

        private void FixedUpdate()
        {
            if (!IsPlayerMovable) return;
            
            var cTransform = PlayerRotationController.cameraTransform;
            var currentVelocity = (X_movementUnitVector * cTransform.right + Z_movementUnitVector *
                cTransform.forward) * moveSpeed;
            _rbPlayer.velocity = currentVelocity;

            if (_isJumpAvailable && IsPlayerGrounded())
            {
                PlayerAnimator.SetTrigger(JumpTrigger);
                PlayerAnimator.SetBool(IsGrounded, false);
                _rbPlayer.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                _isJumpAvailable = false;
            }
        }

        private bool IsPlayerGrounded()
        {
            // RaycastHit hit;
            var isGrounded = Physics.Raycast(transform.position, Vector3.down, /*out hit,*/
                _groundCheckDistance, groundMask);
            PlayerAnimator.SetBool(IsGrounded, isGrounded);
            return isGrounded;
        }
    }
}