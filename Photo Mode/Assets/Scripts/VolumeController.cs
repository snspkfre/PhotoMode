using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class VolumeController : MonoBehaviour
{
    [SerializeField] List<string> NameList = new List<string>();
    [SerializeField] List<VolumeProfile> ProfileList = new List<VolumeProfile>();
    int currentFilter = 0;

    private Volume volume;

    void Awake()
    {
        volume = GetComponent<Volume>();
        SetupController();
    }
    void SetupController()
    {
        SetFilter();
    }

    void SetFilter()
    {
        volume.profile = ProfileList[currentFilter];
    }

    [ContextMenu("SwapFilter")]
    void SwitchToNextFilter()
    {
        currentFilter++;
        currentFilter %= ProfileList.Count;

        if (NameList.Count < currentFilter - 1)
        {
            Debug.LogError("Error: No Filter Name Set");
            return;
        }

        Debug.Log(NameList[currentFilter]);
        SetFilter();
    }
}
