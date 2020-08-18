using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 움직임 관련 변수들
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpForce;
    private Vector3 velocity; //움직이는 속도
    private float move_direction = 0f;
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
        if (isGround)
        {
            Movement();
            Jump();
            //Move();
        }
        IsGround();
    }

    private void Movement()
    {
        move_direction = Mathf.Lerp(move_direction, Input.GetAxisRaw("Horizontal"), 0.1f);
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            move_direction = Mathf.Lerp(move_direction, 0, 0.5f); ;
        }
        velocity = (transform.right * move_direction).normalized * walkSpeed * 0.1f;
        myRigid.MovePosition(transform.position + velocity);
    }

    private void Move()
    {
        float move_direction = Input.GetAxisRaw("Horizontal");
        velocity = (transform.right * move_direction).normalized * walkSpeed * 0.1f;
        myRigid.MovePosition(transform.position + velocity);
    }

    private void Jump()
    {
        //점프 거리 조절
        if (Input.GetButtonDown("Jump")) //점프 시작
        {
            myRigid.velocity = transform.up * jumpForce + velocity * jumpForce * 5;
        }
        else if (Input.GetButtonUp("Jump")) //점프 끝
        {

        }
    }

    private void IsGround()
    {
        isGround = Physics2D.Raycast(transform.position, Vector3.down, myCollider.bounds.extents.y + 0.1f);
    }
}
