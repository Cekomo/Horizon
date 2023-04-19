using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerAttackController : MonoBehaviour
    {
        [SerializeField] private Animator PlayerAnimator;

        private static readonly int Attack = Animator.StringToHash("Attack");
        
        private readonly float maceSwingingPeriod = 2.5f; // fix its period, this is too short

        // Update is called once per frame
        void Update()
        {
            if (!Input.GetMouseButtonDown(0) || !PlayerMovementController.IsPlayerMovable) return;

            PlayerMovementController.IsPlayerMovable = false;
            StartCoroutine(SwingMaze());
        }

        private IEnumerator SwingMaze()
        {
            PlayerAnimator.SetTrigger(Attack);

            yield return new WaitForSeconds(maceSwingingPeriod); 
            PlayerMovementController.IsPlayerMovable = true;
        }
    }
}
