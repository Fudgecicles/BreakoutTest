using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    [SerializeField]
    private float _maxBounceAngle;

    private Bounds _bounds;

    private void Awake()
    {
        _bounds = GetComponent<SpriteRenderer>().bounds;
    }

    public Vector2 GetBouncedVelocity(Vector2 contactPosition, Vector2 currentVelocity)
    {
        // convert contact position into local space;
        contactPosition =  contactPosition - (Vector2)transform.position;
        // get contact position closeness to left or right of paddle
        float lerpAmount = Mathf.InverseLerp(_bounds.min.x, _bounds.max.x, contactPosition.x);
        // get the degree to shoot the paddle at, offset by 90 so that the center is straight up
        float bounceDegree = Mathf.Lerp(_maxBounceAngle, -_maxBounceAngle, lerpAmount) + 90;
        // create vector and multiply by current speed
        return currentVelocity.magnitude * new Vector2(Mathf.Cos(bounceDegree * Mathf.Deg2Rad), Mathf.Sin(bounceDegree * Mathf.Deg2Rad));
    }

}
