using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineupController : MonoBehaviour
{
    public TriggerData InitialDialogue;


    void Start()
    {
        if (InitialDialogue)
        {
            DialogueRunner.StartDialogue(InitialDialogue);
        }
    }


}
