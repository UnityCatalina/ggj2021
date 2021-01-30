using System.Collections.Generic;
using UnityEngine;

public class SimulationControl : MonoBehaviour
{
    List<int> speedIntervals = new List<int>{ -4, -2, -1, 0, 1, 2, 4 };
    int speedIndex = 4;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SimulationManager.Instance.RecordSimulation(4f, 160f, 2f);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SimulationManager.Instance.RunSimulation(0f, 1f);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            speedIndex = Mathf.Max(speedIndex - 1, 0);
            SimulationManager.Instance.SetSimulationSpeed(speedIntervals[speedIndex]);
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            speedIndex = Mathf.Min(speedIndex + 1, speedIntervals.Count - 1);
            SimulationManager.Instance.SetSimulationSpeed(speedIntervals[speedIndex]);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SimulationManager.Instance.SetSimulationTime(SimulationManager.Instance.SimulationTime + 10f);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SimulationManager.Instance.SetSimulationTime(SimulationManager.Instance.SimulationTime - 10f);
        }
    }

}
