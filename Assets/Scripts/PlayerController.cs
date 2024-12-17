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

        if (pawn != null && moveVector != null)
        {
            pawn.Move(moveVector);

            RotatePawn(isMouseRotation);
        }

        if (Input.GetButtonDown("Fire1")) {
            pawn.weapon.OnPrimaryAttackBegin.Invoke();
        }
        if (Input.GetButtonUp("Fire1")) {
            pawn.weapon.OnPrimaryAttackEnd.Invoke();
        }
        if (Input.GetButtonDown("Fire2")) {
            pawn.weapon.OnSecondaryAttackBegin.Invoke();
        }
        if (Input.GetButtonUp("Fire2")) {
            pawn.weapon.OnSecondaryAttackEnd.Invoke();
        }
    }

    private void RotatePawn(bool isMouseRotation)
    {
        
        if (isMouseRotation) 
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            Plane footPlane = new Plane(Vector3.up, pawn.transform.position);

            float distanceToIntersect;   

            if (footPlane.Raycast(mouseRay, out distanceToIntersect)) 
            { 
                Vector3 intersectionPoint = mouseRay.GetPoint(distanceToIntersect);

                pawn.RotateToLookAt(intersectionPoint);
            } else 
            {
                Debug.Log("Camera is not looking at the ground - no intersection between plane and ray");
            }
        } else 
            { 
                pawn.Rotate(Input.GetAxis("CameraRotation"));
            }
    }
}