using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
// [เพิ่มจาก Lab 02]
// ใช้สำหรับโหลด Scene ใหม่

public class BallMove : MonoBehaviour
{
    public float speed = 5f;
    public float sprintMultiplier = 1.8f;
    public float jumpForce = 5f;

    public float fallOfMap = -20f;
    // [เพิ่มจาก Lab 02]
    // ค่าความสูงแกน Y ที่ถือว่า Player ตก Map

    Rigidbody rb;

    bool isGrounded = false;
    bool jumpQueued = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ------------------------------
        // ตรวจสอบการตก Map
        // ------------------------------
        if (transform.position.y < fallOfMap)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // [เพิ่มจาก Lab 02]
            // โหลด Scene ปัจจุบันใหม่
        }

        // อ่านคำสั่งกระโดด
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            jumpQueued = true;
        }
    }

    void FixedUpdate()
    {
        Vector2 dir = Vector2.zero;

        if (Keyboard.current.aKey.isPressed) dir.x -= 1;
        if (Keyboard.current.dKey.isPressed) dir.x += 1;
        if (Keyboard.current.wKey.isPressed) dir.y += 1;
        if (Keyboard.current.sKey.isPressed) dir.y -= 1;

        float currentSpeed = speed;
        if (Keyboard.current.leftShiftKey.isPressed)
            currentSpeed *= sprintMultiplier;

        Vector3 move = new Vector3(dir.x, 0f, dir.y).normalized;

        rb.MovePosition(
            rb.position + move * currentSpeed * Time.fixedDeltaTime
        );

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