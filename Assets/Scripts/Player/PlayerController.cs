using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float acceleration = 3f;
    public float deceleration = 2f;
    public float maxSpeed = 5f;

    [Header("Jump Settings")]
    public float jumpForce = 7f;
    public float gravity = 9.81f;


    private float currentSpeed = 0;
    bool canJump = true;

    // component
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //canJump = false;
        }

        Movement();
    }

    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal > 0.01f)
        {
            currentSpeed += acceleration * Time.deltaTime;     // -+ 좌,우 방향 구분
        }
        else if(horizontal < -0.01f)
        {
            currentSpeed -= acceleration * Time.deltaTime;     // -+ 좌,우 방향 구분
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        float y = rb.velocity.y;
        y -= gravity * Time.deltaTime;
        rb.velocity = transform.right * currentSpeed + transform.up * y;
    }

    public void TakeDamage(float amount)
    {
        Debug.Log($"체력 감소! {amount}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Plane"))
        {
            canJump = true;
            collision.transform.GetComponent<PlaneBase>().OnTileEnter(this);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Plane"))
        {
            //canJump = false;
        }
    }
}
