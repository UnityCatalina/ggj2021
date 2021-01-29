using Chronos;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISpeedControl : MonoBehaviour
{
    TextMeshProUGUI label;

    public void Awake()
    {
        label = GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
        Clock clock = Chronos.Timekeeper.instance.Clock("Default");

        label.text = "Speed: " + clock.localTimeScale;
    }
}
