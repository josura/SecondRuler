using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootersManager : MonoBehaviour {
    [SerializeField] GameObject shooterBullet, shooterLaser, shooterRocket;


    private static ShootersManager _instance;
    public static ShootersManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);   //to keep the singleton between scenes
    }

    public void upgradeBullet()
    {
        shooterBullet.GetComponent<shooterBullet>().upgrade();
    }

    public void upgradeLaser()
    {
        shooterLaser.GetComponent<shooterLaser>().upgrade();
    }

    public void upgradeRocket()
    {
        shooterRocket.GetComponent<shooterRocket>().upgrade();
    }

    public void resumeUpgrades()
    {
        int bullevel = PlayerManager.Instance.stats.shooterBulletLevel;
        int laslevel = PlayerManager.Instance.stats.shooterLaserLevel;
        int rocklevel = PlayerManager.Instance.stats.shooterRocketLevel;
        shooterBullet.GetComponent<shooterBullet>().resumeUpgrades(bullevel);
        shooterLaser.GetComponent<shooterLaser>().resumeUpgrades(laslevel);
        shooterRocket.GetComponent<shooterRocket>().resumeUpgrades(rocklevel);
    }
    public int getLasersLevel()
    {
        return shooterLaser.GetComponent<shooterLaser>().getLevel();
    }
    public int getBulletsLevel()
    {
        return shooterBullet.GetComponent<shooterBullet>().getLevel();
    }
    public int getRocketsLevel()
    {
        return shooterRocket.GetComponent<shooterRocket>().getLevel();
    }
    public float getLasersDamage()
    {
        return shooterLaser.GetComponent<shooterLaser>().getDamage();
    }
    public float getBulletsDamage()
    {
        return shooterBullet.GetComponent<shooterBullet>().getDamage();
    }
    public float getRocketsDamage()
    {
        return shooterRocket.GetComponent<shooterRocket>().getDamage();
    }
    public int getBulletsCount()
    {
        return shooterBullet.GetComponent<shooterBullet>().getAmmoNumber();
    }
    public int getRocketsCount()
    {
        return shooterRocket.GetComponent<shooterRocket>().getAmmoNumber();
    }
    public int getTotalBullets()
    {
        return shooterBullet.GetComponent<shooterBullet>().getTotalAmmo();
    }
    public int getTotalRockets()
    {
        return shooterRocket.GetComponent<shooterRocket>().getTotalAmmo();
    }
}
