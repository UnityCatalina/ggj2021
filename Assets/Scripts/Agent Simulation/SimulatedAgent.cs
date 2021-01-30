using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimulatedAgent : MonoBehaviour, ISimulatedComponent
{
    NavMeshAgent navMeshAgentComponent;

    void Awake()
    {
        navMeshAgentComponent = GetComponent<NavMeshAgent>();
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
        Vector3 destination = new Vector3();
        destination.x = Random.Range(-100f, 100f);
        destination.z = Random.Range(-100f, 100f);
        navMeshAgentComponent.SetDestination(destination);
    }

    public void OnRecordSimulationFinished()
    {
        navMeshAgentComponent.isStopped = true;
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
}
