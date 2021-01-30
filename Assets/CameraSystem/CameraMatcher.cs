using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraMatcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var screenControls = FindObjectsOfType<ScreenControl>();
        var screenCams = FindObjectsOfType<ScreenCamera>();
        foreach (var (screenControl, screenCam) in screenControls.Zip(screenCams,
                (screenControl, screenCam) => (screenControl, screenCam)))
        {
            screenControl.screenCamera = screenCam;
            screenControl.SetRt(null);
            screenCam.cam.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
