using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlButton : MonoBehaviour
{
    public Collider pressCollider;
    float upZ;
    public float downZ;

    // Start is called before the first frame update
    void Start()
    {
        upZ = transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState(bool down)
    {
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            transform.localPosition.y,
            down ? downZ : upZ);
    }
}
