using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public GameObject hitEF;
    private void OnCollisionEnter2D(Collision2D col) {
        if (col != null) {
            if (col.transform.tag != "Player") {
                Destroy(Instantiate(hitEF, transform.position, Quaternion.identity), 1f);
                Destroy(this.gameObject);
            }
        }
    }
    void Update() {
        transform.right = GetComponent<Rigidbody2D>().velocity;
    }
}