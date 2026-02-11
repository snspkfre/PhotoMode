using System;
using UnityEngine;

[Serializable]
public abstract class BaseController : MonoBehaviour
{
    public abstract void Initialize();

    public abstract void ResetCamera(Transform cam);
    public abstract void UpdateBehavior(Transform cam);
}
