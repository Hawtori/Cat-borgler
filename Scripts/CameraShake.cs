using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CinemachineVirtualCamera vcam;


    private void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    public static void StartShake() {
        Debug.Log("START SHAKING NOW BROOOOOOOOOOOO");
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1;
        
    }
    public static void StopShake() {
        Debug.Log("STOP STOP STOP STOP STOP");
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
    
}
