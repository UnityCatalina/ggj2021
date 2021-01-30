using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLord : MonoBehaviour
{
    // For debugging in inspector
    public float m_timeSeconds = 0.0f;
    public bool m_isPlaying = false;

    PlayableDirector[] m_directors;

    static TimeLord s_instance;

    void Awake()
    {
        m_directors = FindObjectsOfType<PlayableDirector>();
        s_instance = this;
        Debug.Log("Sequence length = " + GetSequenceLength());
    }

    public static void SetTime(float timeSeconds)
    {
        if (s_instance != null)
        {
            s_instance.m_timeSeconds = timeSeconds;
        }
    }
    public static float GetSequenceLength()
    {
        return 60.0f * 4.0f;
    }

    void Update()
    {
        if (m_isPlaying)
        {
            m_timeSeconds += Time.deltaTime;
        }
        UpdateDirectors();
    }

    void UpdateDirectors()
    {
        foreach(PlayableDirector director in m_directors)
        {
            if (director.state == PlayState.Paused)
            {
                // this will call RebuildGraph if needed
                director.Play();
                // will set the speed of the graph to 0, so it's playing but never advancing
                int count = director.playableGraph.GetRootPlayableCount();
                for (int i = 0; i < count; i++)
                {
                    director.playableGraph.GetRootPlayable(i).SetSpeed(0);
                }
            }

            director.time = m_timeSeconds;
        }
    }
}
