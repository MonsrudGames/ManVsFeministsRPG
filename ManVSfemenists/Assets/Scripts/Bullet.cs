using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject BulletObj;

    public float LifeTime;

    public GameObject ObjHit;
    
    private void FixedUpdate()
    {
        LifeTime += Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("Enemy"))
        {
            ObjHit = other.gameObject;
        }
    }
}
