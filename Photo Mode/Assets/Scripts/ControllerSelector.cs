using System;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSelector : MonoBehaviour
{
    //[SerializeField] List<BaseController> Controllers = new List<BaseController>();
    public List<Action<Transform, Transform>> resetFunctions = new List<Action<Transform, Transform>> ();
    public List<Action<Transform, Transform>> updateFunctions = new List<Action<Transform, Transform>>();

    public Transform target;

    int currentController = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        resetFunctions.Add(OrbitController.ResetCamera);
        resetFunctions.Add(FreeroamController.ResetCamera);
        updateFunctions.Add(OrbitController.UpdateBehavior);
        updateFunctions.Add(FreeroamController.UpdateBehavior);

        SetupController();
    }

    [ContextMenu("SwapControls")]
    void SwitchToNextController()
    {
        currentController++;
        currentController %= updateFunctions.Count;
        SetupController();
    }

    void SetupController()
    {
        resetFunctions[currentController]?.Invoke(transform, target);
    }

    // Update is called once per frame
    void Update()
    {
        updateFunctions[currentController]?.Invoke(transform, target);
    }
}
