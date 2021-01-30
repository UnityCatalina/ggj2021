using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCamera : MonoBehaviour
{
    public string m_name;
    public Camera cam;
    public float interpSpeed;
    Matrix4x4 defaultProjMatrix;
    Matrix4x4 targetProjMatrix;

    // Start is called before the first frame update
    void Start()
    {
        // Assume this doesn't change...
        targetProjMatrix = defaultProjMatrix = cam.projectionMatrix;
    }

    // Update is called once per frame
    void Update()
    {
        var t = Mathf.Pow(1.0f - interpSpeed, Time.deltaTime);
        Matrix4x4 projMatrix = cam.projectionMatrix;
        for (int i = 0; i != 4; ++i)
            projMatrix.SetRow(i, Vector4.Lerp(
                targetProjMatrix.GetRow(i),
                projMatrix.GetRow(i),
                t));
        cam.projectionMatrix = projMatrix;
    }

    public void ResetView()
    {
        targetProjMatrix = defaultProjMatrix;
    }

    public void SetView(float scale, Vector3 normCenter)
    {
        Vector3 clampedCenter = new Vector3(
            Mathf.Clamp(normCenter.x, 0.5f / scale, 1.0f - (0.5f / scale)),
            Mathf.Clamp(normCenter.y, 0.5f / scale, 1.0f - (0.5f / scale)),
            0.0f);
        Vector3 ndcCenter = (clampedCenter * 2.0f) - new Vector3(1.0f, 1.0f, 0.0f);
        targetProjMatrix =
            Matrix4x4.Scale(new Vector3(scale, scale, 1.0f)) *
            Matrix4x4.Translate(-ndcCenter) *
            defaultProjMatrix;
    }

    public RaycastHit? Raycast(Vector3 normPoint)
    {
        var ray = cam.ViewportPointToRay(normPoint);
        RaycastHit throughHit;
        if (!Physics.Raycast(ray, out throughHit))
            return null;
        return throughHit;
    }
}
