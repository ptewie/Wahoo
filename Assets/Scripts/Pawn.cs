using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Pawn : MonoBehaviour
{
    public Controller controller;
    public float maxRotationSpeed;
    public float maxMoveSpeed;

    public Weapon weapon;
    [SerializeField] protected Weapon[] startingWeaponOptions;
    public Transform weaponAttachmentPoint;

    protected virtual void Start()
    {
        if (startingWeaponOptions.Length > 0)
        {
            EquipWeapon(startingWeaponOptions[UnityEngine.Random.Range(0,startingWeaponOptions.Length)]);
        }
    }

    public abstract void Move(Vector3 direction);

    public abstract void Rotate(float speed);   

    public abstract void RotateToLookAt(Vector3 target);

    public void EquipWeapon ( Weapon weaponToEquip )
    {
        UnequipWeapon();
        Debug.Log(weaponToEquip + " " + weaponAttachmentPoint);
        weapon = Instantiate(weaponToEquip, weaponAttachmentPoint) as Weapon;
        weapon.gameObject.layer = this.gameObject.layer;
        
        // Set the weapon's owner
        weapon.owner = this;
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
