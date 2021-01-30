using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenControl : MonoBehaviour
{
    RenderTexture defaultRt;
    public MeshRenderer meshRend;
    public ScreenCamera screenCamera;

    public BoxCollider screenCollider;
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
        if (screenCamera != null)
        {
            RenderTexture rt = (overrideRt == null) ? defaultRt : overrideRt;
            screenCamera.cam.targetTexture = rt;
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

    public Vector3 NormalisePoint(Vector3 point)
    {
        var localPoint = screenCollider.transform.InverseTransformPoint(point);
        var relCenterPoint = localPoint - screenCollider.center;
        return new Vector3(
            -relCenterPoint.x / screenCollider.size.x,
            relCenterPoint.y / screenCollider.size.y,
            relCenterPoint.z / screenCollider.size.z) + new Vector3(0.5f, 0.5f, 0.5f);
    }
}
