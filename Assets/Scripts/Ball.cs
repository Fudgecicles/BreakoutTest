using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField]
    private float _intialSpeed;
    [SerializeField]
    private float _bounceCooldown = .01f;

    private Rigidbody2D _body;
    private Vector2 _velocity;
    private float _lastBounceTime;

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

    public void SetSpeed(float speed)
    {
        float currentMagnitude = _velocity.magnitude;
        float newMagnitude = Mathf.Max(speed, currentMagnitude);
        _velocity = _velocity.normalized * newMagnitude;
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
            // prevent bouncing multiple times in a single frame with a very small cooldown
            if (Time.time - _lastBounceTime < _bounceCooldown) return;

            _lastBounceTime = Time.time;
            Vector2 normal = collision.contacts[0].normal;
            // prevent any weirdness with normal calculation, we should only be bouncing in cardinal directions to clamp to them 
            if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
            {
                normal.x = 1 * Mathf.Sign(normal.x);
                normal.y = 0;
            }
            else
            {
                normal.x = 0;
                normal.y = 1 * Mathf.Sign(normal.y);
            }
            _velocity = _velocity - 2 * (Vector2.Dot(_velocity, normal)) * normal;
        }
    }
}
