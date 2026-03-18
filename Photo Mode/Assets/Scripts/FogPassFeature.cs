using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.Util;
using System;

public class FogPassFeature : ScriptableRendererFeature
{
    class FogEffectPass : ScriptableRenderPass
    {
        const string m_PassName = "FogPass";
        Material m_FogMaterial;

        public void Setup(Material mat)
        {
            m_FogMaterial = mat;
            requiresIntermediateTexture = true;
        }
        

        // RecordRenderGraph is where the RenderGraph handle can be accessed, through which render passes can be added to the graph.
        // FrameData is a context container through which URP resources can be accessed and managed.
        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            var stack = VolumeManager.instance.stack;
            var fogSettings = stack.GetComponent<FogVolumeComponent>();

            //Only does the pass when the effect is active
            if (!fogSettings.IsActive())
                return;

            m_FogMaterial.SetColor("_FogColor", fogSettings.fogColor.value);
            m_FogMaterial.SetFloat("_NearIntensity", fogSettings.nearIntensity.value);
            m_FogMaterial.SetFloat("_FarIntensity", fogSettings.farIntensity.value);
            m_FogMaterial.SetFloat("_CamNear", fogSettings.camNear.value);
            m_FogMaterial.SetFloat("_CamFar", fogSettings.camFar.value);

            //access point to all the renderers texture handles
            var resourceData = frameData.Get<UniversalResourceData>();

            if(resourceData.isActiveTargetBackBuffer)
            {
                Debug.LogError($"Skipping render pass. fogEffectRendererFeature requires a ColorTexture," +
                               $" the backbuffer is not able to be used as a texture input.");
                return;
            }

            var source = resourceData.activeColorTexture;

            var destinationDesc = renderGraph.GetTextureDesc(source);
            destinationDesc.name = $"CameraColor-{m_PassName}";
            destinationDesc.clearBuffer = false;

            TextureHandle destination = renderGraph.CreateTexture(destinationDesc);

            RenderGraphUtils.BlitMaterialParameters para = new(source,  destination, m_FogMaterial, 0);
            renderGraph.AddBlitPass(para, passName: m_PassName);

            resourceData.cameraColor = destination;
        }
    }

    public RenderPassEvent injectionPoint = RenderPassEvent.AfterRenderingOpaques;
    public Material material;

    FogEffectPass m_ScriptablePass;

    /// <inheritdoc/>
    public override void Create()
    {
        m_ScriptablePass = new FogEffectPass();

        // Configures where the render pass should be injected.
        m_ScriptablePass.renderPassEvent = injectionPoint;
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (material == null)
        {
            Debug.LogWarning("No material on render pass");
            return;
        }

        if (renderingData.cameraData.camera != Camera.main)
            return;

        m_ScriptablePass.Setup(material);
        renderer.EnqueuePass(m_ScriptablePass);
    }
}