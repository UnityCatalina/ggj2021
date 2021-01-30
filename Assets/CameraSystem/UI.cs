using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip pressClip;
    public MainCamControl mainCamControl;
    Camera mainCam;
    ScreenControl[] screenControls;

    RenderTexture bigRt;

    float t;
    int playSpeed; // 0=pause, +/-1=forward/rev, +/-2=fast forward/rev
    bool draggingScrubber;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = mainCamControl.GetComponent<Camera>();
        screenControls = FindObjectsOfType<ScreenControl>();
        
        // Assume screen res won't change...
        bigRt = new RenderTexture(
            Screen.currentResolution.width,
            Screen.currentResolution.height,
            24);
        bigRt.Create();
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

    private void SetActiveScreen(ScreenControl screenControl)
    {
        if (mainCamControl.activeScreen == screenControl)
            return;
        if (mainCamControl.activeScreen != null)
            mainCamControl.activeScreen.SetRt(null);
        mainCamControl.activeScreen = screenControl;
        if (screenControl != null)
            screenControl.SetRt(bigRt);
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
                        SetActiveScreen(collider.GetComponent<ScreenControl>());
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
                else if (Array.Exists(mainCamControl.activeScreen.exitColliders,
                    exitCollider => collider == exitCollider))
                {
                    SetActiveScreen(null);
                }
            }
            else if (Input.GetMouseButtonDown(1))
                SetActiveScreen(null);
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
            float scaledTimeDelta = Time.deltaTime * playSpeed * (1.0f / TimeLord.GetSequenceLength());
            t = Mathf.Clamp01(t + scaledTimeDelta);
        }

        foreach (var screenControl in screenControls)
            screenControl.SetState(t, playSpeed);

        // Tell Dr Who what time it is
        TimeLord.SetTime(t * TimeLord.GetSequenceLength());
    }
}
