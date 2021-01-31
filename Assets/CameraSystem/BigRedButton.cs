using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigRedButton : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip pressClip;
    public MainCamControl mainCamControl;
    ScreenControl[] screenControls;

    // Start is called before the first frame update
    void Start()
    {
        screenControls = FindObjectsOfType<ScreenControl>();
    }

    private RaycastHit? RaycastMouse()
    {
        var ray = mainCamControl.cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit))
            return null;
        return hit;
    }

    private void PlayPressSound()
    {
        audioSource.PlayOneShot(pressClip);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var maybeHit = RaycastMouse();
            if (maybeHit is RaycastHit hit)
            {
                Debug.Log("Hit!");
                PlayPressSound();
                StartCoroutine("nextScene");
            }
        }
    }

    private IEnumerator nextScene()
    {
       yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}
