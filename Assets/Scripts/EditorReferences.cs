using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Editor References", menuName = "Breakout/Create Editor References", order = 0)]
public class EditorReferences : ScriptableObject {

    private static EditorReferences _instance;

    public static EditorReferences instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = Resources.Load<EditorReferences>("ScriptableObjects/Editor References");
            }
            return _instance;
        }
    }

    public ScreenShakeConstants screenShakeConsts;

}
