using UnityEngine;

public class Puerta : MonoBehaviour
{

    public float speed = 1f;

    public float min = 0f;

    public float max = 5f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(transform.position.z > max || transform.position.z < min)
        {
            speed = -speed;
        }
    }
}
