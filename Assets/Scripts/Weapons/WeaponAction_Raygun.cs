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
            // if so, do the Raycast

            if (Physics.Raycast(firepoint.position, firepoint.forward, out hit, fireDistance)) {
                // If we hit, and the other object has a Health component
                Health otherHealth = hit.collider.gameObject.GetComponent<Health>();

                if ( otherHealth != null) {
                    // Tell it to take damage!
                    otherHealth.TakeDamage(weapon.damageDone * chargefactor);
                    Debug.Log("Hit!");
                }
            }

            LaserBeam laser = Instantiate(laserPrefab, this.transform).GetComponent<LaserBeam>();
            laser.startPoint = firepoint.position;
            laser.endPoint = laser.startPoint + (firepoint.forward * fireDistance);

            // Save the time we shot
            lastShotTime = Time.time;
        }
    }

        public void Charge()
        {
            if (isCharged) 
            {
                isCharged = false;
                chargefactor = chargeModifier; 
            } 
            else
            {
                chargefactor = 1;
                isCharged = true;
            }
        }
}

