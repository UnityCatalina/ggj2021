using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SeatInteraction : MonoBehaviour, IInteraction, ISimulatedComponent
{
    public Vector2 IdleDuration = new Vector2(5f, 30f);

    public Vector2 InitialCooldownDuration = new Vector2(-10f, 30f);

    public Vector2 CooldownDuration = new Vector2(5f, 30f);

    bool isReserved = false;

    GameObject User = null;

    float idle = 0;

    float cooldown = 0;

    Vector3 Location;

    public void OnEnable()
    {

        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 100, -1))
        {
            Location = hit.position;
        }

        InteractionRegistar.Register(this);
    }

    public void OnDisable()
    {
        InteractionRegistar.Unregister(this);
    }

    public Vector3 GetLocation()
    {
        return Location;
    }

    public bool IsAvailable()
    {
        return !isReserved && cooldown <= 0f;
    }

    public void Reserve(GameObject Object)
    {
        isReserved = true;
    }

    public void DoAction(GameObject Object)
    {
        User = Object;
        idle = Random.Range(IdleDuration.x, IdleDuration.y);
    }

    public bool IsControlling(GameObject Object)
    {
        if(User == Object)
        {
            return true;
        }

        return false;
    }

    public void Update()
    {
        if(idle > 0f)
        {
            idle -= Time.deltaTime;
            
            if(User)
            {
                User.transform.position = transform.position;
                User.transform.rotation = transform.rotation;
            }

            if(idle <= 0f)
            {
                User = null;
                isReserved = false;
                cooldown = Random.Range(CooldownDuration.x, CooldownDuration.y);
            }
        }

        if(cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
        }
    }

    public void OnRecordSimulationStarted()
    {
  
    }

    public void OnRecordSimulationFinished()
    {
        enabled = false;
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
