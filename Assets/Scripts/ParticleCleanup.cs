using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCleanup : MonoBehaviour {

    ParticleSystem _system;

	// Use this for initialization
	void Start () {
        _system = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!_system.IsAlive())
        {
            Destroy(gameObject);
        }
	}
}
