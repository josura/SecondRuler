using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baton_twirl : MonoBehaviour {
    [SerializeField] float speed;
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(speed * Time.deltaTime, 0, 0, Space.Self);
	}
}
