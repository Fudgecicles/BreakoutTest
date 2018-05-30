using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

/// <summary>
/// manages game state
/// </summary>
public class GameController : MonoBehaviour {

    public static GameController Instance { get; private set; }

    public static bool GameStarted { get; private set; }

    [SerializeField]
    private GameObject _startPlayCanvasPrefab;
    [SerializeField]
    private GameObject _countdownCanvasPrefab;
    [SerializeField]
    private GameObject _gameHudCanvasPrefab;
    [SerializeField]
    private GameObject _levelIntroCanvasPrefab;
    [SerializeField]
    private GameObject _gameOverCanvasPrefab;
    [SerializeField]
    private int _startingLives = 3;
    [SerializeField]
    private bool _debugEnabled;

    public bool DebugEnabled { get { return _debugEnabled; } }

    public int Lives { get; private set; }
    private int _score = 0;
    private int _level = 1;

    private GameObject _startPlayCanvas;
    private GameHud _hud;

	void Awake () {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Lives = _startingLives;

        // spawn start play canvas and setup callbacks
        _startPlayCanvas = Instantiate(_startPlayCanvasPrefab);
        Button startPlayButton = _startPlayCanvas.GetComponentInChildren<Button>();
        startPlayButton.onClick.AddListener(StartCountdown);

        // listen to events
        EventMessenger.Instance.OnGameStarted.AddListener(StartGame);
        EventMessenger.Instance.OnAllBallsDestroyed.AddListener(LoseLife);
        EventMessenger.Instance.OnBrickDestroyed.AddListener(BlockDestroyed);
        EventMessenger.Instance.OnLevelFinished.AddListener(LevelFinished);
        EventMessenger.Instance.OnLevelStarted.AddListener(LevelStarted);
    }

    void StartCountdown()
    {
        Instantiate(_countdownCanvasPrefab);
        EventMessenger.Instance.OnCountdownStarted.Invoke();
        // if the canvas exists destroy it to clear the screen
        if (_startPlayCanvas)
        {
            Destroy(_startPlayCanvas);
        }
    }

    void LevelFinished()
    {
        _level += 1;
        GameObject introCanvas = Instantiate(_levelIntroCanvasPrefab);
        introCanvas.GetComponent<LevelIntro>().SetLevel(_level);
        GameStarted = false;
    }

    void BlockDestroyed(int points)
    {
        _score += points;
    }

    void LoseLife()
    {
        Lives -= 1;
        if(Lives == 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameStarted = false;
        GameOverScreen overScreen = Instantiate(_gameOverCanvasPrefab).GetComponent<GameOverScreen>();
        overScreen.SetScore(_score);
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
            _hud = Instantiate(_gameHudCanvasPrefab).GetComponent<GameHud>();
        }
    }

    void LevelStarted()
    {
        GameStarted = true;
    }
	
	// Update is called once per frame
	void Update () {
        // update hud values if hud exists
        if (_hud)
        {
            _hud.SetValues(_level, Lives, _score);
        }
	}
}
