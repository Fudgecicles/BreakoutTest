using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRespawner : MonoBehaviour {

    [SerializeField]
    private GameObject _ballPrefab;

    private int _ballsSpawned = 0;

    // Use this for initialization
    void Start() {
        EventMessenger.Instance.OnBallDestroyed.AddListener(BallDestroyed);
        EventMessenger.Instance.OnGameStarted.AddListener(SpawnBallInitial);
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

    // function to spawn and launch the ball immediately 
    void SpawnBallInitial()
    {
        SpawnBall().GetComponent<Ball>().Launch();
    }

    GameObject SpawnBall()
    {
        if (GameController.Instance.Lives <= 0) return null;
        _ballsSpawned += 1;
        return Instantiate(_ballPrefab, transform.position, Quaternion.identity, transform);
    }

    public void ResetBalls()
    {
        Ball[] balls = FindObjectsOfType<Ball>();
        for(int k=0; k < balls.Length; k++)
        {
            Destroy(balls[k].gameObject);
        }
        _ballsSpawned = 0;
        SpawnBall();
    }
}
