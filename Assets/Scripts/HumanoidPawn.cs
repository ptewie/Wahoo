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
        direction *= maxMoveSpeed;

        direction = transform.InverseTransformDirection(direction);

        animator.SetFloat("Forward", direction.z);
        animator.SetFloat("Right", direction.x);
    }

    public override void Rotate(float speed)
    {
        transform.Rotate(0, speed * maxRotationSpeed * Time.deltaTime, 0);
    }

    public override void RotateToLookAt(Vector3 target)
    {
        Vector3 lookVector = target - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(lookVector, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, maxRotationSpeed * Time.deltaTime);
    }

    public void OnAnimatorMove()
    {
        // After the animation runs
        transform.position = animator.rootPosition;
        transform.rotation = animator.rootRotation;

        AIController aiController = controller as AIController;
        if (aiController != null) {
            aiController.agent.nextPosition = animator.rootPosition;            
        }
    }
}