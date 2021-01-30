using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TransitionedPanel : MonoBehaviour
{
    public float m_transitionInSpeed = 2.0f;
    public float m_transitionOutSpeed = 1.0f;

    public TransitionConfig m_transitionConfig;

    [Tooltip("The transform you want to animate in/out.  Leave null to use the local transform")]
    [SerializeField] private RectTransform m_transitionTransform;

    [Tooltip("Whether the position will transition x and y based on local width/height or world (screen) width/height")]
    [SerializeField] private bool m_localSpace = false;

    [SerializeField] private bool m_shouldBeOn;
    Vector2 m_startWorldPosition;
    Vector2 m_startAnchoredPosition;
    Vector3 m_startLocalScale;
    CanvasGroup m_canvasGroup;
    bool m_initialised = false;
    float m_transition;

    public UnityEvent m_onTransitionedOnEvent;
    public UnityEvent m_onTransitionedOffEvent;
    public UnityEvent m_onStartTransitionOnEvent;
    public UnityEvent m_onStartTransitionOffEvent;


    void OnEnable()
    {
        if (m_transitionTransform == null)
        {
            m_transitionTransform = GetComponent<RectTransform>();
        }
        TransitionOn();
    }

    void Initialise()
    {
        if (!m_initialised)
        {
            if (m_transitionTransform == null)
            {
                m_transitionTransform = GetComponent<RectTransform>();
            }
            m_startWorldPosition = m_transitionTransform.position;
            m_startAnchoredPosition = m_transitionTransform.anchoredPosition;
            m_startLocalScale = m_transitionTransform.localScale;
            m_canvasGroup = m_transitionTransform.GetComponent<CanvasGroup>();
            m_initialised = true;
        }
    }

    public void SetShouldBeOn(bool shouldBeOn)
    {
        if (shouldBeOn != m_shouldBeOn)
        {
            if (shouldBeOn)
            {
                TransitionOn();
            }
            else
            {
                TransitionOff();
            }
        }
    }

    public void ToggleShouldBeOn()
    {
        SetShouldBeOn(!m_shouldBeOn);
    }

    public void TransitionOn()
    {
        Initialise();
        if (gameObject != null && !gameObject.activeSelf) // For some reason, WebGL can have a null gameobject (FML)
        {
            gameObject.SetActive(true);
        }
        if (!m_shouldBeOn)
        {
            m_shouldBeOn = true;
        }
        m_onStartTransitionOnEvent.Invoke();
        if (m_transition >= 1.0f) // If we're already transitioned fully on, then another TransitionOn should still trigger the onTransitionedOnEvent
        {
            m_onTransitionedOnEvent.Invoke();
        }
        UpdateTransitionInVisuals(m_transition);
    }
    public void TransitionOnWithReset()
    {
        m_transition = 0.0f;
        TransitionOn();
    }

    public void TransitionOff()
    {
        Initialise();
        if (m_shouldBeOn)
        {
            m_shouldBeOn = false;
            m_onStartTransitionOffEvent.Invoke();
        }
        UpdateTransitionOutVisuals(m_transition);
    }

    void UpdateTransitionInVisuals(float t)
    {
        if (m_transitionTransform == null)
        {
            m_transitionTransform = GetComponent<RectTransform>();
        }

        SetTransitionPosition(t, m_transitionConfig.m_xTransitionIn, m_transitionConfig.m_yTransitionIn);
        SetTransitionScale(t, m_transitionConfig.m_xScaleTransitionIn, m_transitionConfig.m_yScaleTransitionIn);
        SetTransitionAlpha(t, m_transitionConfig.m_alphaTransitionIn);
    }

    void UpdateTransitionOutVisuals(float t)
    {
        if (m_transitionTransform == null)
        {
            m_transitionTransform = GetComponent<RectTransform>();
        }
        t = 1.0f - t;

        SetTransitionPosition(t, m_transitionConfig.m_xTransitionOut, m_transitionConfig.m_yTransitionOut);
        SetTransitionScale(t, m_transitionConfig.m_xScaleTransitionOut, m_transitionConfig.m_yScaleTransitionOut);
        SetTransitionAlpha(t, m_transitionConfig.m_alphaTransitionOut);
    }

    void SetTransitionPosition(float t, AnimationCurve xTransitionCurve, AnimationCurve yTransitionCurve)
    {
        if (!m_transitionConfig.m_enablePositionTransition)
        {
            return;
        }
        Vector2 transitionPosition = new Vector2(xTransitionCurve.Evaluate(t), yTransitionCurve.Evaluate(t));
        if (m_localSpace)
        {
            //Rect screenRect = Utils.RectTransformToScreenSpace(m_transitionTransform);
            //width = screenRect.width;
            //height = screenRect.height;
            //width = m_transitionTransform.sizeDelta.x;
            //height = m_transitionTransform.sizeDelta.y;
            Vector2 scale =  m_transitionTransform.rect.size;
            m_transitionTransform.anchoredPosition = Vector2.Scale(transitionPosition, scale) + m_startAnchoredPosition;
        }
        else
        {
            Vector2 scale = new Vector2(Screen.width, Screen.height);
            m_transitionTransform.position =  Vector2.Scale(transitionPosition, scale) + m_startWorldPosition;
        }
    }
    void SetTransitionScale(float t, AnimationCurve xScaleTransitionCurve, AnimationCurve yScaleTransitionCurve)
    {
        if (!m_transitionConfig.m_enableScaleTransition)
        {
            return;
        }
        float xScale = xScaleTransitionCurve != null && xScaleTransitionCurve.length > 0 ? xScaleTransitionCurve.Evaluate(t) : 1.0f;
        float yScale = yScaleTransitionCurve != null && yScaleTransitionCurve.length > 0 ? yScaleTransitionCurve.Evaluate(t) : 1.0f;
        m_transitionTransform.localScale = Vector3.Scale(m_startLocalScale, new Vector3(xScale, yScale, 1.0f));
    }
    void SetTransitionAlpha(float t, AnimationCurve alphaTransitionCurve)
    {
        if (!m_transitionConfig.m_enableAlphaTransition)
        {
            return;
        }
        float alphaValue = alphaTransitionCurve.Evaluate(t);
        m_canvasGroup.alpha = alphaValue;
    }

    public bool IsTransitioningOn()
    {
        return m_shouldBeOn && m_transition < 1.0f;
    }
    public bool IsTransitioningOff()
    {
        return !m_shouldBeOn && m_transition > 0.0f;
    }

    public bool IsTransitionedOff()
    {
        return !m_shouldBeOn && m_transition <= 0.0f;
    }
    public bool IsTransitionedOn()
    {
        return m_shouldBeOn && m_transition >= 1.0f;
    }

    void Update()
    {
        if (m_shouldBeOn && m_transition < 1.0f)
        {
            m_transition += Time.unscaledDeltaTime * m_transitionInSpeed;
            if (m_transition >= 1.0f)
            {
                m_transition = 1.0f;
            }
            UpdateTransitionInVisuals(m_transition);

            if (m_transition >= 1.0f)
            {
                // Transition In Finished
                m_onTransitionedOnEvent.Invoke();
            }
        }
        else if (!m_shouldBeOn && m_transition > 0.0f)
        {
            m_transition -= Time.unscaledDeltaTime * m_transitionOutSpeed;
            if (m_transition <= 0.0f)
            {
                m_transition = 0.0f;
                gameObject.SetActive(false);
            }
            UpdateTransitionOutVisuals(m_transition);

            if (m_transition <= 0.0f)
            {
                // Transition Out Finished
                m_onTransitionedOffEvent.Invoke();
            }
        }
    }
}
