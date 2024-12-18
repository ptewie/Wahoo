using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    public float damageDone; 
    public float fireRate;

    public float maxAccuracyRotation;

    public virtual float GetAccuracyRotationDegrees(float accuracyModifier = 1)
     {
        // Return a random percentage between min and max AccuracyRoation 
        // Get a random number between 0 and 1 ( a percentage )
        float accuracyDeltaPercentage = Random.value;

        // Find that percentage between the negative (to the Left) and positive (to the right) values of this rotation.
        float accuracyDeltaDegrees = Mathf.Lerp(-maxAccuracyRotation, maxAccuracyRotation, accuracyDeltaPercentage);
        accuracyDeltaDegrees *= accuracyModifier;
        
        // Return that value
        return accuracyDeltaDegrees;
     }

    public Transform RightHandIKTarget;

    public Transform LeftHandIKTarget;

    [Header("Events")]
    public UnityEvent OnPrimaryAttackBegin;
    public UnityEvent OnPrimaryAttackEnd;
    public UnityEvent OnSecondaryAttackBegin;
    public UnityEvent OnSecondaryAttackEnd;
}
