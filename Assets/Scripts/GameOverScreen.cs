using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {

    [SerializeField]
    private Text _scoreText;

    private void Awake()
    {
        GetComponentInChildren<Button>().onClick.AddListener(TryAgain);
    }

    public void SetScore(int score)
    {
        _scoreText.text = score.ToString();
    }
	
    void TryAgain()
    {
        SceneManager.LoadScene(0);        
    }

}
