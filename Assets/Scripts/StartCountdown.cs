using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class StartCountdown : MonoBehaviour {

    [SerializeField]
    private int _countdownStart = 3;
    [SerializeField]
    private Text _text;

    private float _timer;
    private Animator _anim;

	// Use this for initialization
	void Start () {
        _timer = _countdownStart;
        _anim = GetComponent<Animator>();
        _anim.SetTrigger("Bounce");
        _anim.GetBehaviour<AnimationEndMessenger>().OnStateFinished.AddListener(DestroySelf);
        _text.text = _countdownStart.ToString();
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (_timer <= 0) return;

        int preDecrement = (int)_timer;
        _timer -= Time.deltaTime;
        int postDecrement = (int)_timer;
        // if int has changed then update and bounce
        if (preDecrement != postDecrement)
        {
            _text.text = preDecrement.ToString();
            _anim.SetTrigger("Bounce");
        }
        if(_timer <= 0) // if timer is done then fade and say go 
        {
            _text.text = "GO!";
            _anim.SetTrigger("Bounce");
            _anim.SetTrigger("Fade");
            EventMessenger.Instance.OnGameStarted.Invoke();
        }
	}
}
