using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public float maxRotationSpeed;
    public Controller controller;
    public float maxMoveSpeed;

    public abstract void Move(Vector3 direction);

    public abstract void Rotate(float speed);   

    public abstract void RotateToLookAt(Vector3 targetPoint);
}
