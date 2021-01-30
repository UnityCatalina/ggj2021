
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISpeedControl : MonoBehaviour
{
    public TextMeshProUGUI SpeedLabel;
    public TextMeshProUGUI TimeLabel;

    public void Update()
    {
        SpeedLabel.text = "Speed: " + SimulationManager.Instance.SimulationSpeed;
        TimeLabel.text = "Time: " + (int)SimulationManager.Instance.SimulationTime + "/" + (int)SimulationManager.Instance.Duration;
    }
}
