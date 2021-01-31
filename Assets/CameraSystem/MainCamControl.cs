using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamControl : MonoBehaviour
{
    public Camera cam;
    Vector3 defaultPos;
    Quaternion defaultRotation;
    public ScreenControl activeScreen;
    public float screenDistance;
    public float interpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        defaultPos = cam.transform.position;
        defaultRotation = cam.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = defaultPos;
        var rotation = defaultRotation;
        if (activeScreen != null)
        {
            pos = activeScreen.transform.position + (activeScreen.transform.forward * screenDistance);
            rotation = activeScreen.transform.rotation * Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }

        var t = Mathf.Pow(1.0f - interpSpeed, Time.unscaledDeltaTime);
        cam.transform.position = Vector3.Lerp(pos, cam.transform.position, t);
        cam.transform.rotation = Quaternion.Slerp(rotation, cam.transform.rotation, t);
    }
}
