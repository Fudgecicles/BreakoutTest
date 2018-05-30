using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallKillTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ball>())
        {
            Destroy(collision.gameObject);
            EventMessenger.Instance.OnBallDestroyed.Invoke();
        }

    }

}
