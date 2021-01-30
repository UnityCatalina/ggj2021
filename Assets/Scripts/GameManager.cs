using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AgentSpawner Spawner;

    public SimulationManager SimulationManager;

    void Start()
    {
        Spawner.Spawn();
        SimulationManager.RecordSimulation(4f, 120f, 0f);
    }

}
