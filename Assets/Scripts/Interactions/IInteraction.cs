using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteraction
{
    Vector3 Location { get; }

    bool Available { get; set;  }

    void Reserve(GameObject Object);

    void DoAction(GameObject Object);
}
