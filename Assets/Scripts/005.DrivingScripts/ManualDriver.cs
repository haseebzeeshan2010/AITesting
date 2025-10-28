using UnityEngine;
using UnityEngine.InputSystem;

public class ManualDriver : MonoBehaviour
{
    [SerializeField] private WheelDrive[] steeringWheels;
    [SerializeField] private WheelDrive[] driveWheels;
    
    void FixedUpdate()
    {
        float accelerationInput = Input.GetAxis("Vertical");

        // map mouse X from [0, Screen.width] to [-1, 1]
        float mouseXNorm = 0f;
        if (Screen.width > 0)
            mouseXNorm = Mathf.Clamp01(Input.mousePosition.x / (float)Screen.width);
        float steerInput = mouseXNorm * 2f - 1f;

        foreach (WheelDrive wheel in steeringWheels)
            wheel.Steer(steerInput);
        
        foreach (WheelDrive wheel in driveWheels)
            wheel.Accelerate(accelerationInput);
    }
}
