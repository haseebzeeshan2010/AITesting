using Unity.VisualScripting;
using UnityEngine;

public class WheelMesh : MonoBehaviour
{
    [SerializeField] GameObject wheelObject;
    public float restSuspensionDistance = 0.3f;
    public float suspensionLerpSpeed = 0.5f;
    public float offsetY = 0.05f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 tireRawPos;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, restSuspensionDistance))
        {
            // Convert hit.point from world space to local space relative to the current transform
            Vector3 localHitPoint = transform.InverseTransformPoint(hit.point);
            tireRawPos = localHitPoint + (Vector3.up + new Vector3(0, offsetY, 0));
            // Debug.Log(tireRawPos.y);
        }
        else
        {
            tireRawPos = 0 * Vector3.up + new Vector3(0, offsetY - 0.5f, 0);
        }
        wheelObject.transform.localPosition = Vector3.Lerp(wheelObject.transform.localPosition, tireRawPos, suspensionLerpSpeed * Time.fixedDeltaTime);
    }
}
