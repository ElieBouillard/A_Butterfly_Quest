using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class SimpleCameraShakeInCinemachine : MonoBehaviour {


    public static SimpleCameraShakeInCinemachine m_instance;
    public float ShakeDuration = 0.3f;          // Time the Camera Shake effect will last
    public float ShakeAmplitude = 1.2f;         // Cinemachine Noise Profile Parameter
    public float ShakeFrequency = 2.0f;         // Cinemachine Noise Profile Parameter

    private float ShakeElapsedTime = 0f;

    // Cinemachine Shake
    public CinemachineVirtualCamera VirtualCamera;
    public CinemachineFreeLook FreeLookCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;
    private CinemachineBasicMultiChannelPerlin freeLookRig1Noise;
    private CinemachineBasicMultiChannelPerlin freeLookRig2Noise;
    private CinemachineBasicMultiChannelPerlin freeLookRig3Noise;

    private void Awake()
    {
        m_instance = this;
    }

    // Use this for initialization
    void Start()
    {
        // Get Virtual Camera Noise Profile
        if (VirtualCamera != null)
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

        if (FreeLookCamera != null)
            freeLookRig1Noise = FreeLookCamera.GetRig(0).GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
            freeLookRig2Noise = FreeLookCamera.GetRig(1).GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
            freeLookRig3Noise = FreeLookCamera.GetRig(2).GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Replace with your trigger
        if (Input.GetKeyDown("p"))
        {
            ShakeElapsedTime = ShakeDuration;
        }

        // If the Cinemachine componet is not set, avoid update
        if (VirtualCamera != null && virtualCameraNoise != null && FreeLookCamera != null && virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (ShakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;
                freeLookRig1Noise.m_AmplitudeGain = ShakeAmplitude;
                freeLookRig1Noise.m_FrequencyGain = ShakeFrequency;
                freeLookRig2Noise.m_AmplitudeGain = ShakeAmplitude;
                freeLookRig2Noise.m_FrequencyGain = ShakeFrequency;
                freeLookRig3Noise.m_AmplitudeGain = ShakeAmplitude;
                freeLookRig3Noise.m_FrequencyGain = ShakeFrequency;

                // Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                freeLookRig1Noise.m_AmplitudeGain = 0f;
                freeLookRig2Noise.m_AmplitudeGain = 0f;
                freeLookRig3Noise.m_AmplitudeGain = 0f;

                ShakeElapsedTime = 0f;
            }
        }
    }

    public void StartShake(float shakeDuration = 0.2f, float shakeAmplitude = 1f, float shakeFrequency = 1f)
    {
        ShakeDuration = shakeDuration;
        ShakeAmplitude = shakeAmplitude;
        ShakeFrequency = shakeFrequency;
        ShakeElapsedTime = ShakeDuration;
    }
}
