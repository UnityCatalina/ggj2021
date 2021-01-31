using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteraction
{
    Vector3 GetLocation();

    bool IsAvailable();

    void Reserve(GameObject Object);

    void DoAction(GameObject Object);

    bool IsControlling(GameObject Object);
}