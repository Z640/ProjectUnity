using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartridge : MonoBehaviour
{

    [SerializeField]
    private float DestroyTime; 
    
    // Start is called before the first frame update
    void Start()
    {
       Destroy (this.gameObject, DestroyTime); 
    }
}
