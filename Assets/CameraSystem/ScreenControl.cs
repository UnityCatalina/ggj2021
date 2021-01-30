using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenControl : MonoBehaviour
{
    RenderTexture defaultRt;
    public MeshRenderer meshRend;
    public Camera cam;

    public Scrubber scrubber;
    public ControlButton forwardButton;
    public ControlButton fastForwardButton;
    public ControlButton reverseButton;
    public ControlButton fastReverseButton;

    public Collider[] exitColliders;

    // Start is called before the first frame update
    void Awake()
    {
        defaultRt = new RenderTexture(400, 300, 24);
        defaultRt.Create();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRt(RenderTexture overrideRt)
    {
        if (cam != null)
        {
            RenderTexture rt = (overrideRt == null) ? defaultRt : overrideRt;
            cam.targetTexture = rt;
            meshRend.material.SetTexture("_EmissionMap", rt, UnityEngine.Rendering.RenderTextureSubElement.Color);
        }
    }

    public void SetState(float t, int playSpeed)
    {
        scrubber.SetState(t);
        forwardButton.SetState(playSpeed == 1);
        fastForwardButton.SetState(playSpeed == 2);
        reverseButton.SetState(playSpeed == -1);
        fastReverseButton.SetState(playSpeed == -2);
    }
}
