using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Brick : MonoBehaviour , IPointerDownHandler, IPointerUpHandler, IPointerClickHandler{

    [SerializeField]
    private float _hitShakeAmount;
    [SerializeField]
    private float _dieShakeAmount;
    [SerializeField]
    private float[] _healthAlpha;

    
    private SpriteRenderer _renderer;
    private int _hitsTaken;
    private int _points;
    private float _ballSpeed;
    private Animator _anim;
    private bool _destroyed;
    private float _currentAlpha = 1;

	// Use this for initialization
	void Awake () {
        _renderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _anim.GetBehaviour<AnimationEndMessenger>().OnStateFinished.AddListener(DestroySelf);
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
        // destroyed
        if(_hitsTaken >= _healthAlpha.Length)
        {
            ScreenShake.Shake(_dieShakeAmount);
            EventMessenger.Instance.OnBrickDestroyed.Invoke(_points);
            GetComponent<Collider2D>().enabled = false;
            _anim.SetTrigger("Destroy");
            _destroyed = true;
        }
        // damaged
        else
        {
            ScreenShake.Shake(_hitShakeAmount);
            _currentAlpha = _healthAlpha[_hitsTaken];
        }
    }

    void LateUpdate()
    {
        // override animation alpha in late update, but only if we are not playing the death animation which uses alpha
        if (_destroyed) return;
        Color curCol = _renderer.color;
        curCol.a = _currentAlpha;
        _renderer.color = curCol;
    }

    void DestroySelf()
    {
        Destroy(gameObject);
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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameController.Instance.DebugEnabled)
        {
            TakeDamage();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }
}
