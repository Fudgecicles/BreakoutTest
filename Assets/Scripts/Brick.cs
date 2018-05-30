using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {


    [SerializeField]
    private float[] _healthAlpha;

    
    private SpriteRenderer _renderer;
    private int _hitsTaken;
    private int _points;
    private float _ballSpeed;
    private Animator _anim;

	// Use this for initialization
	void Awake () {
        _renderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    public void SetTier(BlockTier tier)
    {
        _renderer.color = tier.Color;
        _points = tier.Points;
        _ballSpeed = tier.BallSpeed;
    }
	
    void TakeDamage()
    {
        _hitsTaken += 1;
        _anim.SetTrigger("Hit");
        if(_hitsTaken >= _healthAlpha.Length)
        {
            EventMessenger.Instance.OnBrickDestroyed.Invoke(_points);
            Destroy(gameObject);
        }
        else
        {
            Color curCol = _renderer.color;
            curCol.a = _healthAlpha[_hitsTaken];
            _renderer.color = curCol;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball)
        {
            TakeDamage();
            ball.SetSpeed(_ballSpeed);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
