using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    public Vector3 offset = new Vector3(0, 3, -7); 
    
    // ปรับค่าเหล่านี้ใน Inspector ตามคำแนะนำด้านล่าง
    public float smoothSpeed = 15f;         
    public float rotationSpeed = 20f;       

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("hero");
        if (player != null)
        {
            target = player.transform;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 1. การติดตามตำแหน่ง: ใช้ Lerp ที่ค่า Speed สูงขึ้น
        // การคูณด้วย Time.deltaTime จะทำให้ความเร็วคงที่ทุกเครื่อง
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // 2. การหันกล้อง: ใช้ LookAt ร่วมกับความนุ่มนวลเล็กน้อย
        // ถ้าต้องการให้เร็วที่สุด ให้ใช้ LookAt ตรงๆ หรือ Slerp ที่ค่าสูงๆ
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}