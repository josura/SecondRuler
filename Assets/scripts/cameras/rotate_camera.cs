using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_camera : MonoBehaviour {

    public Transform playerTrans;

    public bool lookatplayer = false;
    public bool rotateAroundPlayer = true;

    public float rotationSpeed;
    public float zoomSpeed;
    public float maxzoom;
    public float minzoom;
    private float zoommino;

    Vector3 _camera_off;

    [Range(0.01f, 1.0f)] public float smoothfactor = 5.0f;

    private void OnEnable()
    {
        _camera_off= new Vector3(10, 5, 10);// playerTrans.position - new Vector3(5, -5, 5);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        _camera_off = new Vector3(10, 5, 10);// transform.position - playerTrans.position;
        rotationSpeed = 5.0f;
        zoomSpeed = 1f;
        maxzoom = 2f;
        minzoom = 0.5f;
        zoommino = 1f;
    }

    private void Update()
    {
        if (!pauseManager.isGamePaused)
        {
            

            zoommino -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            zoommino = Mathf.Clamp(zoommino, minzoom, maxzoom);

        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!pauseManager.isGamePaused)
        {
            if (rotateAroundPlayer)
            {
                Quaternion camturn = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);

                _camera_off = camturn * _camera_off;
            }

            Vector3 newPos = playerTrans.position + _camera_off * zoommino;

            transform.position = Vector3.Slerp(transform.position, newPos, smoothfactor);
            if (rotateAroundPlayer || lookatplayer)
                transform.LookAt(playerTrans);
        }
    }
}
