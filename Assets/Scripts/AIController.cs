using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : Controller
{
    [HideInInspector] public NavMeshAgent agent;
    public float stoppingDistance;

    public Pawn pawn;
    public Transform targetTransform;
    private Vector3 desiredVelocity = Vector3.zero;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void PossessPawn ( Pawn pawnToPossess ) 
    {  
        // Set the variables (from the base class's definition)
        base.PossessPawn(pawnToPossess);

        // Get the agent off the pawn
        agent = pawn.GetComponent();

        // If it doesn't have one, add one and store it
        if (agent == null) {
            agent = pawn.gameObject.AddComponent();
        }

        // Set the stopping distance
        agent.stoppingDistance = stoppingDistance;

        // Set the max speed of the AI from the pawn data
        agent.speed = pawn.maxMoveSpeed;

        // Set the max rotation speed of the AI from the pawn data
        agent.angularSpeed = pawn.maxRotationSpeed;

        // Disable movement and rotation from the NavMeshAgent
        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    public override void UnpossessPawn ()
    {
        // Remove the NavMeshAgent
        Destroy(agent);

        // Set the variables (from the base class's definition)
        base.UnpossessPawn();
    }

    // Update is called once per frame
    protected override void Update()
    {      
        // Do what all controllers do each frame (Make Decisions)
        base.Update();
    }

    protected override void MakeDecisions()
    {
        // If we don't have a pawn, we can't make decisions for it, so do nothing
        if (pawn == null)
        {
            return;
        }

        // Set our NavMeshAgent to seek our target
        agent.SetDestination(targetTransform.position);

        // Find the velocity that the agent wants to move in order to follow the path
        desiredVelocity = agent.desiredVelocity;

        // Send the direction in to our Move function (use the move function to add speed)
        pawn.Move(desiredVelocity.normalized);

        // Look towards the player
        pawn.RotateToLookAt(targetTransform.position);
    }
}

