using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooterBullet : Shooter {

    public enum bulletType
    {
        Small = 5,
        Medium = 10,
        Big = 15
    }

    [SerializeField] protected bulletType bulletT;

    [SerializeField] protected int bulletCounter = 0;
    protected bool bulletFound = false;

    [SerializeField] protected GameObject[] bulletList;
    [SerializeField] protected int bulletLength,maxupgrade;
    [SerializeField] protected GameObject bullet;
    

    override protected void Awake()
    {

        bulletList = new GameObject[bulletLength];
        totalDamage = (int)bulletT;
        for (int i = 0; i < bulletLength; i++)
        {
            bulletList[i] = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            bulletList[i].GetComponent<HitBullet>().SetBulletType((int)bulletT);
            bulletList[i].SetActive(false);
        }
        currentLevel = 1;
        fireRate = 0.5f;
        maxupgrade = 0;
        totalAmmo = 24;
        CurrentAmmo = totalAmmo;
        fireButton = KeyBindManager.Instance.keyBindings["FIREBULLET"];
        reloadTime = new WaitForSeconds(5f);
        StartCoroutine(reload());
    }

   
    protected override void shoot()
    {
        SpawnBullet();
        
    }

    public override void upgrade()
    {
        int newBulletT;
        switch (bulletT)
        {
            case bulletType.Small:
                newBulletT = (int)bulletType.Medium;
                bulletT = bulletType.Medium;
                break;
            case bulletType.Medium:
                newBulletT = (int)bulletType.Big;
                bulletT = bulletType.Big;
                break;
            case bulletType.Big:
                newBulletT = (int)bulletType.Big + (++maxupgrade)*3;
                break;
            default:
                newBulletT = 100;  //impossible
                break;
        }
        base.upgrade();
        totalDamage = newBulletT;
        totalAmmo += 2;
        for (int i = 0; i < bulletLength; i++)
        {
            bulletList[i].GetComponent<HitBullet>().SetBulletType(newBulletT);
        }
    }
    

    public override IEnumerator shotEffect()
    {
        SoundManager.Instance.PlayShootingBullet();
        yield return null;
    }

    void SpawnBullet()
    {
        this.bulletFound = false;
        bool nobullets = false;
        while (!bulletFound && !nobullets)
        {
            int counter = 0;
            for (int i = bulletCounter; i < bulletLength && !bulletFound; i++,counter++)
            {
                //Debug.Log(counter);
                if (!bulletList[i].activeInHierarchy)
                {
                    bulletList[i].GetComponent<Bullet>().Spawn(spawnPoint);
                    bulletCounter = i + 1;
                    CurrentAmmo--;
                    bulletFound = true;
                }
                if (i == bulletLength - 1)
                    bulletCounter = 0;
            }
            if (bulletCounter == bulletLength)
                bulletCounter = 0;
            if (counter >= bulletLength)
            {
                nobullets = true;
            }

            

        }
    }

    public override void resumeUpgrades(int level)
    {
        int oldDamage;
        switch (level)
        {
            case 1:
                bulletT = bulletType.Small;
                oldDamage = (int)bulletType.Small;
                break;
            case 2:
                bulletT = bulletType.Medium;
                oldDamage = (int)bulletType.Medium;
                break;
            case 3:
                bulletT = bulletType.Big;
                oldDamage = (int)bulletType.Big;
                break;
            default:
                bulletT = bulletType.Big;
                oldDamage = (int)bulletType.Big + (level -3)*3;
                break;
        }
        base.resumeUpgrades(level);
        totalDamage = oldDamage;
        totalAmmo = totalAmmo + (level - 1)*2;
        CurrentAmmo = totalAmmo;
        for (int i = 0; i < bulletLength; i++)
        {
            bulletList[i].GetComponent<HitBullet>().SetBulletType((int)totalDamage);
        }
    }
}
