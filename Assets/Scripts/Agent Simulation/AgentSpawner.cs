using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentSpawner : MonoBehaviour
{
    public GameObject Agent;

    public int Amount;

    public bool SpawnOnStart = false;

    public void Start()
    {
        if(SpawnOnStart)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        if (Agent)
        {
            for (int i = 0; i < Amount; i++)
            {
                GameObject.Instantiate(Agent, NavMeshUtilities.GetRandomLocationOnNavMesh(), Quaternion.identity);
            }
        }
    }
}
