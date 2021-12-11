using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour {
    [SerializeField] private GameObject enemy1,enemy2; //prossimamente per più tipi di enemy[] enemyTypes;
    [SerializeField] private GameObject[] enemyList;
    [SerializeField] private int enemyListLength;
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private float timeToSpawn;
    private float tEnemySpawn = 100;
    private int enemyCounter = 0;

    private static EnemyManager _instance;

    public static EnemyManager Instance
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
        spawnPoint = GameObject.Find("spawnpoints").transform.Cast<Transform>().ToArray();
    }


    // Use this for initialization
    void Start () {
        enemyList = new GameObject[enemyListLength];

        //Riempiamo l'array dei nostri nemici con delle copie del prefab "enemy" e disattiviamo gli oggetti una volta instanziati
        for (int i = 0; i < enemyListLength; i++)
        {
            if (Random.Range(0,100) > 50)
            {
                enemyList[i] = GameObject.Instantiate(enemy1, spawnPoint[0].position, spawnPoint[0].rotation);
            }
            else
            {
                enemyList[i] = GameObject.Instantiate(enemy2, spawnPoint[0].position, spawnPoint[0].rotation);
            }
            enemyList[i].SetActive(false);
        }

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeToSpawn);
        }
    }
	

    void SpawnEnemy()
    {
        tEnemySpawn = 0;
        for (int i = enemyCounter; i < enemyListLength; i++)
        {
            if (!enemyList[i].activeInHierarchy)
            {
                enemyList[i].GetComponent<Enemy>().Spawn(spawnPoint[Random.Range(0,spawnPoint.Length-1)]);
                enemyList[i].SetActive(true);

                enemyCounter = i + 1;
                break;
            }
            if (i == enemyListLength - 1)
                enemyCounter = 0;

        }
        if (enemyCounter >= enemyListLength)
        {
            enemyCounter = 0;
        }
    }
    public void resetAll()
    {
        for (int i = 0; i < enemyListLength; i++)
        {
            enemyList[i].SetActive(false);
        }
    }
}
