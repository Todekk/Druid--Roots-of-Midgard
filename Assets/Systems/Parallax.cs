using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float speed = 0.1f;
    private float length, startPos;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (transform.position.x + (speed * Time.deltaTime)) % length;
        transform.position = new Vector3(startPos + temp, transform.position.y, transform.position.z);
    }
}

