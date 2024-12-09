using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
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
    }
}