using UnityEngine;
using UnityEngine.InputSystem;

public class ManualDriver : MonoBehaviour
{
    [SerializeField] Rigidbody carRigidbody;
    [SerializeField] float gravityAdder = -9.81f;
    void Start()
    {
        // Initialization logic can be added here if needed.
    }

    void FixedUpdate()
    {
        carRigidbody.AddForce(Vector3.down * gravityAdder, ForceMode.Acceleration);
    }
}
