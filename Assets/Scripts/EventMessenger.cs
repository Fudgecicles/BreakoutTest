using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// container for events
/// </summary>
public class EventMessenger : MonoBehaviour {

    public static EventMessenger Instance { get; private set; }

    public UnityEvent OnGameStarted;
    public UnityEvent OnBallDestroyed;
    public UnityEvent OnAllBallsDestroyed;
    public UnityEvent OnBrickDestroyed;
    public UnityEvent OnLevelFinished;
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
