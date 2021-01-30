using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenControl : MonoBehaviour
{
    public RenderTexture rt;
    public MeshRenderer meshRend;

    public Scrubber scrubber;
    public ControlButton forwardButton;
    public ControlButton fastForwardButton;
    public ControlButton reverseButton;
    public ControlButton fastReverseButton;

    public Collider[] exitColliders;

    // Start is called before the first frame update
    void Awake()
    {
        rt = new RenderTexture(400, 300, 24);
        rt.Create();
        meshRend.material.SetTexture("_EmissionMap", rt, UnityEngine.Rendering.RenderTextureSubElement.Color);
    }

    // Update is called once per frame
    void Update()
    {

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
