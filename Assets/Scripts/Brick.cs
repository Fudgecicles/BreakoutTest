using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {


    [SerializeField]
    private float[] _healthAlpha;

    private SpriteRenderer _renderer;
    private int _hitsTaken;
    private int _points;

	// Use this for initialization
	void Awake () {
        _renderer = GetComponent<SpriteRenderer>();
	}

    public void SetTier(BlockTier tier)
    {
        _renderer.color = tier.Color;
        _points = tier.Points;
    }
	
    void TakeDamage()
    {
        _hitsTaken += 1;
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
        if (collision.gameObject.GetComponent<Ball>())
        {
            TakeDamage();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
