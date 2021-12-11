using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerData {

    public int health;
    public int coins;
    public int shooterBulletLevel;
    public int shooterLaserLevel;
    public int shooterRocketLevel;
    public float[] position;

    public playerData(int _health,int _coins, int bulLevel,int lasLevel, int rocLevel, Vector3 position)
    {
        health = (_health);
        coins = _coins;
        shooterBulletLevel = bulLevel;
        shooterLaserLevel = lasLevel;
        shooterRocketLevel = rocLevel;
        //position = new float[3];
    }
}
