using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionRegistar : MonoBehaviour
{
    static List<IInteraction> Interactions = new List<IInteraction>();

    public static void Register(IInteraction interaction)
    {
        Interactions.Add(interaction);
    }

    public static void Unregister(IInteraction interaction)
    {
        Interactions.Remove(interaction);
    }

    public static IInteraction GetAvailableInteraction()
    {
        foreach(IInteraction interaction in Interactions)
        {
            if(interaction.IsAvailable())
            {
                return interaction;
            }
        }

        return null;
    }

    public void OnDestroy()
    {
        Interactions.Clear();
    }
}
