using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelIntro : MonoBehaviour {

    [SerializeField]
    private Text _levelText;

    private void Start()
    {
        GetComponent<Animator>().GetBehaviour<AnimationEndMessenger>().OnStateFinished.AddListener(DestroySelf);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void SetLevel(int level)
    {
        _levelText.text = level.ToString();
    }
	

}
