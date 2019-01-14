using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public TMP_Text AmmoText;

    void Start()
    {
        AmmoText.text = (GetComponent<PlayerManager>().AmmoInMag + " / " + GetComponent<PlayerManager>().MaxAmmoInMag);
    }
    
    void LateUpdate()
    {
        AmmoText.text = (GetComponent<PlayerManager>().AmmoInMag + " / " + GetComponent<PlayerManager>().MaxAmmoInMag);
    }
}
