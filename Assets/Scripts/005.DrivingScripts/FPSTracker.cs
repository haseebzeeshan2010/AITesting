using UnityEngine;

public class FPSTracker : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI fpsText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    [SerializeField, Range(0.01f, 1f)]
    private float smoothAlpha = 0.1f;
    private float smoothedDelta = 0f;

    void Update()
    {
        float dt = Time.unscaledDeltaTime;
        if (dt <= 0f) return;

        // optional: disable smoothing by setting smoothAlpha = 1f
        if (smoothedDelta <= 0f) smoothedDelta = dt;
        smoothedDelta += (dt - smoothedDelta) * smoothAlpha;

        // clamp to avoid division by zero
        float safeDelta = Mathf.Max(1e-6f, smoothedDelta);
        float fps = 1f / safeDelta;
        fpsText.text = $"FPS: {fps:0.0}";
    }
}
