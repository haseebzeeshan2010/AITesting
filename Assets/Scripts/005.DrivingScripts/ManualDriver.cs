using UnityEngine;

public class ManualDriver : MonoBehaviour
{
    public float driveForce = 10f;
    public float turnTorque = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Determine forward/backward input using arrow keys or WASD
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // Apply forward/backward force
        Vector3 force = transform.right * -moveInput * driveForce;
        rb.AddForce(force, ForceMode.Force);

        // Apply turning torque around the y-axis
        Vector3 torque = transform.forward * turnInput * turnTorque;
        rb.AddTorque(torque, ForceMode.Force);
    }
}
