using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Suspect : MonoBehaviour
{
    public int id;
    public TriggerData Intro;
    public TriggerData Confirm;
    public TriggerData Result;
    
    public Light Light;

    int stage = 0;
    static int selected = -1;
    static bool IsFinished = false;

    public void OnMouseEnter()
    {
        if (!IsFinished)
        {
            Light.gameObject.SetActive(true);
        }
    }

    public void OnMouseExit()
    {
        if (!IsFinished)
        {
            Light.gameObject.SetActive(false);
        }
    }

    public void Update()
    {
        if (DialogueRunner.IsFinished() && IsFinished && selected == id)
        {
            Light[] Lights = FindObjectsOfType<Light>();
            foreach (Light light in Lights)
            {
                if (Light != light)
                {
                    light.gameObject.SetActive(false);
                }
                else
                {
                    Light.gameObject.SetActive(true);
                }
            }
        }
    }

    public void OnMouseDown()
    {
        if (DialogueRunner.IsFinished() && !IsFinished)
        {
            if (stage == 0)
            {
                DialogueRunner.StartDialogue(Intro);
            }

            if(stage == 2 && selected != id)
            {
                stage = 1;
            }

            if(stage == 1)
            {
                DialogueRunner.StartDialogue(Confirm);
                selected = id;
            }

            if(stage == 2)
            {
                DialogueRunner.StartDialogue(Result);
                Light.gameObject.SetActive(true);
                IsFinished = true;
            }

            stage++;
        }
    }
}
