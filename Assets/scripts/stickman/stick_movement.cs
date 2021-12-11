using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stick_movement : MonoBehaviour {
    [SerializeField] float speed;
    [SerializeField] float rotationspeed;
    [SerializeField] KeyCode tkey;
    float progress;
    public int iscameraNorm = 0;
    bool isgrounded;
    Vector3 jump;
    Rigidbody rb;
    void Start()
    {
        isgrounded = false;
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        //rotationspeed = 0.05f;
    }


    void OnCollisionStay()
    {
        isgrounded = true;
    }
    // Update is called once per frame
    void Update () {
        if (!pauseManager.isGamePaused)
        {
            if (iscameraNorm == 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                float distance;
                if (plane.Raycast(ray, out distance))
                {
                    Vector3 target = ray.GetPoint(distance);
                    Vector3 direction = target - transform.position;
                    float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, rotation, 0);
                }

            }
            if (iscameraNorm == 2)
            {
                Quaternion dest_rota = GameObject.Find("Cameraevil").transform.rotation;
                dest_rota = Quaternion.Euler(0, dest_rota.eulerAngles.y, 0);
                //transform.rotation = Quaternion.Slerp(transform.rotation, dest_rota, 1);
                transform.rotation = dest_rota;
            }
            Space visual = Space.Self;
            if (iscameraNorm == 0) visual = Space.World;
            if (Input.GetKey(KeyBindManager.Instance.keyBindings["UP"]))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed, visual);   //space.self di default
                if (iscameraNorm == 1)
                {
                    Quaternion dest_rota = GameObject.Find("Cameranorm").transform.rotation;
                    dest_rota = Quaternion.Euler(0, dest_rota.eulerAngles.y, 0);
                    progress += rotationspeed * Time.deltaTime;
                    progress = Mathf.Clamp01(progress);
                    transform.rotation = Quaternion.Slerp(transform.rotation, dest_rota, progress);
                }
            }

            if (Input.GetKey(KeyBindManager.Instance.keyBindings["DOWN"]))
            {
                transform.Translate(Vector3.back * Time.deltaTime * speed, visual);
                if (iscameraNorm == 1)
                {
                    Quaternion dest_rota = GameObject.Find("Cameranorm").transform.rotation;
                    dest_rota = Quaternion.Euler(0, dest_rota.eulerAngles.y, 0);
                    progress += rotationspeed * Time.deltaTime;
                    progress = Mathf.Clamp01(progress);
                    transform.rotation = Quaternion.Slerp(transform.rotation, dest_rota, progress);
                }
            }
            if (Input.GetKey(KeyBindManager.Instance.keyBindings["RIGHT"]))
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed, visual);
            }
            if (Input.GetKey(KeyBindManager.Instance.keyBindings["LEFT"]))
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed, visual);
            }
            if (Input.GetKeyUp(KeyBindManager.Instance.keyBindings["UP"]) || Input.GetKeyUp(KeyBindManager.Instance.keyBindings["DOWN"]))
            {
                progress = 0;
            }
            if (Input.GetKey(KeyCode.Space) && isgrounded)
            {
                rb.AddForce(jump, ForceMode.Impulse);
                isgrounded = false;
            }
        }
       
    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(tkey))
        {
            iscameraNorm = (++iscameraNorm) % 3;
        }
    }
}
