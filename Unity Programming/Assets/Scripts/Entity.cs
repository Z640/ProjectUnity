using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Entity : MonoBehaviour
{

    private bool dead;
    private float health;
    private float speed;
    private float max_speed;

    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();


        dead = false;
        health = 10;
        speed = 1;
        max_speed = 5;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Move()
    {
        if (!(Mathf.Abs(rigid.velocity.x) > max_speed))
        {
            float move_horizontal = Input.GetAxisRaw("Horizontal");
            rigid.AddForce(Vector2.right * move_horizontal * speed, ForceMode2D.Impulse);


        }
    }

    void Force_move()
    {

    }

    bool Is_dead()
    {
        return dead;
    }
    float Get_health()
    {
        return health;
    }

    

}
