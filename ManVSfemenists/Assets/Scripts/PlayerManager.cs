using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    bool GameIsPaused;
    bool HideMouseCursor = true;

    public GameObject Player;

    public int AmmoInMag;
    public int MaxAmmoInMag;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void LateUpdate()
    {

        AmmoInMag = Player.GetComponent<PlayerWeaponManager>().ActiveWeaponObj.GetComponent<Weapon>().MagCurrentAmmo;
        MaxAmmoInMag = Player.GetComponent<PlayerWeaponManager>().ActiveWeaponObj.GetComponent<Weapon>().MagMaxAmmo;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideMouseCursor = !HideMouseCursor;
        }

        if (HideMouseCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
