using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {
    public enum coinType
    {
        bronze = 1,
        silver = 5,
        gold = 15
    }
    public enum coinScore
    {
        bronze = 5,
        silver = 20,
        gold = 100
    }
    protected int coinsAmount;
    protected int coinscore;
	// Use this for initialization
	void Start () {
        transform.setY(transform.position.y+3);
	}
	
    public int getAmount()
    {
        return coinsAmount;
    }

    public int getScore()
    {
        return coinscore;
    }
    

    void Spawn(Transform spawnPoint,int amount)
    {
        transform.Spawn(spawnPoint);
        coinsAmount = amount;
    }
}
