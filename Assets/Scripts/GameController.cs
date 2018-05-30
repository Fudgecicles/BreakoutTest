using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

/// <summary>
/// manages game state
/// </summary>
public class GameController : MonoBehaviour {

    public static bool GameStarted { get; private set; }


    [SerializeField]
    private GameObject _startPlayCanvasPrefab;
    [SerializeField]
    private GameObject _gameHudCanvasPrefab;
    [SerializeField]
    private int _startingLives = 3;

    private int _lives;
    private int _score = 0;
    private int _level = 1;

    private GameObject _startPlayCanvas;
    private GameHud _hud;

	void Awake () {
        _lives = _startingLives;

        // spawn start play canvas and setup callbacks
        _startPlayCanvas = Instantiate(_startPlayCanvasPrefab);
        Button startPlayButton = _startPlayCanvas.GetComponentInChildren<Button>();
        startPlayButton.onClick.AddListener(StartGame);

        // listen to events
        EventMessenger.Instance.OnAllBallsDestroyed.AddListener(LoseLife);
        EventMessenger.Instance.OnBrickDestroyed.AddListener(BlockDestroyed);
        EventMessenger.Instance.OnLevelFinished.AddListener(LevelFinished);
    }

    void LevelFinished()
    {
        _level += 1;
    }

    void BlockDestroyed(int points)
    {
        _score += points;
    }

    void LoseLife()
    {
        _lives -= 1;
        if(_lives == 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameStarted = false;
    }

    /// <summary>
    /// called when start play button is pressed
    /// </summary>
    void StartGame()
    {
        if (!GameStarted)
        {
            // start game, trigger events, spawn in game hud
            GameStarted = true;
            EventMessenger.Instance.OnGameStarted.Invoke();
            _hud = Instantiate(_gameHudCanvasPrefab).GetComponent<GameHud>();
            // if the canvas exists destroy it to clear the screen
            if (_startPlayCanvas)
            {
                Destroy(_startPlayCanvas);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        // update hud values if hud exists
        if (_hud)
        {
            _hud.SetValues(_level, _lives, _score);
        }
	}
}
