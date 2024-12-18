using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public Controller controller;
    public float maxRotationSpeed;
    public float maxMoveSpeed;

    public Weapon weapon;
    public Transform weaponAttachmentPoint;

    public abstract void Move(Vector3 direction);

    public abstract void Rotate(float speed);   

    public abstract void RotateToLookAt(Vector3 target);

    public void EquipWeapon ( Weapon weaponToEquip )
    {
        UnequipWeapon();
        Debug.Log(weaponToEquip + " " + weaponAttachmentPoint);
        weapon = Instantiate(weaponToEquip, weaponAttachmentPoint) as Weapon;
        weapon.gameObject.layer = this.gameObject.layer;
    }

    public void UnequipWeapon ()
    {
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
        }

        weapon = null;
    }
}
