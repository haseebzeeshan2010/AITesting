using UnityEngine;

public class ResolutionController : MonoBehaviour
{
    private bool lowres = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResToggle()
    {
        lowres = !lowres;
        if (lowres)
        {
            // exclusive/fullscreen (may be platform-dependent)
            Screen.SetResolution(1280, 720, FullScreenMode.ExclusiveFullScreen);
            Debug.Log("Resolution set to 1280x720");
        }
        else
        {
            Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);
            Debug.Log("Resolution set to 1920x1080");
        }
    }
}