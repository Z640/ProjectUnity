using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.T)) {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.rotation.z * 10, 0f));
            
            Debug.Log(transform.rotation);
        }
    }
}