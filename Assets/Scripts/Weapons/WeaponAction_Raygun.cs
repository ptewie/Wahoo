using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAction_Raygun : WeaponAction
{
    public float fireDistance;
    public Transform firepoint;
    public bool isCharged;
    private float chargefactor = 1;

    public float chargeModifier;

    [SerializeField] private GameObject laserPrefab;

    private bool isAutofireActive;
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    public override void Awake()
        {
            base.Awake();        
        }

        public override void Start()
        {        
            base.Start();      
            isCharged = false; 
        }

        // Update is called once per frame
        public override void Update()
        {
            if (isAutofireActive)
            {
                Shoot();
            }   
            base.Update();       
        }
        public void Shoot()
        {        
        // Create a variable to hold our raycast hit data
        RaycastHit hit;

        // Check if it is time to fire our weapon 
        float secondsPerShot = 1/weapon.fireRate;
        if (Time.time >= lastShotTime + secondsPerShot) {

            // Store the direction we shoot without the accuracy system
            Vector3 newFireDirection = firepoint.forward;

            // Get the roation change based on our accuracy
            Quaternion accuracyFireDelta = Quaternion.Euler(0, weapon.GetAccuracyRotationDegrees(weapon.owner.controller.accuracy), 0);

            // Multiply by the rotation from inaccuacy to set new rotation value
            newFireDirection = accuracyFireDelta * newFireDirection;

            if (Physics.Raycast(firepoint.position, newFireDirection, out hit, fireDistance)) {

                // If we hit, and the other object has a Health component
                Health otherHealth = hit.collider.gameObject.GetComponent<Health>();

                if ( otherHealth != null) {
                    // Tell it to take damage!
                    otherHealth.TakeDamage(weapon.damageDone * chargefactor);
                    Debug.Log("Hit!");
                    ClearCharge();
                }
                
            }

            LaserBeam laser = Instantiate(laserPrefab, this.transform).GetComponent<LaserBeam>();
            laser.startPoint = firepoint.position;
            laser.endPoint = firepoint.position + (newFireDirection * fireDistance);

            // Save the time we shot
            lastShotTime = Time.time;
        }
    }

        public void Charge()
        {
            if (isCharged) 
            {
                isCharged = false;
                chargefactor = 1; 
            } 
            else
            {
                chargefactor = chargeModifier;
                isCharged = true;
            }
        }

        public void ClearCharge()
        {
            isCharged = false;
        }
}

