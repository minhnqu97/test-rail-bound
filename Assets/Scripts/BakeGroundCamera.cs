using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using RenderPipeline = UnityEngine.Rendering.RenderPipelineManager;

public class BakeGroundCamera : MonoBehaviour
{
    [SerializeField] Camera m_groundCamera;
    [SerializeField] MeshRenderer m_defaultGroundMeshRenderer;
    [SerializeField] RenderTexture m_bakedGroundTexture;
    [SerializeField] MeshRenderer m_finalGroundRenderer;

    public void Start()
    {
        m_bakedGroundTexture = new RenderTexture(2 * Screen.width, 2 * Screen.height, 2, RenderTextureFormat.ARGB32);
        m_groundCamera.targetTexture = m_bakedGroundTexture;
        m_finalGroundRenderer.sharedMaterial.SetTexture("_MainTex", m_bakedGroundTexture);
        DoBakeGround();
    }

    [MyBox.ButtonMethod] 
    public void DoBakeGround()
    {
        m_defaultGroundMeshRenderer.transform.localScale = new Vector3(-4, 4, 4);
        RenderPipeline.beginCameraRendering += UpdateCamera;
    }

    void UpdateCamera(ScriptableRenderContext context, Camera camera)
    {
        UniversalRenderPipeline.RenderSingleCamera(context, m_groundCamera);
        m_defaultGroundMeshRenderer.transform.localScale = new Vector3(4, 4, 4);
        m_groundCamera.gameObject.SetActive(false);
        m_defaultGroundMeshRenderer.gameObject.SetActive(false);
        RenderPipeline.beginCameraRendering -= UpdateCamera;
    }

}
