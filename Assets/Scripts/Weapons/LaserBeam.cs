using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))] 
public class LaserBeam : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;
    public Color color = Color.white;
    public float lifespan = 0.1f;
    public float width = 0.5f;
    private LineRenderer lr;


    // Start is called before the first frame update
    void Start()
    {
        // Get the line renderer
        lr = GetComponent<LineRenderer>();
    
        // Set the color
        lr.startColor = color;
        lr.endColor = color;

        // Set the width
        lr.startWidth = width;
        lr.endWidth = width;    

        // Set the start and end points
        Vector3[] points = {startPoint, endPoint};
        lr.SetPositions(points);

        // Self-destruct after lifespan
        Destroy(gameObject, lifespan );        
    }
}