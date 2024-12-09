using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    public bool isMouseRotation;

    protected override void Update()
    {
        base.Update();
    }

    protected override void MakeDecisions()
    {
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveVector = Vector3.ClampMagnitude(moveVector, 1);
        pawn.Move(moveVector);
        pawn.Rotate(Input.GetAxis("CameraRotation"));
        Debug.Log("test");
        if (isMouseRotation) 
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            Plane footPlane = new Plane(Vector3.up, pawn.transform.position);

            float distanceToIntersect;
            if (footPlane.Raycast(mouseRay, out distanceToIntersect)) 
            { 
                // Find the intersection point
                Vector3 intersectionPoint = mouseRay.GetPoint(distanceToIntersect);

                // Look at the intersection point
                //pawn.RotateToLookAt(intersectionPoint);
            } else 
            {
                Debug.Log("Camera is not looking at the ground - no intersection between plane and ray");
            }
        } else 
        { 
            // Tell the pawn to rotate based on the CameraRotation axis
            pawn.Rotate(Input.GetAxis("CameraRotation"));
        }
    }

    
}