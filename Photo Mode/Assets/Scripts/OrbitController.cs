using UnityEngine;

public class OrbitController : TargettedController
{
    public override void Initialize(){ }

    public override void UpdateBehavior(Transform cam)
    {
        if (!Input.GetMouseButton(1))
            return;
        float mouseY = -Input.GetAxis("Mouse X");
        float mouseX = Input.GetAxis("Mouse Y");
        Vector3 rotateValue = new Vector3(mouseX, mouseY, 0);

        cam.eulerAngles = cam.eulerAngles - rotateValue;

        cam.position = target.position - cam.forward * cam.localScale.z * 10;
    }
}
