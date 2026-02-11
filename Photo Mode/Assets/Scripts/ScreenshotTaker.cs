using System;
using UnityEngine;

public class ScreenshotTaker : MonoBehaviour
{
    [SerializeField] string _gameName = "GameName";

    [ContextMenu("ScreenCap")]
    public void CaptureScreen()
    {
        DateTime currentTime = DateTime.Now;
        string scName = _gameName + currentTime.ToString("_yyyy-MM-dd-HH-mm-ss") + ".png";
        ScreenCapture.CaptureScreenshot(scName, 2);
    }
}
