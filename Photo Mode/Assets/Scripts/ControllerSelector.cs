using System.Collections.Generic;
using UnityEngine;

public class ControllerSelector : MonoBehaviour
{
    [SerializeField] List<BaseController> Controllers = new List<BaseController>();
    int currentController = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        SetupController();
    }

    [ContextMenu("SwapControls")]
    void SwitchToNextController()
    {
        currentController++;
        currentController %= Controllers.Count;
        SetupController();
    }

    void SetupController()
    {
        Controllers[currentController].Initialize();
        Controllers[currentController].ResetCamera(transform);
    }

    // Update is called once per frame
    void Update()
    {
        Controllers[currentController].UpdateBehavior(transform);
    }
}
