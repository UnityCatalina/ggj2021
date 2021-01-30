using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip pressClip;
    MainCamControl mainCamControl;
    Camera mainCam;
    ScreenControl[] screenControls;
    float t;
    int playSpeed; // 0=pause, +/-1=forward/rev, +/-2=fast forward/rev
    bool draggingScrubber;

    // Start is called before the first frame update
    void Start()
    {
        mainCamControl = FindObjectOfType<MainCamControl>();
        mainCam = mainCamControl.GetComponent<Camera>();
        screenControls = FindObjectsOfType<ScreenControl>();
    }

    private Collider GetColliderAtMouse()
    {
        var ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (!Physics.Raycast(ray, out hitInfo))
            return null;
        return hitInfo.collider;
    }

    private void PlayPressSound()
    {
        audioSource.PlayOneShot(pressClip);
    }

    // Update is called once per frame
    void Update()
    {
        if (!draggingScrubber)
        {
            if (mainCamControl.activeScreen == null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    var collider = GetColliderAtMouse();
                    if (collider != null)
                        mainCamControl.activeScreen = collider.GetComponent<ScreenControl>();
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                var collider = GetColliderAtMouse();
                if (collider == mainCamControl.activeScreen.forwardButton.pressCollider)
                {
                    playSpeed = (playSpeed == 1) ? 0 : 1;
                    PlayPressSound();
                }
                else if (collider == mainCamControl.activeScreen.fastForwardButton.pressCollider)
                {
                    playSpeed = (playSpeed == 2) ? 0 : 2;
                    PlayPressSound();
                }
                else if (collider == mainCamControl.activeScreen.reverseButton.pressCollider)
                {
                    playSpeed = (playSpeed == -1) ? 0 : -1;
                    PlayPressSound();
                }
                else if (collider == mainCamControl.activeScreen.fastReverseButton.pressCollider)
                {
                    playSpeed = (playSpeed == -2) ? 0 : -2;
                    PlayPressSound();
                }
                else if ((collider == mainCamControl.activeScreen.scrubber.dragCollider) ||
                    (collider == mainCamControl.activeScreen.scrubber.scrubLineCollider))
                {
                    draggingScrubber = true;
                }
            }
            else if (Input.GetMouseButtonDown(1))
                mainCamControl.activeScreen = null;
        }

        if (draggingScrubber)
        {
            if (!Input.GetMouseButton(0))
                draggingScrubber = false;
            else
            {
                var ray = mainCam.ScreenPointToRay(Input.mousePosition);
                t = mainCamControl.activeScreen.scrubber.RayToTime(ray);
            }
        }

        if (!draggingScrubber)
        {
            float scaledTimeDelta = Time.deltaTime * playSpeed * (1.0f / 30.0f);
            t = Mathf.Clamp01(t + scaledTimeDelta);
        }

        foreach (var screenControl in screenControls)
            screenControl.SetState(t, playSpeed);
    }
}
