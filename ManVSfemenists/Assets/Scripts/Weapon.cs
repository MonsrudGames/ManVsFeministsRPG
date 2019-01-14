using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float Damage;
    public float FireRate;
    public float FireSpread;
    public float ReloadSpeed;

    public int MagCurrentAmmo;
    public int MagMaxAmmo;

    public bool FullAuto;

    public bool IsEquiped;

    public GameObject WeaponObj;
    public Transform ShootPos;
}
