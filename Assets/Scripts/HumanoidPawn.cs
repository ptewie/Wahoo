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
        animator.SetFloat("Forward", direction.z);
        animator.SetFloat("Right", direction.x);
    }
}