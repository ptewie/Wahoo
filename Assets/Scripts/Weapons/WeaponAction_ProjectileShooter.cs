using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAction_ProjectileShooter : WeaponAction
{
    public float damageDone;
    public float fireRate;

    public Transform firePoint;
    public GameObject projectilePrefab;

    public void Shoot()
    {
        // Check if it is time to fire our weapon 
        float secondsPerShot = 1 / weapon.fireRate;
        if (Time.time >= lastShotTime + secondsPerShot)
        {
            // if so...

            // Store the direction we shoot without the accuracy system
            Vector3 newFireDirection = firePoint.forward;

            // Get the roation change based on our accuracy
            Quaternion accuracyFireDelta = Quaternion.Euler(0, weapon.GetAccuracyRotationDegrees(), 0);

            // Multiply by the rotation from inaccuacy to set new rotation value
            newFireDirection = accuracyFireDelta * newFireDirection;

            // Instantiate the projectile
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation) as GameObject;

            projectile.transform.Rotate(0, weapon.GetAccuracyRotationDegrees(weapon.owner.controller.accuracy), 0);       
           

            // Set the data for the projectile
            Projectile projectileData = projectile.GetComponent<Projectile>();
            if (projectileData != null )
            {
                projectileData.damage = weapon.damageDone;
            }

            // Save the time we shot
            lastShotTime = Time.time;
        }
    }
}
