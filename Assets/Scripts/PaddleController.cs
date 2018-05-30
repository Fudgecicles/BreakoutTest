using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour {

    [SerializeField]
    private float _paddleMoveSpeed;
    [SerializeField]
    private float _paddleSmoothDamp;

    private float _paddleVel;
    private float _paddleDampVel;
    private float _targetPaddleSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // input
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _targetPaddleSpeed += _paddleMoveSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            _targetPaddleSpeed -= _paddleMoveSpeed;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _targetPaddleSpeed -= _paddleMoveSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _targetPaddleSpeed += _paddleMoveSpeed;
        }

        // speed lerping
        _paddleVel = Mathf.SmoothDamp(_paddleVel, _targetPaddleSpeed, ref _paddleDampVel, _paddleSmoothDamp, 100, Time.deltaTime);

        // translate in x direction based on velocity
        transform.Translate(new Vector2(_paddleVel * Time.deltaTime, 0));
	}
}
