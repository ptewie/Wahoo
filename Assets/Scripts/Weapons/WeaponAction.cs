using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class WeaponAction : GameAction
{
    protected Weapon weapon;
    public float lastShotTime;

    public override void Awake() 
    {
        weapon = GetComponent<Weapon>();
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();        
    }
}