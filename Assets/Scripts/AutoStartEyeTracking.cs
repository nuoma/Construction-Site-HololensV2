using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Examples.Demos.EyeTracking.Logging;

public class AutoStartEyeTracking : MonoBehaviour
{
    public GameObject RecorderNode;
    // Start is called before the first frame update
    void Start()
    {
        RecorderNode.GetComponent<UserInputRecorder>().StartLogging();
        RecorderNode.GetComponent<UserInputRecorderFeedback>().StartRecording();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
