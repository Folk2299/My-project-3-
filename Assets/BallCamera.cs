using UnityEngine;

public class BallCamera : MonoBehaviour
{
    public Transform target;          // ลูกบอล
    public float distance = 6f;       // ระยะกล้อง
    public float mouseSensitivity = 200f;
    public float height = 2f;         // ความสูงจากลูกบอล

    float xRotation = 15f;
    float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // ล็อกเมาส์กลางจอ
    }

    void LateUpdate()
    {
        // รับค่าการหมุนจากเมาส์
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        // จำกัดมุมมองขึ้นลง
        xRotation = Mathf.Clamp(xRotation, -30f, 60f);

        // คำนวณตำแหน่งกล้อง
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Vector3 offset = rotation * new Vector3(0, height, -distance);

        transform.position = target.position + offset;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}