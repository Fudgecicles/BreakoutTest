using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour {

    [SerializeField]
    private float _paddleMoveSpeed;
    [SerializeField]
    private float _paddleSmoothDamp;
    [SerializeField]
    private float _minPos;
    [SerializeField]
    private float _maxPos;

    private float _paddleVel;
    private float _paddleDampVel;
    private float _targetPaddleSpeed;

    private bool _leftPressed;
    private bool _rightPressed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameController.GameStarted) return;

        // input
        // need to store pressed bools because its possible for up to be processed without down if its held before the game starts
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _rightPressed = true;
            _targetPaddleSpeed += _paddleMoveSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) && _rightPressed)
        {
            _rightPressed = false;
            _targetPaddleSpeed -= _paddleMoveSpeed;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _leftPressed = true;
            _targetPaddleSpeed -= _paddleMoveSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) && _leftPressed)
        {
            _leftPressed = false;
            _targetPaddleSpeed += _paddleMoveSpeed;
        }

        // speed lerping
        _paddleVel = Mathf.SmoothDamp(_paddleVel, _targetPaddleSpeed, ref _paddleDampVel, _paddleSmoothDamp, 100, Time.deltaTime);

        // translate in x direction based on velocity and clamp
        Vector3 pos = transform.position;
        pos.x += _paddleVel * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, _minPos, _maxPos);
        transform.position = pos;   
    }
}
