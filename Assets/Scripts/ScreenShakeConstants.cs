using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ScreenShake Constants", menuName = "Breakout/Create Screenshake Constants", order = 0)]
public class ScreenShakeConstants : ScriptableObject{

    private static ScreenShakeConstants _instance;
    public static ScreenShakeConstants instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = EditorReferences.instance.screenShakeConsts;
            }
            return _instance;
        }
    }


    public float decayPerSecond;
    public float shakeSpeed;
    public float maxRotation;
    public float maxOffset;
    public float intensityPower;
    public float maxScreenPunch;

}
