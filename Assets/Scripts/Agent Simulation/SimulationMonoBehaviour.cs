using UnityEngine;
using Chronos;

public class SimulationBehaviour : MonoBehaviour
{
    public Timeline SimulationTime
    {
        get
        {
            return GetComponent<Timeline>();
        }
    }
}