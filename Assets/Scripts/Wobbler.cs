using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobbler : MonoBehaviour
{
    Vector3 m_startPosition;
    public Vector3 m_translationWobble;
    public Vector3 m_translationWobbleFrequence;
    Quaternion m_startRotation;
    public Vector3 m_rotationWobble;
    public Vector3 m_rotationWobbleFrequence;
    public float m_minSpeedWobble = 0.1f;
    public float m_maxSpeedWobble = 1.0f;

    Vector3 m_lastParentPosition;
    float m_lastParentSampleTime;
    float m_lastSpeed;

    void Start()
    {
        m_startPosition = transform.localPosition;
        m_startRotation = transform.localRotation;
    }

    void Update()
    {
        float time = TimeLord.GetTime();
        Vector3 newParentPos = transform.parent.position;
        float timeDiff = time - m_lastParentSampleTime;
        if (timeDiff != 0.0f)
        {
            float speed = Vector3.Magnitude(newParentPos - m_lastParentPosition) / timeDiff;
            m_lastParentPosition = newParentPos;
            m_lastParentSampleTime = time;
            m_lastSpeed = speed;
        }
       float magnitude = Mathf.Clamp(Mathf.Abs(m_lastSpeed), m_minSpeedWobble, m_maxSpeedWobble);

        Vector3 wobbleWave = new Vector3(
            Mathf.Sin(time * m_translationWobbleFrequence.x),
            Mathf.Sin(time * m_translationWobbleFrequence.y),
            Mathf.Sin(time * m_translationWobbleFrequence.z)) * magnitude;

        transform.localPosition = m_startPosition + Vector3.Scale(m_translationWobble, wobbleWave);
        Vector3 rotationWobbleWave = new Vector3(
            Mathf.Sin(time * m_rotationWobbleFrequence.x),
            Mathf.Sin(time * m_rotationWobbleFrequence.y),
            Mathf.Sin(time * m_rotationWobbleFrequence.z)) * magnitude;
        transform.localRotation = m_startRotation * Quaternion.Euler(Vector3.Scale(m_rotationWobble, rotationWobbleWave));
    }
}
