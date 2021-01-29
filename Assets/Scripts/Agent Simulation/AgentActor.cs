using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentActor : SimulationBehaviour
{
    NavMeshAgent navMeshAgentComponent;
    
    void Awake()
    {
        navMeshAgentComponent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Vector3 destination = new Vector3();
        destination.x = Random.Range(-100f, 100f);
        destination.z = Random.Range(-100f, 100f);
        //navMeshAgentComponent.SetDestination(destination);
        SimulationTime.navMeshAgent.component.SetDestination(destination);
        Debug.Log(destination);
    }

    void Update()
    {
        
    }
}
