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
        Vector3 newPosition = new Vector3(target.position.x, target.position.y + distance, target.position.z);
        transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        transform.LookAt(target.position, target.forward);
    }
}