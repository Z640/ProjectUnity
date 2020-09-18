using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    private bool dead;
    private bool visible;
    private float health;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    bool is_dead()
    {
        return dead;
    }
    bool is_visible()
    {
        return visible;
    }
    float get_health()
    {
        return health;
    }
}
