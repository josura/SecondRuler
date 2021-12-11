using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooterLaser : Shooter {

                                            // Distance in Unity units over which the player can fire                                       // Holds a reference to the gun end object, marking the muzzle location of the gun
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);    // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible
    private LineRenderer laserLine;      
    [SerializeField] float laserDamage=1f;
    

    override protected void Start()
    {
        playerCamera = GameObject.Find("player1").GetComponent<stick_movement>();
        cameraNum = playerCamera.iscameraNorm;
        startRotation = transform.localRotation;
        shooterAudio = GetComponent<AudioSource>();
        laserLine = GetComponent<LineRenderer>();
        currentLevel = 1;
        CurrentAmmo = 1;
        totalAmmo = 1;
        totalDamage = laserDamage ;
        fireButton = KeyBindManager.Instance.keyBindings["FIRELASER"];


    }


    override public IEnumerator shotEffect()
    {
        shooterAudio.Play();
        // Turn on our line renderer
        laserLine.enabled = true;

        //Wait for .07 seconds
        yield return shotDuration;

        // Deactivate our line renderer after waiting
        laserLine.enabled = false;
    }

    


    protected override void shoot()
    {
        if (cameraNum == 2)
        {
            Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            // Declare a raycast hit to store information about what our raycast has hit
            RaycastHit hit;

            // Set the start position for our visual effect for our laser to the position of gunEnd
            laserLine.SetPosition(0, spawnPoint.position);
            //Debug.DrawRay(transform.position, Vector3.forward);

            // Check if our raycast has hit anything
            if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out hit, weaponRange))
            {
                // Set the end position for our laser line 
                laserLine.SetPosition(1, hit.point);

                if (hit.collider.gameObject.layer == 9)
                {
                    Enemy health = hit.collider.GetComponent<Enemy>();
                    if (health != null)
                    {
                        health.Damage(laserDamage);
                    }

                }



                // Check if the object we hit has a rigidbody attached
                if (hit.rigidbody != null)
                {
                    // Add force to the rigidbody we hit, in the direction from which it was hit
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (Camera.main.transform.forward * weaponRange));
            }
            SoundManager.instance.PlayShootingLaser();
        }
    }
    
    public override void upgrade()
    {
        laserDamage += 2.5f;
        totalDamage = laserDamage;
        if (fireRate>0.05f)
        {
            fireRate -= 0.01f;
        }
        base.upgrade();
    }

    public override void resumeUpgrades(int level)
    {
        laserDamage = 1 + (level - 1) * 2.5f;
        totalDamage = laserDamage;
        base.resumeUpgrades(level);
    }

    
}
