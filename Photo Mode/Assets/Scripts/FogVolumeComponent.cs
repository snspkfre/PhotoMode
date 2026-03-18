using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
[Serializable]
[VolumeComponentMenu("Custom/Fog")]
public class FogVolumeComponent : VolumeComponent, IPostProcessComponent
{
    public ColorParameter fogColor = new(Color.blue);
    public ClampedFloatParameter nearIntensity = new(1, 0, 1);
    public ClampedFloatParameter farIntensity = new(1, 0, 1);
    public FloatParameter camNear = new(5);
    public FloatParameter camFar = new(1000);

    public bool IsActive() => nearIntensity.value != farIntensity.value;
}