using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    public float damageDone; 
    public float fireRate;

    [Header("Events")]
    public UnityEvent OnPrimaryAttackBegin;
    public UnityEvent OnPrimaryAttackEnd;
    public UnityEvent OnSecondaryAttackBegin;
    public UnityEvent OnSecondaryAttackEnd;
}
