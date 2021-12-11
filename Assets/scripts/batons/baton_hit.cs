using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baton_hit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        SoundManager.instance.PlayBatonHit();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
