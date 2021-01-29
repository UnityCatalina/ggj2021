using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    public GameObject Agent;

    public int Amount;

    public void Start()
    {
        if (Agent)
        {
            for (int i = 0; i < Amount; i++)
            {
                Vector3 location = new Vector3();
                location.x = Random.Range(-100f, 100f);
                location.z = Random.Range(-100f, 100f);

                GameObject.Instantiate(Agent, location, Quaternion.identity);
            }
        }
    }
}
