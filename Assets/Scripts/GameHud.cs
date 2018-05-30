using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class GameHud : MonoBehaviour {

    [SerializeField]
    private Text _livesValue;
    [SerializeField]
    private Text _levelValue;
    [SerializeField]
    private Text _scoreValue;

	public void SetValues(int level, int lives, int score)
    {
        _livesValue.text = lives.ToString();
        _levelValue.text = level.ToString();
        _scoreValue.text = score.ToString();
    }

}
