using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(HumanoidPawn))]
public class Death_RagdollAndDestroy : GameAction
{
    public float delayBeforeDestruction;
    public UnityEvent OnDestroyGameObject;
    public UnityEvent OnDespawn;
    private HumanoidPawn humanoidPawn;
    private Health health;

    private Collider mainCollider;
    private Rigidbody mainRigidbody;
    private Collider[] childColliders;
    private Rigidbody[] childRigidbodies;

    public override void Awake()
    {
        // Get our Health component in a variable, so we can access it
        health = GetComponent<Health>();

        // Get our humanoidPawn component in a variable, so we can access it
        humanoidPawn = GetComponent<HumanoidPawn>();

        // get the rigidbodies and colliders
        mainRigidbody = GetComponent<Rigidbody>();
        mainCollider = GetComponent<Collider>();
        childColliders = GetComponentsInChildren<Collider>();
        childRigidbodies = GetComponentsInChildren<Rigidbody>();

        // Do whatever all GameActions do on Awake
        base.Awake();
    }

    public override void Start()
    {
        // Subscribe automatically to the OnDeath event, like our other Death events do.
        if (health != null )
        {
            health.OnDeath.AddListener(RagdollAndDestroy);
        }

        // Make sure we are NOT in ragdoll mode
        DeactivateRagdoll();

        // Do whatever all GameActions do on Start
        base.Start();
    }

    public void RagdollAndDestroy()
    {
        // Turn on the ragdoll mode
        ActivateRagdoll();

        // Set this object up to be destroyed after a short time
        Destroy(gameObject, delayBeforeDestruction);



        // Unequip our weapon
        humanoidPawn.UnequipWeapon();

        // Activate ragdoll
        ActivateRagdoll();
    }

    private void ActivateRagdoll()
    {
        // Turn on the animator
        humanoidPawn.GetAnimator().enabled = false;

        // Turn on all the child colliders
        foreach (Collider collider in childColliders)
        {
            collider.enabled = true;
        }

        // Turn on all the child rigidbodies ( Make isKinematic false )
        foreach (Rigidbody rb in childRigidbodies)
        {
            rb.isKinematic = false;
        }

        // Turn off our main collider
        mainCollider.enabled = false;

        // Turn off our main rigidbody
        mainRigidbody.isKinematic = true;
    }

    private void DeactivateRagdoll()
    {
        // Turn off the animator
        humanoidPawn.GetAnimator().enabled = true;

        // Turn off all the child colliders
        foreach (Collider collider in childColliders)
        {
            collider.enabled = false;
        }

        // Turn off all the child rigidbodies (make isKinematic true)
        foreach (Rigidbody rb in childRigidbodies)
        {
            rb.isKinematic = true;
        }

        // Turn on our main collider
        mainCollider.enabled = true;

        // Turn on our main rigidbody
        mainRigidbody.isKinematic = false;
    }


    public void OnDestroy()
    {
        // When this object is actually destroyed, send out an event
        OnDestroyGameObject.Invoke();
    }
}