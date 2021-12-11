using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

    [SerializeField] protected Image lifebar;
    [SerializeField] protected Text healthTXT;
    [SerializeField] protected Text coins;
    [SerializeField] protected Text scoreTXT;
    [SerializeField] protected Text bullets, rockets;

    protected int score;

    private static GUIManager _instance;
    public static GUIManager Instance
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
        Screen.SetResolution(SaveSystem.GetWidth(), SaveSystem.GetHeight(), System.Convert.ToBoolean(SaveSystem.GetFullscreen()));
    }

    protected void OnEnable()
    {
        int health =PlayerManager.Instance.getMaxHealth();
        healthTXT.text = "" + health + "\\" + health;
        lifebar.fillAmount = 1f;
    }

    private void Update()
    {
        int health = PlayerManager.Instance.getHealth();
        int maxhealth = PlayerManager.Instance.getMaxHealth();
        healthTXT.text = "" + health + "/" + maxhealth;
        lifebar.fillAmount = health / (float)maxhealth;
        coins.text = "" + CoinManager.Instance.getTotalCoins();
        scoreTXT.text = "Score: " + score;
        bullets.text = ShootersManager.Instance.getBulletsCount() + "/" + ShootersManager.Instance.getTotalBullets();
        rockets.text = ShootersManager.Instance.getRocketsCount() + "/" + ShootersManager.Instance.getTotalRockets();
    }

    public int getScore()
    {
        return score;
    }

    public void setScore(int _score)
    {
        
        score= _score;
        
    }

    public void addScore(int s)
    {
        score += s;
    }

}
