using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaange_camera : MonoBehaviour {
    public Transform player;
    [SerializeField] GameObject[] POVs;
    [SerializeField] KeyCode tkey;
    bool camchanged;
    int numCameras;
    short camerina;

    private void Awake()
    {
        camchanged = true;
        GameObject cameras = GameObject.Find("cameras");
        numCameras = cameras.transform.childCount;
        POVs = new GameObject[numCameras];
        for (int i = 0; i < numCameras; i++)
        {
            POVs[i] = cameras.transform.GetChild(i).gameObject;
            if (i > 0) POVs[i].SetActive(false);
        }
        camerina = 0;
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(tkey) && camchanged)
        {
            camchanged = false;
            if(++camerina == numCameras)
                camerina=0;
            for (int i = 0; i < numCameras; i++)
            {
                if (i != camerina)
                {
                    POVs[i].SetActive(false);
                }
                else
                {
                    POVs[i].SetActive(true);
                }
            }
            /*camerina = !camerina;
            normal.gameObject.SetActive(camerina);
            evil.gameObject.SetActive(!camerina);*/
        }
        if (Input.GetKeyUp(tkey)){
            camchanged = true;
        }
	}
}
