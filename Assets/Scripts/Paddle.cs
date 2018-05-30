using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    [SerializeField]
    private float _maxBounceAngle;

    [SerializeField]
    private bool _descritizedBouncing;
    [SerializeField]
    private int _descritizedBouncingSections;

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

        // round the lerp if we are using discrete bouncing
        if (_descritizedBouncing)
        {
            // offset by .5 because otherwise the start is shorter and the end is longer
            lerpAmount = Mathf.Round(lerpAmount * _descritizedBouncingSections - .5f);
            // offset by 1 because otherwise we would only bounce fully if we're completely right of the paddle
            lerpAmount =  (float)lerpAmount / (_descritizedBouncingSections-1);
            Debug.Log(lerpAmount);
        }
        // get the degree to shoot the paddle at, offset by 90 so that the center is straight up
        float bounceDegree = Mathf.Lerp(_maxBounceAngle, -_maxBounceAngle, lerpAmount) + 90;

        // create vector and multiply by current speed
        return currentVelocity.magnitude * new Vector2(Mathf.Cos(bounceDegree * Mathf.Deg2Rad), Mathf.Sin(bounceDegree * Mathf.Deg2Rad));
    }

}
