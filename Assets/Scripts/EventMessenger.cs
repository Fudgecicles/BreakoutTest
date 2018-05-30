using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// container for events
/// </summary>
public class EventMessenger : MonoBehaviour {

    public static EventMessenger Instance { get; private set; }

    public UnityEvent GameStarted;
    public UnityEvent GameEnded;


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
