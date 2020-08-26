using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Gun currentGun;

    private void OnCollisionEnter(Collision other)
    {
        if(other != null)
        {
            //if (other.transform.tag == "NPC")
                //other.transform.GetComponent<Animal>().Damage(currentGun.damage, transform.position);
        }
        Destroy (this.gameObject);
    }

    void Update() {
        transform.right = GetComponent<Rigidbody2D>().velocity;
    }
}
