using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    
    [Header("Camera Shake")]
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualNoiseCamera;
    public float shakeDuration;
    public float elapsedTime;
    public float shakeAmplitude;
    public float shakeFrequency;

    private void Start()
    {
        if (virtualCamera != null)
        {
            virtualNoiseCamera = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }

    }

    private void Update()
    {
        Shake();
    }

    private void Shake()
    {
        if (elapsedTime > 0)
        {
            virtualNoiseCamera.m_AmplitudeGain = shakeAmplitude;
            virtualNoiseCamera.m_FrequencyGain = shakeFrequency;
            elapsedTime -= Time.deltaTime;
        }
        else
        {
            elapsedTime = 0;
            virtualNoiseCamera.m_FrequencyGain = 0;
            virtualNoiseCamera.m_AmplitudeGain = 0;
        }
    }

    public void C_Shake(float duration = .01f, float amplitude = 1f, float frequency = 1f)
    {
        elapsedTime = duration;
        shakeAmplitude = amplitude;
        shakeFrequency = frequency;
        Debug.Log("Called!!");
    }
}
