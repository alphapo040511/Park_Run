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

    [Header("Ground Check Settings")]
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    public float rotateSpeed = 5f;

    private float currentSpeed = 0;
    private float targetAngle = 0;
    private float currentAngle = 0;

    // component
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentAngle = transform.localEulerAngles.z;
        targetAngle = currentAngle;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        Movement();
        Rotate();
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

        float y = Vector3.Dot(rb.velocity, transform.up);
        y -= gravity * Time.deltaTime;
        rb.velocity = transform.right * currentSpeed + transform.up * y;
    }

    void Rotate()
    {
        float delta = Mathf.DeltaAngle(currentAngle, targetAngle);
        if (Mathf.Abs(currentAngle - targetAngle) > 0.01f)
        {
            currentAngle += delta * Time.deltaTime * rotateSpeed;
        }
        else
        {
            currentAngle = targetAngle;
        }

        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));
    }

    public void TakeDamage(float amount)
    {
        Debug.Log($"체력 감소! {amount}");
    }

    bool IsGrounded()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, checkRadius, groundLayer);

        return col.Length > 0;      // 하나라도 걸리면 바닥이다.
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Plane"))
        {
            collision.transform.GetComponent<PlaneBase>().OnTileEnter(this);
            targetAngle = collision.transform.parent.localEulerAngles.z;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            other.transform.GetComponent<PlaneBase>().OnTileEnter(this);
            targetAngle = other.transform.parent.localEulerAngles.z;
        }
    }
}
