using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamControl : MonoBehaviour
{
    public Camera m_camera;
    Vector3 m_defaultPos;
    Quaternion m_defaultRotation;
    ScreenControl m_activeScreen;
    public float m_screenDistance;
    public float m_interpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        m_defaultPos = m_camera.transform.position;
        m_defaultRotation = m_camera.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = m_camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                m_activeScreen = hitInfo.collider.GetComponent<ScreenControl>();
            }
            else
                m_activeScreen = null;
        }
        else if (Input.GetMouseButtonDown(1))
            m_activeScreen = null;

        var pos = m_defaultPos;
        var rotation = m_defaultRotation;
        if (m_activeScreen != null)
        {
            pos = m_activeScreen.transform.position + (m_activeScreen.transform.forward * m_screenDistance);
            rotation = m_activeScreen.transform.rotation * Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }

        var t = Mathf.Pow(1.0f - m_interpSpeed, Time.deltaTime);
        m_camera.transform.position = Vector3.Lerp(pos, m_camera.transform.position, t);
        m_camera.transform.rotation = Quaternion.Slerp(rotation, m_camera.transform.rotation, t);
    }
}
