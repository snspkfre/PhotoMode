using UnityEngine;

public class FreeroamController : TargettedController
{
    public override void Initialize() { }

    public override void UpdateBehavior(Transform cam)
    {
        if (!Input.GetMouseButton(1))
            return;
        float mouseX = Input.GetAxis("Mouse Y");
        float mouseY = -Input.GetAxis("Mouse X");
        Vector3 rotateValue = new Vector3(mouseX, mouseY, 0);

        cam.eulerAngles = cam.eulerAngles - rotateValue;

        float moveR = Input.GetAxis("Horizontal");
        float moveF = Input.GetAxis("Vertical");
        float moveU = (Input.GetKey(KeyCode.Q) ? 0 : 1) + (Input.GetKey(KeyCode.E) ? 0 : -1);

        cam.position += moveR * cam.right + moveF * cam.forward + moveU * cam.up;
    }
}