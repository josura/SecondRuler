using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    protected stick_movement playerCamera;
    protected Vector3 startposition;
    [SerializeField] protected int currentLevel;
    protected Quaternion startRotation;
    [SerializeField] protected int cameraNum;
    protected Camera mainCamera;
    protected Vector3 centerScreen = new Vector3(0.5f, 0.5f, 0f);
    public float fireRate = 0.25f;
    public float hitForce = 100f;
    protected AudioSource shooterAudio;
    protected float nextFire;
    protected int totalAmmo, CurrentAmmo;
    [SerializeField] protected float timeToSpawn;
    [SerializeField] protected Transform spawnPoint;
    public float weaponRange = 50f;
    protected WaitForSeconds reloadTime;

    [SerializeField] protected KeyCode fireButton;


    [SerializeField] protected float totalDamage;

    public int getTotalAmmo()
    {
        return totalAmmo;
    }

    public int getAmmoNumber()
    {
        return CurrentAmmo;
    }

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        playerCamera = GameObject.Find("player1").GetComponent<stick_movement>();
        cameraNum = playerCamera.iscameraNorm;
        startRotation = transform.localRotation;
        shooterAudio = GetComponent<AudioSource>();
        
    }

    protected IEnumerator reload(){
        while(true){
            if (CurrentAmmo<totalAmmo)
            {
                CurrentAmmo++;
            }
            yield return reloadTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseManager.isGamePaused)
        {
            cameraNum = playerCamera.iscameraNorm;
            aim();
            if (Input.GetKeyDown(fireButton) && Time.time > nextFire && CurrentAmmo>0)
            {
                nextFire = Time.time + fireRate;
                StartCoroutine(shotEffect());
                shoot();
            }
        }
    }

    public virtual IEnumerator shotEffect()
    {
        shooterAudio.Play();
        yield return null;
    }

    public virtual void upgrade()
    {
        currentLevel++;
    }


    protected virtual void shoot()
    {
    }


    protected virtual void aim()
    {
        if (cameraNum == 2)
        {
            /*
            Vector3 screenpoint = Input.mousePosition;
            screenpoint.z = Camera.main.farClipPlane;
            Vector3 worldpoint = Camera.main.ScreenToWorldPoint(screenpoint);
            Vector3 origin = transform.position;
            Vector3 direction = (worldpoint - origin).normalized;
            Ray targetdirection = new Ray(origin, direction);
            Quaternion targetRotation = Quaternion.LookRotation(targetdirection.direction);
            */
            /*
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Quaternion targetRotation = Quaternion.LookRotation(cameraRay.direction);
            */
            /*Ray cameraRay = Camera.main.ViewportPointToRay(centerScreen);
            Quaternion targetRotation = Quaternion.LookRotation(cameraRay.direction);
            */
            Vector3 rayorigin = Camera.main.ViewportToWorldPoint(centerScreen);
            RaycastHit hit;

            if (Physics.Raycast(rayorigin, Camera.main.transform.forward, out hit))
            {
                if (hit.collider.gameObject.layer!=8)
                { 
                    transform.LookAt(hit.point);
                }
            }
            else
            {
                transform.LookAt(transform.position + Camera.main.transform.forward);
            }

            //transform.rotation = targetRotation;
        }
        else
        {
                transform.localRotation = startRotation;
        }
    }

    public float getDamage()
    {
        return totalDamage;
    }

    public virtual void resumeUpgrades(int level)
    {
        currentLevel = level;
    }

    public int getLevel()
    {
        return currentLevel;
    }

}
