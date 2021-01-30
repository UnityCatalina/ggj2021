using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimulatedAgent : MonoBehaviour, ISimulatedComponent
{
    public Vector2 AgentSpeedMultiplier = new Vector2(0.5f, 3f);

    NavMeshAgent navMeshAgentComponent;

    bool recording;

    float idle = 0;

    void Awake()
    {
        navMeshAgentComponent = GetComponent<NavMeshAgent>();

        float Multiplier = Random.Range(AgentSpeedMultiplier.x, AgentSpeedMultiplier.y);
        navMeshAgentComponent.speed *= Multiplier;
        navMeshAgentComponent.angularSpeed *= Multiplier;
        navMeshAgentComponent.acceleration *= Multiplier;

        Vector3 destination = NavMeshUtilities.GetRandomLocationOnNavMesh();
        navMeshAgentComponent.SetDestination(destination);
        navMeshAgentComponent.isStopped = true;
    }

    public void OnEnable()
    {
        SimulationManager.Instance.RegisterSimulatedComponent(this);
    }

    public void OnDisable()
    {
        SimulationManager.Instance.UnregisterSimulatedComponent(this);
    }

    public void OnRecordSimulationStarted()
    {
        recording = true;
    }

    public void OnRecordSimulationFinished()
    {
        navMeshAgentComponent.isStopped = true;
        recording = false;
    }

    public void OnRunSimulationStarted()
    {

    }

    public void OnRunSimulationFinished()
    {
 
    }

    public void Record(float simulationTime)
    {

    }

    public void TickSimulation(float simulationTime)
    {

    }

    public void Update()
    {
        if (recording)
        {
            if (!navMeshAgentComponent.pathPending)
            {
                if (navMeshAgentComponent.remainingDistance < 1f && idle <= 0)
                {
                    idle = Random.Range(0f, 10f);
                    navMeshAgentComponent.isStopped = true;
                }
                else if (idle > 0)
                {
                    idle -= Time.deltaTime;
                }

                if (navMeshAgentComponent.isStopped && idle <= 0)
                {
                    Vector3 destination = NavMeshUtilities.GetRandomLocationOnNavMesh();
                    navMeshAgentComponent.SetDestination(destination);
                    navMeshAgentComponent.isStopped = false;
                }
            }
        }
    }


}
