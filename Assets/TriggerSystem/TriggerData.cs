using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StoryTrigger")]
public class TriggerData : ScriptableObject
{
    public string m_cameraName;
    public float m_timeStart;
    public float m_timeEnd;
}
