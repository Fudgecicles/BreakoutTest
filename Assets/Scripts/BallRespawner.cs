using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRespawner : MonoBehaviour {

    [SerializeField]
    private GameObject _ballPrefab;

    private int _ballsSpawned = 0;

    // Use this for initialization
    void Start() {
        SpawnBall();
        EventMessenger.Instance.OnBallDestroyed.AddListener(BallDestroyed);
        EventMessenger.Instance.OnGameStarted.AddListener(SpawnBall);
        EventMessenger.Instance.OnLevelFinished.AddListener(ResetBalls);
    }

    void BallDestroyed()
    {
        _ballsSpawned -= 1;
        if(_ballsSpawned == 0)
        {
            EventMessenger.Instance.OnAllBallsDestroyed.Invoke();
            SpawnBall();
        }
    }

    void SpawnBall()
    {
        if (!GameController.GameStarted) return;
        _ballsSpawned += 1;
        Instantiate(_ballPrefab, transform.position, Quaternion.identity, transform);
    }

    public void ResetBalls()
    {
        Ball[] balls = FindObjectsOfType<Ball>();
        for(int k=0; k < balls.Length; k++)
        {
            Destroy(balls[k].gameObject);
        }
        SpawnBall();
    }
}
