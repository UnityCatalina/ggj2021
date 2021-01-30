using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatInteraction : MonoBehaviour, IInteraction
{
    public Vector3 Location
    {
        get
        {
            return transform.position;
        }
    }

    public bool Available
    {
        get;
        set;
    }

    public void Reserve(GameObject Object)
    {
        Available = false;
    }

    public void DoAction(GameObject Object)
    {
        Available = false;
    }
}
