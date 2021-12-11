using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour {

	[SerializeField] float speed;

	void Update()
	{
		transform.Translate (Vector3.forward * speed * Time.deltaTime, Space.Self);
	}
}
