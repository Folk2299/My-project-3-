using UnityEngine;
using UnityEngine.InputSystem;

public class BallMove : MonoBehaviour
{
    public float moveForce = 25f;        // แรงผลักให้ลูกบอลกลิ้ง
    public float maxSpeed = 8f;          // จำกัดความเร็วไม่ให้พุ่งเกิน
    public float sprintMultiplier = 1.8f;
    public float jumpForce = 5f;

    Rigidbody rb;

    bool isGrounded = false;
    bool jumpQueued = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // อ่าน Space ใน Update กันพลาด
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            jumpQueued = true;
    }

    void FixedUpdate()
    {
        Vector2 dir = Vector2.zero;

        if (Keyboard.current.aKey.isPressed) dir.x -= 1;
        if (Keyboard.current.dKey.isPressed) dir.x += 1;
        if (Keyboard.current.wKey.isPressed) dir.y += 1;
        if (Keyboard.current.sKey.isPressed) dir.y -= 1;

        float currentForce = moveForce;
        if (Keyboard.current.leftShiftKey.isPressed)
            currentForce *= sprintMultiplier;

        Vector3 moveDir = new Vector3(dir.x, 0f, dir.y).normalized;

        // 1) ใช้แรงผลัก → ลูกบอลจะกลิ้งจริง
        rb.AddForce(moveDir * currentForce, ForceMode.Force);

        // 2) จำกัดความเร็วแนวราบ (กันไหลเร็วเกิน)
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limited = flatVel.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(limited.x, rb.linearVelocity.y, limited.z);
        }

        // 3) กระโดด
        if (jumpQueued && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        jumpQueued = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}