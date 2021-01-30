using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Mode
{
    Normal,
    DraggingScrubber,
    Enhancing,
    RunningDialogue
}

public class UI : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip pressClip;
    public MainCamControl mainCamControl;
    public Transform debugTransform;
    ScreenControl[] screenControls;

    RenderTexture bigRt;

    float t;
    int playSpeed; // 0=pause, +/-1=forward/rev, +/-2=fast forward/rev
    Mode mode;
    // Valid iff mode=Enhancing or mode=RunningDialogue
    float enhanceTime;
    TriggerData enhanceTrigger;

    // Start is called before the first frame update
    void Start()
    {
        screenControls = FindObjectsOfType<ScreenControl>();
        
        // Assume screen res won't change...
        bigRt = new RenderTexture(
            Screen.currentResolution.width,
            Screen.currentResolution.height,
            24);
        bigRt.Create();
    }

    private RaycastHit? RaycastMouse()
    {
        var ray = mainCamControl.cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit))
            return null;
        return hit;
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
        switch (mode)
        {
            case Mode.Normal:
                if (mainCamControl.activeScreen == null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        var maybeHit = RaycastMouse();
                        if (maybeHit is RaycastHit hit)
                            SetActiveScreen(hit.collider.GetComponent<ScreenControl>());
                    }
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    var maybeHit = RaycastMouse();
                    if (maybeHit is RaycastHit hit)
                    {
                        if (hit.collider == mainCamControl.activeScreen.forwardButton.pressCollider)
                        {
                            playSpeed = (playSpeed == 1) ? 0 : 1;
                            PlayPressSound();
                        }
                        else if (hit.collider == mainCamControl.activeScreen.fastForwardButton.pressCollider)
                        {
                            playSpeed = (playSpeed == 2) ? 0 : 2;
                            PlayPressSound();
                        }
                        else if (hit.collider == mainCamControl.activeScreen.reverseButton.pressCollider)
                        {
                            playSpeed = (playSpeed == -1) ? 0 : -1;
                            PlayPressSound();
                        }
                        else if (hit.collider == mainCamControl.activeScreen.fastReverseButton.pressCollider)
                        {
                            playSpeed = (playSpeed == -2) ? 0 : -2;
                            PlayPressSound();
                        }
                        else if ((hit.collider == mainCamControl.activeScreen.scrubber.dragCollider) ||
                            (hit.collider == mainCamControl.activeScreen.scrubber.scrubLineCollider))
                        {
                            mode = Mode.DraggingScrubber;
                        }
                        else if (Array.Exists(mainCamControl.activeScreen.exitColliders,
                            exitCollider => hit.collider == exitCollider))
                        {
                            SetActiveScreen(null);
                        }
                        else if ((hit.collider == mainCamControl.activeScreen.screenCollider) &&
                            (mainCamControl.activeScreen.screenCamera != null))
                        {
                            var normPoint = mainCamControl.activeScreen.NormalisePoint(hit.point);
                            mainCamControl.activeScreen.screenCamera.SetView(3.0f, normPoint);
                            mode = Mode.Enhancing;
                            enhanceTime = 0.0f;
                            enhanceTrigger = null;
                            var maybeThroughHit = mainCamControl.activeScreen.screenCamera.Raycast(normPoint);
                            if (maybeThroughHit is RaycastHit throughHit)
                            {
                                enhanceTrigger = throughHit.collider.GetComponent<TriggerData>();
                                debugTransform.position = throughHit.point;
                            }
                        }
                    }
                }
                else if (Input.GetMouseButtonDown(1))
                    SetActiveScreen(null);
                break;
            case Mode.DraggingScrubber:
                if (!Input.GetMouseButton(0))
                    mode = Mode.Normal;
                else
                {
                    var ray = mainCamControl.cam.ScreenPointToRay(Input.mousePosition);
                    t = mainCamControl.activeScreen.scrubber.RayToTime(ray);
                }
                break;
            case Mode.Enhancing:
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                {
                    mainCamControl.activeScreen.screenCamera.ResetView();
                    mode = Mode.Normal;
                }
                else
                {
                    enhanceTime += Time.deltaTime;
                    if ((enhanceTime >= 0.5f) && (enhanceTrigger != null))
                    {
                        DialogueRunner.StartDialogue(enhanceTrigger);
                        enhanceTrigger = null;
                        mode = Mode.RunningDialogue;
                    }
                }
                break;
            case Mode.RunningDialogue:
                if (DialogueRunner.IsFinished())
                    mode = Mode.Enhancing;
                break;
        }

        if (mode == Mode.Normal)
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
