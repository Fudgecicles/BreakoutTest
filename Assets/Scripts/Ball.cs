using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField]
    private float _intialSpeed;

    private Rigidbody2D _body;
    private Vector2 _velocity;

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    void Launch()
    {
        transform.parent = null; 
        _velocity = Vector2.up * _intialSpeed;
    }

    private void Update()
    {
        if (!GameController.GameStarted) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }
    }

    private void FixedUpdate()
    {
        _body.MovePosition(_body.position + _velocity * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Paddle paddle = collision.gameObject.GetComponent<Paddle>();
        if (paddle)
        {
            _velocity = paddle.GetBouncedVelocity(collision.contacts[0].point, _velocity);
        }
        else
        {
            Vector2 normal = collision.contacts[0].normal;
            _velocity = _velocity - 2 * (Vector2.Dot(_velocity, normal)) * normal;
        }
    }
}
