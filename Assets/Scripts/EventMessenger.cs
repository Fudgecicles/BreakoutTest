using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// container for events
/// </summary>
public class EventMessenger : MonoBehaviour {

    public static EventMessenger Instance { get; private set; }

    public UnityEvent OnCountdownStarted;
    public UnityEvent OnGameStarted;
    public UnityEvent OnBallDestroyed;
    public UnityEvent OnAllBallsDestroyed;
    public BrickDestroyedEvent OnBrickDestroyed = new BrickDestroyedEvent();
    public UnityEvent OnLevelFinished;
    public UnityEvent OnLevelStarted;
    public UnityEvent OnGameEnded;


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

public class BrickDestroyedEvent : UnityEvent<int>{ }
