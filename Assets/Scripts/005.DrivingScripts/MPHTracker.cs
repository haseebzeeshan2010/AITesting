using UnityEngine;

public class MPHTracker : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI speedText;
    [SerializeField] private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        int mph = (int)(rb.linearVelocity.magnitude * 2.23694f); // Convert from m/s to mph
        speedText.text = mph.ToString();
    }
}
