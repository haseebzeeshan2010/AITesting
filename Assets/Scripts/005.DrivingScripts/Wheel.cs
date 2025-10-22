using Unity.VisualScripting;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] Rigidbody carRigidbody;
    [SerializeField] float strength = 1000;
    [SerializeField] float dampingCoefficient = 0.7f;
    [SerializeField] float tireGripFactor = 0.5f;

    [SerializeField] float tireMass = 20f;

    public float restSuspensionDistance = 0.3f;
    public Vector3 tireWorldVel = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, restSuspensionDistance))
        {
            float distance = hit.distance;

            // Calculate spring force
            Vector3 springForce = Vector3.up * (restSuspensionDistance - distance) * strength;

            // Get the velocity of the wheel at the contact point
            tireWorldVel = carRigidbody.GetPointVelocity(transform.position);

            // Project the velocity onto the spring axis (upward direction)
            float relativeVelocity = Vector3.Dot(tireWorldVel, Vector3.up);

            // Calculate damping force proportional to the relative velocity
            Vector3 dampingForce = -relativeVelocity * dampingCoefficient * Vector3.up;

            // Apply both forces
            carRigidbody.AddForceAtPosition(springForce + dampingForce, transform.position);



            // Lateral grip force
            Vector3 steeringDirection = transform.forward;
            float steeringVel = Vector3.Dot(steeringDirection, tireWorldVel);
            float desiredVelChange = -steeringVel * tireGripFactor;
            float desiredAccel = desiredVelChange / Time.fixedDeltaTime;
            carRigidbody.AddForceAtPosition(steeringDirection * tireMass * desiredAccel, transform.position);

        }
    }
}