using UnityEngine;

public class SawMovement : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 3f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float move = Mathf.PingPong(Time.time * speed, distance * 2) - distance;
        transform.position = startPosition + new Vector3(move, 0, 0);
    }
}

