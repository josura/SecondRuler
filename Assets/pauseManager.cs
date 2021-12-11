using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class pauseManager : MonoBehaviour {

    [SerializeField] Text bulletdamage, laserdamage, rocketdamage;
    [SerializeField] Text bulletcost, lasercost, rocketcost;
    [SerializeField] GameObject pauseGUI;
    int costBullet, costLaser, costRocket;

    private static pauseManager _instance;

    public static bool isGamePaused;
    [SerializeField] bool esterno;
    public static pauseManager Instance
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
        isGamePaused = false;
        costBullet = 20;
        costLaser = 10;
        costRocket = 30;
        pauseGUI.SetActive(false);
    }

    private void Update()
    {
        esterno = isGamePaused;
        if (! isGamePaused && !lostManager.isGameEnded)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseGame();
            }
        }
        else if (!lostManager.isGameEnded)
        {
            bulletdamage.text = "" + ShootersManager.Instance.getBulletsDamage();
            bulletcost.text = "" + costBullet;
            laserdamage.text = "" + ShootersManager.Instance.getLasersDamage();
            lasercost.text = "" + costLaser;
            rocketdamage.text = "" + ShootersManager.Instance.getRocketsDamage();
            rocketcost.text = "" + costRocket;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                unpauseGame();
            }
        }

    }

    public void pauseGame()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        isGamePaused = true;
        pauseGUI.SetActive(true);
    }

    public void unpauseGame()
    {
        Time.timeScale = 1;
        isGamePaused = false;
        pauseGUI.SetActive(false);
    } 

    public void saveGame()
    {
        SaveSystem.save();
    }

    public void upgradeShooterBullet()
    {
        int coinsAfterTrans = CoinManager.Instance.getTotalCoins() - costBullet;
        if (coinsAfterTrans >= 0)
        {
            ShootersManager.Instance.upgradeBullet();
            CoinManager.Instance.setTotalCoins(coinsAfterTrans);
            costBullet += 20;
        }
    }

    public void upgradeShooterLaser()
    {
        int coinsAfterTrans = CoinManager.Instance.getTotalCoins() - costLaser;
        if (coinsAfterTrans >= 0)
        {
            ShootersManager.Instance.upgradeLaser();
            CoinManager.Instance.setTotalCoins(coinsAfterTrans);
            costLaser += 10;
        }
    }

    public void upgradeShooterRocket()
    {
        int coinsAfterTrans = CoinManager.Instance.getTotalCoins() - costRocket;
        if (coinsAfterTrans >= 0)
        {
            ShootersManager.Instance.upgradeRocket();
            CoinManager.Instance.setTotalCoins(coinsAfterTrans);
            costRocket += 50;
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void mainmenu()
    {
        Time.timeScale = 1;
        isGamePaused = false;
        AsyncOperation p = SceneManager.LoadSceneAsync(0);

    }

    public void loadPrices()
    {
        costLaser  = 10* ShootersManager.Instance.getLasersLevel();
        costBullet = 20* ShootersManager.Instance.getBulletsLevel();
        costRocket = 50*ShootersManager.Instance.getRocketsLevel();
    }

    
}
