using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatedTransform : MonoBehaviour, ISimulatedComponent
{
    public struct TransformRecord
    {
        public float time;

        public Vector3 position;

        public Quaternion rotation;

        public Vector3 scale;
    }

    List<TransformRecord> records = new List<TransformRecord>();

    public void OnEnable()
    {
        SimulationManager.Instance.RegisterSimulatedComponent(this);
    }

    public void OnDisable()
    {
        SimulationManager.Instance.UnregisterSimulatedComponent(this);
    }

    public void Record(float simulationTime)
    {
        TransformRecord record = new TransformRecord();
        record.time = simulationTime;
        record.position = transform.position;
        record.rotation = transform.rotation;
        record.scale = transform.localScale;
        records.Add(record);
    }

    public void TickSimulation(float simulationTime)
    {
        if (records.Count > 0)
        {
            TransformRecord first = records[0];
            TransformRecord last = records[records.Count - 1];

            int index = Mathf.FloorToInt(((simulationTime - first.time) / (last.time - first.time)) * records.Count);

            if (index + 1 <= records.Count - 1)
            {
                TransformRecord from = records[index];
                TransformRecord to = records[index + 1];

                float alpha = (simulationTime - from.time) / (to.time - from.time);
                transform.position = Vector3.Lerp(from.position, to.position, alpha);
                transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, alpha);
                transform.localScale = Vector3.Lerp(from.scale, to.scale, alpha);

            }
            else if(index <= records.Count - 1)
            {
                TransformRecord current = records[index];
                transform.position = current.position;
                transform.rotation = current.rotation;
                transform.localScale = current.scale;

            }
        }
    }

    public void OnRecordSimulationStarted()
    {

    }

    public void OnRecordSimulationFinished()
    {
    
    }

    public void OnRunSimulationStarted()
    {
 
    }

    public void OnRunSimulationFinished()
    {

    }
}
