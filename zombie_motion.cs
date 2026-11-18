using UnityEngine;

public class zombie_motion : MonoBehaviour
{
    public Vector3 direction = Vector3.forward;
    public float speed = 5f;
    Rigidbody rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * speed;
    }
}
