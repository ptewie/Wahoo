using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidPawn : Pawn
{
    private Animator animator;    

    public void Awake()
    {
        animator = GetComponent<Animator>();
        
    }

    public Animator GetAnimator()
    {
        return animator;
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
    
        transform.position = animator.rootPosition;
        transform.rotation = animator.rootRotation;

        AIController aiController = controller as AIController;
        if (aiController != null) {
            aiController.agent.nextPosition = animator.rootPosition;            
        }
    }

        public void OnAnimatorIK()
    {
        // If they don't have a weapon, don't worry about IK
        if (!weapon) {
            // Set their IK weights to 0
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
        
            // Leave the function
            return;
        }

        // Set the IK for our Right Hand
        if (weapon.RightHandIKTarget)
        {
            animator.SetIKPosition(AvatarIKGoal.RightHand, weapon.RightHandIKTarget.position);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            animator.SetIKRotation(AvatarIKGoal.RightHand, weapon.RightHandIKTarget.rotation);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
        }

        // Set the IK for our Left LeftHandIKTarget)
        if (weapon.LeftHandIKTarget)
        {
            animator.SetIKPosition(AvatarIKGoal.LeftHand, weapon.LeftHandIKTarget.position);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, weapon.LeftHandIKTarget.rotation);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
        }
    }
}