using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class first_person_cam : MonoBehaviour {
    public Transform playertrans;
    public float mouseSensitivity;
    public float minangleY , maxangleY;
    float rotationX;
	// Use this for initialization
	void OnEnable () {
        mouseSensitivity = 10f;
        transform.position = playertrans.position;
        transform.rotation = playertrans.rotation;
        rotationX = 0;
        minangleY = -90f;
        maxangleY = 90f;

    }
	
	// Update is called once per frame
	void Update () {
        if (!pauseManager.isGamePaused)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            transform.position = playertrans.position;
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            rotationX = Mathf.Clamp(rotationX, minangleY, maxangleY);

            transform.eulerAngles = new Vector3(rotationX, transform.eulerAngles.y + mouseX, 0f);
        }
    }
}
