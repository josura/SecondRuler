using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapCamera : MonoBehaviour {

    [SerializeField] private GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (!pauseManager.isGamePaused)
        {
            Vector3 newPos = player.transform.position;
            newPos.y = transform.position.y;
            transform.position = newPos;
        }
    }
}
