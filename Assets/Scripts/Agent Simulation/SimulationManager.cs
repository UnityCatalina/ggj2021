using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public bool IsRecordingSimulation { get; private set; }

    public bool IsRunningSimulation { get; private set; }

    public float Duration { get; private set; }

    public float SimulationTime { get; private set; }

    public float SimulationSpeed { get; private set; }

    List<ISimulatedComponent> simulatedComponents = new List<ISimulatedComponent>();

    public float SimulationTimeStep = 0.02f;

    public static SimulationManager Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
        Time.timeScale = 0f;
    }

    public void RegisterSimulatedComponent(ISimulatedComponent Component)
    {
        if(simulatedComponents.Contains(Component))
        {
            return;
        }

        simulatedComponents.Add(Component);
    }

    public void UnregisterSimulatedComponent(ISimulatedComponent Component)
    {
        if (!simulatedComponents.Contains(Component))
        {
            return;
        }

        simulatedComponents.Remove(Component);
    }

    public void RecordSimulation(float speed, float duration)
    {
        IsRecordingSimulation = true;
        Duration = duration;
        SimulationTime = 0f;
        Time.timeScale = 1;
        Time.fixedDeltaTime = SimulationTimeStep;
        Time.captureDeltaTime = SimulationTimeStep * speed;

        foreach (ISimulatedComponent simulatedComponent in simulatedComponents)
        {
            simulatedComponent.OnRecordSimulationStarted();
        }
    }

    public void StopRecording()
    {
        IsRecordingSimulation = false;
        Time.timeScale = 0f;
        Time.captureDeltaTime = 0f;

        foreach (ISimulatedComponent simulatedComponent in simulatedComponents)
        {
            simulatedComponent.OnRecordSimulationFinished();
        }
    }

    public void RunSimulation(float time, float speed)
    {
        if(IsRecordingSimulation)
        {
            return;
        }

        SimulationTime = Mathf.Clamp(time, 0f, Duration);
        SimulationSpeed = speed; 
        IsRunningSimulation = true;
        Time.timeScale = 1f;

        foreach (ISimulatedComponent simulatedComponent in simulatedComponents)
        {
            simulatedComponent.OnRunSimulationStarted();
        }
    }

    public void SetSimulationTime(float time)
    {
        SimulationTime = Mathf.Clamp(time, 0f, Duration);
    }

    public void SetSimulationSpeed(float speed)
    {
        SimulationSpeed = speed;
    }

    public void Update()
    {
        if (IsRecordingSimulation)
        {
            foreach (ISimulatedComponent simulatedComponent in simulatedComponents)
            {
                simulatedComponent.Record(SimulationTime);
            }

            SimulationTime += Time.fixedDeltaTime;

            if (SimulationTime > Duration)
            {
                StopRecording();
            }
        }

        if (IsRunningSimulation)
        {
            SimulationTime += Time.deltaTime * SimulationSpeed;
            SimulationTime = Mathf.Clamp(SimulationTime, 0f, Duration);

            foreach (ISimulatedComponent simulatedComponent in simulatedComponents)
            {
                simulatedComponent.TickSimulation(SimulationTime);
            }
        }
    }
}
