using UnityEngine;

public static class OrbitController
{
    public static void ResetCamera(Transform cam, Transform target)
    {
        cam.position = target.position + target.forward * cam.localScale.z * 10;
        cam.LookAt(target.position, Vector3.up);
    }

    public static void UpdateBehavior(Transform cam, Transform target)
    {
        if (!Input.GetMouseButton(1))
            return;
        float mouseX = Input.GetAxis("Mouse Y");
        float mouseY = -Input.GetAxis("Mouse X");
        Vector3 rotateValue = new Vector3(mouseX, mouseY, 0);

        cam.eulerAngles = cam.eulerAngles - rotateValue;

        cam.position = target.position - cam.forward * cam.localScale.z * 10;
    }
}
