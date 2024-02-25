using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AwesomeScreenShader : MonoBehaviour
{
    public Shader awesomeShader = null;
    public Text debugText;
    public List<Texture2D> palletes;
    private Material m_renderMaterial;
    private int idx = 0;


    void Start()
    {
        if (awesomeShader == null)
        {
            Debug.LogError("awesome shader not set");
            m_renderMaterial = null;
            return;
        }
        m_renderMaterial = new Material(awesomeShader);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (awesomeShader == null) return;

        Graphics.Blit(source, destination, m_renderMaterial);
    }

    private void Update()
    {
        float step = 0.01f;

        //lumenescence
        if (Input.GetKey(KeyCode.Comma))
        {
            var val = m_renderMaterial.GetFloat("_Lum");
            val -= step;
            m_renderMaterial.SetFloat("_Lum", val);
        }
        else if (Input.GetKey(KeyCode.Period))
        {
            var val = m_renderMaterial.GetFloat("_Lum");
            val += step;
            m_renderMaterial.SetFloat("_Lum", val);
        }
        //palletes
        else if (Input.GetKeyUp(KeyCode.LeftBracket))
        {
            idx--;
            if (idx < 0) idx = palletes.Count - 1;

            m_renderMaterial.SetTexture("_ColorRamp", palletes[idx]);
            debugText.text = palletes[idx].name;
        }
        else if (Input.GetKeyUp(KeyCode.RightBracket))
        {
            idx++;
            if (idx > palletes.Count - 1) idx = 0;

            m_renderMaterial.SetTexture("_ColorRamp", palletes[idx]);
            debugText.text = palletes[idx].name;
        }
    }
}
