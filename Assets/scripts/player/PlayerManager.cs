using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour,IDamageable<int>,IKIllable {

    private static PlayerManager _instance;
    public delegate void loadDelegate();
    public loadDelegate loadDel;

    public static PlayerManager Instance
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

        stats.position = new float[3];
        //loadStats();
        
    }

    private void Start()
    {
        loadDel += loadStats;
        loadDel += ShootersManager.Instance.resumeUpgrades;
        loadDel += pauseManager.Instance.loadPrices;
        loadDel += scoreManager.Instance.loadScores;
        SaveSystem.playGame();
    }

    [SerializeField] GameObject player;

    [Serializable]
    public class PlayerStats
    {
        public string playerName;
        public int health;
        public int maxhealth;
        public int coins;
        public int shooterBulletLevel;
        public int shooterLaserLevel;
        public int shooterRocketLevel;
        public int score;
        public float[] position;
    }

    [SerializeField] public PlayerStats stats = new PlayerStats();

    public int getHealth()
    {
        return stats.health;
    }

    public int getMaxHealth()
    {
        return stats.maxhealth;
    }

    public void saveStats()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerStats.stats", FileMode.OpenOrCreate);
        stats.playerName = SaveSystem.playerName;
        //stats.health = PlayerManager.Instance.getHealth();
        stats.coins = CoinManager.Instance.getTotalCoins();
        //shooters saving
        stats.shooterBulletLevel = ShootersManager.Instance.getBulletsLevel();
        stats.shooterLaserLevel = ShootersManager.Instance.getLasersLevel();
        stats.shooterRocketLevel = ShootersManager.Instance.getRocketsLevel();
        stats.score = GUIManager.Instance.getScore();
       
        //position saving
        stats.position[0] = player.transform.position.x;
        stats.position[1] = player.transform.position.y;
        stats.position[2] = player.transform.position.z;
        bf.Serialize(file, stats);
        file.Close();
        if (SaveSystem.newPlayer)
        {
            SaveSystem.newPlayer = false;
        }
    }

    public void loadStats()
    {
        if (File.Exists(Application.persistentDataPath + "/playerStats.stats"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerStats.stats", FileMode.Open);

            stats = (PlayerStats) bf.Deserialize(file);
           
            file.Close();
            SaveSystem.playerName = stats.playerName;
            CoinManager.Instance.setTotalCoins(stats.coins);
            GUIManager.Instance.setScore(stats.score);
            player.transform.position = new Vector3(stats.position[0], stats.position[1], stats.position[2]);
        }
        else
        {
            Debug.LogError("stats file not found");
        }
    }

    public bool Damage(int damage)
    {
        SoundManager.Instance.PlayPlayerAttack();
        stats.health -= damage;
        if (stats.health <= 0)
        {
            SoundManager.Instance.PlayDefeat();
            Kill();
        }
        return true;
    }
    public void Kill()
    {
        //gestione endmenu e score
        int score = GUIManager.Instance.getScore();
        Debug.Log("dai " + score + " " + stats.playerName);
        scoreManager.Instance.addscore(stats.playerName, score);
        scoreManager.Instance.saveScores();
        //scoreManager.Instance.printScores();
        lostManager.Instance.endGame();
    }
    public void resetPos()
    {
        player.transform.resetPosition();
    }
    public void teleport(Vector3 tel)
    {
        player.transform.teleport(tel);
    }
    public void resetHealth()
    {
        stats.health = stats.maxhealth;
    }

}
