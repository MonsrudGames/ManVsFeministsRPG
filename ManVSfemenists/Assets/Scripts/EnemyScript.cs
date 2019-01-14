using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public float Health;

    void FixedUpdate()
    {
        if(Health <= 0f)
        {
            GameObject.Destroy(this.gameObject);
        }    
    }

    public void TakeDamage(float DamageTaken)
    {
        Health -= DamageTaken;
        GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude((this.transform.position - GameObject.FindGameObjectWithTag("Player").transform.position), 1f) * 200);
    }
}
