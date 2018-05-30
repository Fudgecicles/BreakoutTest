using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    [SerializeField]
    private Color[] _healthColors;

    SpriteRenderer _renderer;
    int _hitsTaken;

	// Use this for initialization
	void Start () {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = _healthColors[0];
	}
	
    void TakeDamage()
    {
        _hitsTaken += 1;
        if(_hitsTaken >= _healthColors.Length)
        {
            EventMessenger.Instance.OnBrickDestroyed.Invoke();
            Destroy(gameObject);
        }
        else
        {
            _renderer.color = _healthColors[_hitsTaken];
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
