using Chronos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationControl : MonoBehaviour
{
    List<int> speedIntervals = new List<int>{ -4, -2, -1, 0, 1, 2, 4 };
    int speedIndex = 4;

    void Update()
    {
        Clock clock = Chronos.Timekeeper.instance.Clock("Default");

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            speedIndex = Mathf.Max(speedIndex - 1, 0);
            clock.localTimeScale = speedIntervals[speedIndex];
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            speedIndex = Mathf.Min(speedIndex + 1, speedIntervals.Count - 1);
            clock.localTimeScale = speedIntervals[speedIndex];
        }

    }
}
