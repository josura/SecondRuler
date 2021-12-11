using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isometric_camera : MonoBehaviour {
    [SerializeField] private GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!pauseManager.isGamePaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Vector3 newPos = player.transform.position;
            newPos.y =newPos.y+20;
            transform.position = newPos;
        }
	}
}
