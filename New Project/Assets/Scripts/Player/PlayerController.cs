using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    // 움직임 관련 변수들
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    private float curSpeed;
    private Vector3 velocity; //움직이는 속도
    private float move_direction = 0f;
    private bool isGround;
    private bool isWalk;
    private bool isRun;

    [SerializeField] private Camera cam;
    [SerializeField] private GameObject holder;
    [SerializeField] private GameObject gunHolder;
    private BoxCollider2D myCollider;
    private Rigidbody2D myRigid;
    private Animator anime;

    // Start is called before the first frame update
    void Start() {
        myRigid = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        Move();
        Run();
        CamMovement();
        if (isGround) {
            //Movement();
            Jump();
            //Move();
        }
        IsGround();
        Animation();
        HolderRotation();
    }

    private void HolderRotation() {
        Vector3 lookat = Camera.main.ScreenToWorldPoint(Input.mousePosition) - holder.transform.position;
        float AngleRad = Mathf.Atan2(lookat.y, lookat.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        holder.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }

    private void CamMovement() {
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(this.transform.position.x, 1f, -1f), 0.2f);
    }

    private void Animation() {
        anime.SetBool("isWalk", isWalk);
        anime.SetBool("isRun", isRun && isWalk);
    }

    private void Move() {
        float move_direction = Input.GetAxisRaw("Horizontal");
        if (move_direction > 0) {
            this.transform.localEulerAngles = Vector3.zero;
            gunHolder.transform.localEulerAngles = Vector3.zero;
        } else if (move_direction < 0) {
            this.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            gunHolder.transform.localEulerAngles = new Vector3(180f, 0f, 0f);
        }
        isWalk = (move_direction != 0);
        velocity = (transform.right * Mathf.Abs(move_direction)).normalized * curSpeed * 0.1f;
        myRigid.MovePosition(transform.position + velocity);
    }

    private void Run() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            curSpeed = runSpeed;
            isRun = true;
        } else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            curSpeed = walkSpeed;
            isRun = false;
        }
    }

    private void Jump() {
        //점프 거리 조절
        if (Input.GetButtonDown("Jump")) //점프 시작 
        {
            myRigid.AddForce(transform.up * jumpForce);
            //myRigid.velocity = transform.up * jumpForce;
        } else if (Input.GetButtonUp("Jump")) //점프 끝
        { }
    }

    private void IsGround() {
        isGround = Physics2D.Raycast(transform.position, Vector3.down, myCollider.bounds.extents.y + 0.1f);
    }
}