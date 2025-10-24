using UnityEngine;
using UnityEngine.InputSystem;

public class ManualDriver : MonoBehaviour
{
    [SerializeField] private WheelDrive[] steeringWheels;
    [SerializeField] private WheelDrive[] driveWheels;
    
    void FixedUpdate()
    {
        float accelerationInput = Input.GetAxis("Vertical");
        float steerInput = Input.GetAxis("Horizontal");
        
        foreach (WheelDrive wheel in steeringWheels)
            wheel.Steer(steerInput);
        
        foreach (WheelDrive wheel in driveWheels)
            wheel.Accelerate(accelerationInput);
    }
}
