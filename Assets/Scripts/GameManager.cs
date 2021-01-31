using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AgentSpawner Spawner;

    public SimulationManager SimulationManager;

    public Camera LoadingCamera;

    public TextMeshProUGUI LoadingLabel;

    IEnumerator Start()
    {
        yield return StartCoroutine(LoadScene("Airport_001"));

        Spawner.Spawn();

        while (!Input.anyKey)
        {
            yield return null;
        }

        LoadingCamera.gameObject.SetActive(false);
        LoadingLabel.GetComponentInParent<Canvas>().gameObject.SetActive(false);

        SimulationManager.RecordSimulation(4f, TimeLord.GetSequenceLength(), 0f);
    }

    IEnumerator LoadScene(string name)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            LoadingLabel.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";

            if(asyncOperation.progress >= .9f)
            {
                LoadingLabel.text = "Press any key to continue";
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

}
