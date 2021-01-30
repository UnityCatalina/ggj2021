using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenControl : MonoBehaviour
{
    public RenderTexture m_rt;
    public MeshRenderer m_meshRend;

    // Start is called before the first frame update
    void Awake()
    {
        m_rt = new RenderTexture(400, 300, 24);
        m_rt.Create();
        m_meshRend.material.SetTexture("_EmissionMap", m_rt, UnityEngine.Rendering.RenderTextureSubElement.Color);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
