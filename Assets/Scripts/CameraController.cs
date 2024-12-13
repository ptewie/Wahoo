using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float distance;

    void Update()
    {
        if(target != null)
            MoveCamera(target, distance, speed);
    }

    private void MoveCamera(Transform target, float distance, float speed)
    {
        Vector3 newPosition = new Vector3(target.position.x, target.position.y + distance, target.position.z);

        this.transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        
        transform.LookAt(new Vector3(transform.position.x, 0, transform.position.z), transform.up);
        
    }
}