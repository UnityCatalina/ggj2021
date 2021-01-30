using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrubber : MonoBehaviour
{
    public Collider dragCollider;
    public BoxCollider scrubLineCollider;

    // Assume this stuff doesn't change!
    Vector3 scrubBeginPoint;
    Vector3 scrubLineVec;
    Plane scrubPlane;

    // Start is called before the first frame update
    void Start()
    {
        scrubBeginPoint = scrubLineCollider.transform.TransformPoint(
            scrubLineCollider.center + new Vector3(scrubLineCollider.size.x * 0.5f, 0.0f, 0.0f));
        scrubLineVec = scrubLineCollider.transform.TransformVector(
            new Vector3(-scrubLineCollider.size.x, 0.0f, 0.0f));
        scrubPlane = new Plane(scrubLineCollider.transform.forward, scrubBeginPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState(float t)
    {
        transform.position = scrubBeginPoint + (t * scrubLineVec);
    }

    public float RayToTime(Ray ray)
    {
        float enter;
        if (!scrubPlane.Raycast(ray, out enter))
            return 0.0f; // Shouldn't happen!
        var rayPoint = ray.GetPoint(enter);

        var scrubBeginToRay = rayPoint - scrubBeginPoint;
        return Mathf.Clamp01(Vector3.Dot(scrubBeginToRay, scrubLineVec) / scrubLineVec.sqrMagnitude);
    }
}
