using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : Controller
{
    [HideInInspector] public NavMeshAgent agent;
    public float stoppingDistance;

    //public Pawn pawn;
    public Transform targetTransform;
    private Vector3 desiredVelocity = Vector3.zero;

    protected override void Start()
    {
        base.Start();
    }

    public override void PossessPawn (Pawn pawnToPossess) 
    {  
        base.PossessPawn(pawnToPossess);

        agent = pawn.GetComponent<NavMeshAgent>();
        if (agent == null) {
            agent = pawn.gameObject.AddComponent<NavMeshAgent>();
            }
        agent.stoppingDistance = stoppingDistance;
        agent.speed = pawn.maxMoveSpeed;
        agent.angularSpeed = pawn.maxRotationSpeed;

        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    public override void UnpossessPawn ()
    {
        Destroy(agent);

        base.UnpossessPawn();
    }

    protected override void Update()
    {      
        base.Update();
    }

    protected override void MakeDecisions()
    {

        if (pawn == null)
        {
            return;
        }
        agent.SetDestination(targetTransform.position);
        desiredVelocity = agent.desiredVelocity;

        pawn.Move(desiredVelocity.normalized);
        pawn.RotateToLookAt(targetTransform.position);
    }
}

