using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : Controller
{
    [HideInInspector] public NavMeshAgent agent;
    public float stoppingDistance;
    public float accuracy;

    [SerializeField] private float shootingDistance;

    [SerializeField]  private float shootingAngle;


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

    private bool HasTarget()
    {
        if (targetTransform != null)
        {
            return true;
        }
        return false;
    }

    private void TargetPlayer()
    {
        Controller playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            if (playerController.pawn !=null)
            {
                targetTransform = playerController.pawn.transform;
            }
        }
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

        // If we are within distance
        if (Vector3.Distance(targetTransform.position, pawn.transform.position) <= shootingDistance)
        {
            // And we are within the angle
            Vector3 vectorToTarget = targetTransform.position - pawn.transform.position;
            if ( Vector3.Angle(pawn.transform.forward, vectorToTarget) <= shootingAngle)
            {
                // They should pull the trigger
                pawn.weapon.OnPrimaryAttackBegin.Invoke();
            }
        } else
        {
            // They can release the trigger
            pawn.weapon.OnPrimaryAttackEnd.Invoke();
        }
    }
}

