using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueRunner : MonoBehaviour
{
    public Image m_background;
    public TransitionedPanel m_dialogPanel;
    public TMPro.TextMeshProUGUI m_text;
    public Image m_bubbleImage;
    public TriggerData m_testTrigger;
    public bool m_test;

    bool m_isRunning = false;
    int m_currentLine = 0;
    TriggerData m_currentDialog;
    CharacterData m_lastCharacter;
    bool m_bubbleFlip = false;

    static DialogueRunner s_instance;

    private void Awake()
    {
        s_instance = this;
    }

    public static void StartDialogue(TriggerData trigger)
    {
        if (s_instance == null)
        {
            Debug.LogError("No DialogueRunner in scene!");
        }

        s_instance.StartDialogueInner(trigger);
    }


    void StartDialogueInner(TriggerData trigger)
    {
        if (trigger.m_onTrigger.m_dialog.Count <= 0)
        {
            Debug.LogError("No dialog!");
            return;
        }
        m_currentDialog = trigger;
        m_isRunning = true;
        m_background.gameObject.SetActive(true);
        OpenLine(0);
    }
    void OpenLine(int i)
    {
        if (i >= m_currentDialog.m_onTrigger.m_dialog.Count)
        {
            m_isRunning = false;
            m_background.gameObject.SetActive(false);
            return;
        }
        DialogLine line = m_currentDialog.m_onTrigger.m_dialog[i];
        string text = "";
        Color colour = Color.black;
        if (line.m_character != null)
        {
            text = line.m_character.m_name + ": ";
            colour = line.m_character.m_textColour;
            if (line.m_character != m_lastCharacter)
            {
                m_bubbleFlip = !m_bubbleFlip;
                m_lastCharacter = line.m_character;
            }
        }
        text += line.m_line;

        m_bubbleImage.transform.localScale = new Vector3((m_bubbleFlip ? -1.0f :  1.0f), 1.0f, 1.0f);

        m_text.color = colour;
        m_text.text = text;
        m_dialogPanel.TransitionOn();
        m_currentLine = i;
    }

    void Update()
    {
        if (!m_isRunning)
        {
            if (m_test && m_testTrigger != null)
            {
                m_test = false;
                StartDialogue(m_testTrigger);
            }
            return;
        }
        if (m_dialogPanel.IsTransitionedOn() && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
        {
            // skip text?
            m_dialogPanel.TransitionOff();
        }

        if (m_dialogPanel.IsTransitionedOff())
        {
            OpenLine(m_currentLine + 1);
        }
    }

    public static bool IsFinished()
    {
        if (s_instance == null)
        {
            return true;
        }
        return !s_instance.m_isRunning;
    }
}
