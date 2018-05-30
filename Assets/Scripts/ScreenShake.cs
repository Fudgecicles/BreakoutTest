using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// perlin noise based screenshake I've copied for all my projects 
/// </summary>
public class ScreenShake : MonoBehaviour
{

    private static ScreenShake screenShakeInstance;

    Camera cam;

    float intialOrthoSize;
    Vector3 initialPos;
    Quaternion initialRotation;
    Quaternion offsetRotation;
    Vector3 velocity = Vector3.zero;
    bool shaking;

    float intensity;
    private float seed;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    // Use this for initialization
    void Start()
    {
        initialPos = transform.position;
        initialRotation = transform.rotation;
        offsetRotation = transform.rotation;
        intialOrthoSize = cam.orthographicSize;
        seed = Time.time;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (shaking && Time.timeScale != 0)
        {
            if (intensity == 0)
            {
                transform.position = initialPos;
                enabled = false;
                return;
            }

            intensity -= ScreenShakeConstants.instance.decayPerSecond * Time.deltaTime;
            intensity = Mathf.Clamp01(intensity);
            cam.orthographicSize = intialOrthoSize + Mathf.Lerp(0, ScreenShakeConstants.instance.maxScreenPunch, intensity);
            float x = Mathf.PerlinNoise(seed, Time.time * ScreenShakeConstants.instance.shakeSpeed) * 2 - 1;
            float y = Mathf.PerlinNoise(seed + 1, Time.time * ScreenShakeConstants.instance.shakeSpeed) * 2 - 1;
            float intensityPow = Mathf.Pow(intensity, ScreenShakeConstants.instance.intensityPower);
            x *= intensityPow * ScreenShakeConstants.instance.maxOffset;
            y *= intensityPow * ScreenShakeConstants.instance.maxOffset;
            transform.position = new Vector3(initialPos.x + x, initialPos.y + y, initialPos.z);
            float rotZ = (Mathf.PerlinNoise(seed + 4, Time.time * ScreenShakeConstants.instance.shakeSpeed) * 2 - 1) * intensityPow * ScreenShakeConstants.instance.maxRotation;
            Vector3 eulerAngles = initialRotation.eulerAngles;
            eulerAngles.z += rotZ;
            transform.rotation = Quaternion.Euler(eulerAngles);
        }
    }

    public void screenShakeHelper(float intensity)
    {
        shaking = true;
        this.intensity += intensity;
        if (!enabled)
        {
            enabled = true;
            initialPos = transform.position;
            initialRotation = transform.rotation;

            intialOrthoSize = cam.orthographicSize;
        }
    }

    public void stop()
    {
        shaking = false;
        this.intensity = 0;
        enabled = false;
    }

    public static void Stop()
    {
        if (Camera.main == null) return;

        if (screenShakeInstance == null)
        {
            GameObject mainCamera = Camera.main.gameObject;
            screenShakeInstance = mainCamera.AddComponent<ScreenShake>();
        }
        if (screenShakeInstance != null)
        {
            screenShakeInstance.stop();
        }
    }

    public static void Shake(float intensity)
    {
        if (Camera.main == null) return;
        if (intensity == 0) return;

        if (screenShakeInstance == null)
        {

            GameObject mainCamera = Camera.main.gameObject;
            screenShakeInstance = mainCamera.AddComponent<ScreenShake>();
        }
        if (screenShakeInstance != null)
        {
            screenShakeInstance.screenShakeHelper(intensity);
        }
    }
}