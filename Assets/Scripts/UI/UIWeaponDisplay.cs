using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponDisplay : MonoBehaviour
{
    public Image weaponIconImage;

    void Update()
    {
        if (GameManager.instance.player != null
            && GameManager.instance.player.pawn != null
            && GameManager.instance.player.pawn.weapon != null
            && GameManager.instance.player.pawn.weapon.weaponIcon != null)
        {
            weaponIconImage.enabled = true;
            weaponIconImage.sprite = GameManager.instance.player.pawn.weapon.weaponIcon;
        } else
        {
            weaponIconImage.enabled = false;
        }
    }
}