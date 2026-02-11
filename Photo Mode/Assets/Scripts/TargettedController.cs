using UnityEngine;

public abstract class TargettedController : BaseController
{
    public Transform target;

    public override void ResetCamera(Transform cam)
    {
        cam.position = target.position + target.forward * cam.localScale.z * 10;
        cam.LookAt(target.position, Vector3.up);
    }
}
