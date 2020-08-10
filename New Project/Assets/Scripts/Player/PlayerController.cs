using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 움직임 관련 변수들
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpForce;
    private Vector3 velocity; //움직이는 속도
    private bool isGround;

    private BoxCollider2D myCollider;
    private Rigidbody2D myRigid;


    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGround();
        Jump();
        Move();
    }
    
    private void Move()
    {
        float move_direction = Input.GetAxisRaw("Horizontal");
        velocity = (transform.right * move_direction).normalized * walkSpeed;
        myRigid.MovePosition(transform.position + velocity);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            myRigid.velocity = transform.up * jumpForce/* + velocity * jumpForce*/;
        }
    }
    
    private void IsGround()
    {
        isGround = Physics2D.Raycast(transform.position, Vector3.down, myCollider.bounds.extents.y + 0.05f);
    }
}
