using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

/// <summary>
/// manages game state, calls events
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
        _startPlayCanvas = Instantiate(_startPlayCanvasPrefab);
        Button startPlayButton = _startPlayCanvas.GetComponentInChildren<Button>();
        startPlayButton.onClick.AddListener(StartGame);
	}

    void StartGame()
    {
        if (!GameStarted)
        {
            GameStarted = true;
            EventMessenger.Instance.GameStarted.Invoke();
            if (_startPlayCanvas)
            {
                Destroy(_startPlayCanvas);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (_hud)
        {
            _hud.SetValues(_level, _lives, _score);
        }
	}
}
