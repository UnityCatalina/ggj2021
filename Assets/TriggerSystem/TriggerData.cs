using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogLine
{
    public CharacterData m_character;
    [TextArea]
    public string m_line;
}

[System.Serializable]
public class TriggeredEvent
{
    public List<DialogLine> m_dialog;
    public List<string> m_camerasToUnlock;
}

public class TriggerData : MonoBehaviour
{
    public TriggeredEvent m_onTrigger;
}
