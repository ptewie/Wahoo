using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActionSpawnParticles : GameAction
{
    public GameObject particlePrefab;
    public float lifespan;
    public Transform targetNode;

    public void SpawnParticles ()
    {
        // Instantiate the particles at the target node
        GameObject particles = Instantiate<GameObject>(particlePrefab, targetNode.position, targetNode.rotation);

        // if there is a lifespan, set the particle system to destroy
        if (lifespan > 0)
        {
            Destroy(particles, lifespan);
        }
    }
    public void EndParticles()
    {
        Destroy(particlePrefab);
    }
}
