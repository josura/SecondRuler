using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTurret : MainTurret {

	[SerializeField] GameObject[] bulletList;
	[SerializeField] int bulletLength;
	[SerializeField] Transform spawnPoint;
	[SerializeField] GameObject bullet;


	[SerializeField] private int bulletCounter=0;
	private bool bulletFound=false;

	// Use this for initialization
	void Awake () {

		bulletList = new GameObject[bulletLength];
		for(int i=0;i<bulletLength;i++)
		{
			bulletList[i]=Instantiate(bullet,spawnPoint.position,spawnPoint.rotation);
			bulletList [i].GetComponent<HitBullet> ().SetBulletType ((int)myTurret);
			bulletList [i].SetActive (false);
		}
	}

	IEnumerator Shoot()
	{

		while (true) {

			yield return new WaitForSeconds (timeToSpawn);
			SpawnBullet ();
			//SoundManager.instance.PlayTurretClip (tag);
		}
	}

	void SpawnBullet()
	{
		bulletFound = false;
		while (!bulletFound) {
			for (int i = bulletCounter; i < bulletLength; i++) {
				if (!bulletList [i].activeInHierarchy) {
					bulletList [i].GetComponent<Bullet> ().Spawn (spawnPoint);
					bulletCounter = i + 1;
					bulletFound = true;
					break;
				}
				if (i == bulletLength-1)
					bulletCounter = 0;
			}
			if (bulletCounter == bulletLength)
				bulletCounter = 0;

		}
	}
}
