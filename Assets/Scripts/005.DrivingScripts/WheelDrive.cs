using UnityEngine;
using System.Collections.Generic;

public class WheelDrive : MonoBehaviour
{
    [SerializeField] Rigidbody carRigidbody;
    [SerializeField] float maxStrength = 1000;
    [SerializeField] float maxVelocity = 10f;

    [SerializeField] private AnimationCurve accelerationCurve;
    [SerializeField] private AnimationCurve decelerationCurve;

    public float restSuspensionDistance = 0.3f;
    public float normalizedSpeed = 0f;
    public Vector3 tireWorldVel = Vector3.zero;

    [SerializeField] private float maxSteerAngle = 30f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    public void Accelerate(float accelerationInput)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, restSuspensionDistance))
        {

            if (accelerationInput > 0)
            {
                // Apply force when accelerating
                Vector3 forwardDirection = -transform.right;

                // Calculate normalized speed (0 to 1)
                normalizedSpeed = Mathf.Clamp01(carRigidbody.linearVelocity.magnitude / maxVelocity);

                // Adjust strength using the acceleration curve
                float curveMultiplier = accelerationCurve.Evaluate(normalizedSpeed);

                // Apply force scaled by the multiplier
                carRigidbody.AddForceAtPosition(forwardDirection * maxStrength * curveMultiplier, transform.position);
            }
            else if (accelerationInput < 0)
            {
                // Apply force when braking/reversing
                Vector3 backwardDirection = transform.right;

                // Calculate normalized speed (0 to 1)
                normalizedSpeed = Mathf.Clamp01(carRigidbody.linearVelocity.magnitude / maxVelocity);

                // Adjust strength using the deceleration curve
                float curveMultiplier = decelerationCurve.Evaluate(normalizedSpeed);

                // Apply force scaled by the multiplier
                carRigidbody.AddForceAtPosition(backwardDirection * maxStrength * curveMultiplier, transform.position);
            }
            
        }
    }

    public void Steer(float steerInput)
    {
        float steerAngle = steerInput * maxSteerAngle;
        transform.localRotation = Quaternion.Euler(0, steerAngle, 0);
    }

}
