using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]

public class Death_PreDestroy : GameAction
{
    public override void Awake()
    {
        Health health = GetComponent<Health>();

        health.OnDeath.AddListener(DestroyOnDeath);

        base.Awake();
    }

    private void DestroyOnDeath()
    {
        Destroy(gameObject);
        Debug.Log("Object destroyed!");

    }
}
