using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Require a Health Component
[RequireComponent(typeof(Health))]
public class Death_Destroy : GameAction
{
    [SerializeField] private float delayBeforeDestruction;

    public override void Awake()
    {
        Health health = GetComponent<Health>();

        health.OnDeath.AddListener(DestroyOnDeath);

        base.Awake();
    }

    private void DestroyOnDeath()
    {
        Destroy(gameObject, delayBeforeDestruction);
    }
}