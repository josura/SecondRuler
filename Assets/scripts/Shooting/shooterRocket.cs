using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooterRocket : Shooter {

    public enum rocketType
    {
        normal = 20,
        explosive = 30,
        atomic = 50
    }

    [SerializeField] protected rocketType rocketT;

    [SerializeField] protected int rocketCounter = 0;
    protected bool rocketFound = false;

    [SerializeField] protected GameObject[] rocketList;
    [SerializeField] protected int rocketLength, maxupgrade;
    [SerializeField] protected GameObject[] rocketObj;

    override protected void Awake()
    {

        rocketList = new GameObject[rocketLength];
        totalDamage = (int)rocketT;
        for (int i = 0; i < rocketLength; i++)
        {
            rocketList[i] = Instantiate(rocketObj[0], spawnPoint.position, spawnPoint.rotation);
            rocketList[i].GetComponent<HitRocket>().SetRocketType((int)rocketT);
            rocketList[i].SetActive(false);
        }
        currentLevel = 1;
        fireRate = 1f;
        totalAmmo = 7;
        CurrentAmmo = totalAmmo;
        fireButton = KeyCode.Mouse1;
        reloadTime = new WaitForSeconds(10f);
        StartCoroutine(reload());
    }


    protected override void shoot()
    {
        Spawnrocket();

    }

    public override void upgrade()
    {
        int newrocketT;
        int rockOb;
        switch (rocketT)
        {
            case rocketType.normal:
                newrocketT = (int)rocketType.explosive;
                rocketT = rocketType.explosive;
                rockOb = 1;
                break;
            case rocketType.explosive:
                newrocketT = (int)rocketType.atomic;
                rocketT = rocketType.atomic;
                rockOb = 2;
                break;
            case rocketType.atomic:
                newrocketT = (int)rocketType.atomic + (++maxupgrade) * 10;
                rockOb = 2;
                break;
            default:
                newrocketT = 500;  //impossible
                rockOb = 3;
                break;
        }
        base.upgrade();
        totalDamage = newrocketT;
        totalAmmo++;
        for (int i = 0; i < rocketLength; i++)
        {
            rocketList[i] = Instantiate(rocketObj[rockOb], spawnPoint.position, spawnPoint.rotation);
            rocketList[i].GetComponent<HitRocket>().SetRocketType(newrocketT);
            rocketList[i].SetActive(false);
        }
    }


    public override IEnumerator shotEffect()
    {
        //SoundManager.Instance.PlayShootingrocket();
        yield return null;
    }

    void Spawnrocket()
    {
        this.rocketFound = false;
        bool norockets = false;
        while (!rocketFound && !norockets)
        {
            int counter = 0;
            for (int i = rocketCounter; i < rocketLength && !rocketFound; i++, counter++)
            {
                //Debug.Log(counter);
                if (!rocketList[i].activeInHierarchy)
                {
                    rocketList[i].GetComponent<Rocket>().Spawn(spawnPoint);
                    rocketCounter = i + 1;
                    CurrentAmmo--;
                    rocketFound = true;
                }
                if (i == rocketLength - 1)
                    rocketCounter = 0;
            }
            if (rocketCounter == rocketLength)
                rocketCounter = 0;
            if (counter >= rocketLength)
            {
                norockets = true;
            }



        }
    }

    public override void resumeUpgrades(int level)
    {
        int oldDamage,rockOb;
        switch (level)
        {
            case 0:
                rocketT = rocketType.normal;
                oldDamage = (int)rocketType.normal;
                level = 1;
                rockOb = 0;
                break;
            case 1:
                rocketT = rocketType.normal;
                oldDamage = (int)rocketType.normal;
                rockOb = 0;
                break;
            case 2:
                rocketT = rocketType.explosive;
                oldDamage = (int)rocketType.explosive;
                rockOb = 1;
                break;
            case 3:
                rocketT = rocketType.atomic;
                oldDamage = (int)rocketType.atomic;
                rockOb = 2;
                break;
            default:
                rocketT = rocketType.atomic;
                oldDamage = (int)rocketType.atomic + (level - 3) * 5;
                rockOb = 2;
                break;
        }
        base.resumeUpgrades(level);
        totalDamage = oldDamage;
        totalAmmo = totalAmmo + level - 1;
        CurrentAmmo = totalAmmo;
        for (int i = 0; i < rocketLength; i++)
        {
            rocketList[i] = Instantiate(rocketObj[rockOb], spawnPoint.position, spawnPoint.rotation);
            rocketList[i].GetComponent<HitRocket>().SetRocketType((int)totalDamage);
            rocketList[i].SetActive(false);


        }
    }
}
