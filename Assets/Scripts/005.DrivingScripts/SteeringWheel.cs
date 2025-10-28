using UnityEngine;

public class SteeringWheel : MonoBehaviour
{
    [SerializeField] private float maxSteerAngle = 180f;
    [SerializeField] private GameObject steeringWheelObject;

    [SerializeField] private float steeringLerpSpeed = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float mouseXNorm = 0f;
        if (Screen.width > 0)
            mouseXNorm = Mathf.Clamp01(Input.mousePosition.x / (float)Screen.width);
        float steerInput = mouseXNorm * 2f - 1f;

        Quaternion targetRot = Quaternion.Euler(0f, 0f, steerInput * maxSteerAngle);
        steeringWheelObject.transform.localRotation = Quaternion.Slerp(steeringWheelObject.transform.localRotation, targetRot, Time.deltaTime * steeringLerpSpeed);
    }
}
