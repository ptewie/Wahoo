using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidPawn : Pawn
{
    private Animator animator;    

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Move(Vector3 direction)
    {
        direction = direction * maxMoveSpeed;

        animator.SetFloat("Forward", direction.z);
        animator.SetFloat("Right", direction.x);
    }

    public override void Rotate(float speed)
    {
        transform.Rotate(0, speed * maxRotationSpeed * Time.deltaTime, 0);
    }

    public void OnAnimatorMove()
    {
        // After the animation runs
        // Use root motion to move the game object
        transform.position = animator.rootPosition;
        transform.rotation = animator.rootRotation;

        // If we have a NavMeshAgent on our controller,
        AIController aiController = controller as AIController;
        if (aiController != null) {
            // Set our navMeshAgent to understand it is as the position from the animator
            aiController.agent.nextPosition = animator.rootPosition;            
        }
    }
}