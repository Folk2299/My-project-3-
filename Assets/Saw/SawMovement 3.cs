using UnityEngine;

public class SawMovement3 : MonoBehaviour
{
    public float speed = 16f;
    public float distance = 20f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float move = Mathf.PingPong(Time.time * speed, distance * 2) - distance;
        transform.position = startPosition + new Vector3(0, 0, move);
    }
}
