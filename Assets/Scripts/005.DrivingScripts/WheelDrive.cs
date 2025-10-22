using UnityEngine;
using System.Collections.Generic;

public class WheelDrive : MonoBehaviour
{
    [SerializeField] Rigidbody carRigidbody;
    [SerializeField] float maxStrength = 1000;
    [SerializeField] float maxVelocity = 10f;

    [SerializeField] private AnimationCurve accelerationCurve;

    public float restSuspensionDistance = 0.3f;
    public float normalizedSpeed = 0f;
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
            Vector3 forwardDirection = -transform.right;

            // Calculate normalized speed (0 to 1)
            normalizedSpeed = Mathf.Clamp01(carRigidbody.linearVelocity.magnitude / maxVelocity);
            
            // Adjust strength using the acceleration curve
            float curveMultiplier = accelerationCurve.Evaluate(normalizedSpeed);
                
                // Apply force scaled by the multiplier
                carRigidbody.AddForceAtPosition(forwardDirection * maxStrength * curveMultiplier, transform.position);
            }
    }

}
