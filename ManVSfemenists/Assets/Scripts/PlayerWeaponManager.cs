using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public GameObject Bullet;

    public List<Bullet> Bullets;

    public GameObject WeaponPos;
    public Transform ShootPos;

    public Weapon[] Weapons;

    public int ActiveWeapon;
    public GameObject ActiveWeaponObj;

    Animator _animator;

    bool CanShoot = true;
    bool FirstShootFired = false;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        ShootPos = Weapons[ActiveWeapon - 1].ShootPos;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActiveWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActiveWeapon = 2;
        }


        if (Weapons[ActiveWeapon - 1] != null)
        {
            if (Input.GetKey(KeyCode.R) && Weapons[ActiveWeapon - 1].GetComponent<Weapon>().MagCurrentAmmo < Weapons[ActiveWeapon - 1].GetComponent<Weapon>().MagMaxAmmo)
            {
                StartCoroutine(Reload());
                FirstShootFired = false;
            }
            if (CanShoot)
            {
                if (Weapons[ActiveWeapon - 1].GetComponent<Weapon>().MagCurrentAmmo > 0)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        StartCoroutine(Shoot());
                        FirstShootFired = true;
                    }
                    else if (Input.GetKey(KeyCode.Mouse0) && FirstShootFired)
                    {
                        if (Weapons[ActiveWeapon - 1].GetComponent<Weapon>().FullAuto) 
                        {
                            StartCoroutine(Shoot());
                        }
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        CheckBullets();
        ChangeWeapon();
        CheckWeapons();
    }

    IEnumerator Reload()
    {
        CanShoot = false;
        _animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(Weapons[ActiveWeapon - 1].GetComponent<Weapon>().ReloadSpeed);
        Weapons[ActiveWeapon - 1].GetComponent<Weapon>().MagCurrentAmmo = Weapons[ActiveWeapon - 1].GetComponent<Weapon>().MagMaxAmmo;
        _animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.1f);
        CanShoot = true;
    }

    IEnumerator Shoot()
    {
        if (CanShoot)
        {
            CanShoot = false;
            GameObject CurrBullet = GameObject.Instantiate(Bullet, ShootPos.position, ShootPos.rotation, null);
            Bullets.Add(CurrBullet.GetComponent<Bullet>());
            yield return new WaitForSeconds(1 / Weapons[ActiveWeapon - 1].GetComponent<Weapon>().FireRate);
            Weapons[ActiveWeapon - 1].GetComponent<Weapon>().MagCurrentAmmo -= 1;
            CanShoot = true;
        }
    }

    void CheckBullets()
    {
        Bullet[] GOBullets = Bullets.ToArray();

        for (int i = 0; i < GOBullets.Length; i++)
        {
            if (GOBullets[i].ObjHit != null)
            {
                if (GOBullets[i].ObjHit.GetComponent<EnemyScript>() != null)
                {
                    GOBullets[i].ObjHit.GetComponent<EnemyScript>().TakeDamage(Weapons[ActiveWeapon - 1].Damage);

                    Bullets.Remove(GOBullets[i]);
                    GameObject.Destroy(GOBullets[i].gameObject);
                    GOBullets[i] = null;
                }
            }
            else if (GOBullets[i].LifeTime >= 10f)
            {
                Bullets.Remove(GOBullets[i]);
                GameObject.Destroy(GOBullets[i].gameObject);
                GOBullets[i] = null;
            }
            else
            {
                GOBullets[i].transform.position += GOBullets[i].transform.forward * 100 * Time.fixedDeltaTime;
            }
        }
    }

    void ChangeWeapon()
    {
        if(ActiveWeapon == 1)
        {
            if (Weapons[ActiveWeapon - 1] != null)
            {
                if (Weapons[ActiveWeapon - 1].IsEquiped == false)
                {
                    Weapons[ActiveWeapon - 1].IsEquiped = true;
                }
            }
            if (Weapons[ActiveWeapon] != null)
            {
                Weapons[ActiveWeapon].IsEquiped = false;
            }
        }
        else if(ActiveWeapon == 2)
        {
            if (Weapons[ActiveWeapon - 1] != null)
            {
                if (Weapons[ActiveWeapon - 1].IsEquiped == false)
                {
                    Weapons[ActiveWeapon - 1].IsEquiped = true;
                }
            }
            if (Weapons[ActiveWeapon - 2] != null)
            {
                Weapons[ActiveWeapon - 2].IsEquiped = false;
            }
        }
    }

    void CheckWeapons()
    {
        for (int i = 0; i < Weapons.Length; i++)
        {
            if(Weapons[i] != null)
            {
                if (Weapons[i].IsEquiped)
                {
                    Weapons[i].WeaponObj.SetActive(true);
                    ActiveWeaponObj = Weapons[ActiveWeapon - 1].WeaponObj;
                    Weapons[i].WeaponObj.transform.position = WeaponPos.transform.position;
                }
                else
                {
                    Weapons[i].WeaponObj.SetActive(false);
                }
            }
        }
    }
}
