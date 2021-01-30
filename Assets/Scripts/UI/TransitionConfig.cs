using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TransitionConfig : ScriptableObject
{
    public bool m_enablePositionTransition = true;
    [Tooltip("0 is the start of the fade in, 1 is the end of the fade in")]
    public AnimationCurve m_xTransitionIn;

    [Tooltip("0 is the start of the fade in, 1 is the end of the fade in")]
    public AnimationCurve m_yTransitionIn;


    [Tooltip("0 is the start of the fade out, 1 is the end of the fade out")]
    public AnimationCurve m_xTransitionOut;

    [Tooltip("0 is the start of the fade out, 1 is the end of the fade out")]
    public AnimationCurve m_yTransitionOut;

    public bool m_enableScaleTransition = true;
    public AnimationCurve m_xScaleTransitionIn;
    public AnimationCurve m_yScaleTransitionIn;
    public AnimationCurve m_xScaleTransitionOut;
    public AnimationCurve m_yScaleTransitionOut;

    public bool m_enableAlphaTransition = false;
    public AnimationCurve m_alphaTransitionIn;
    public AnimationCurve m_alphaTransitionOut;
}
