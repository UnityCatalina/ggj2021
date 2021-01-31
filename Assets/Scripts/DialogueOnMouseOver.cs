using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueOnMouseOver : MonoBehaviour
{
    public TriggerData TriggerData;

    public Light Light;



    public void OnMouseOver()
    {
        
    }

    public void OnMouseEnter()
    {
        Light.gameObject.SetActive(true);
    }

    public void OnMouseExit()
    {
        Light.gameObject.SetActive(false);
    }

    public void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        if (DialogueRunner.IsFinished())
        {

            Debug.Log("IsFinished");
            DialogueRunner.StartDialogue(TriggerData);
            Light.gameObject.SetActive(true);
        }
    }
}
