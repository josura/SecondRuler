using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour {

    private static CoinManager _instance;
    [SerializeField] GameObject goldCoins, silverCoins, bronzeCoins;
    [SerializeField] int totalCoins;
    GameObject[] coinsList;
    [SerializeField]int coinsListLength=100;
    int coinsCounter;

    public static CoinManager Instance
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

    void Start()
    {
        coinsList = new GameObject[coinsListLength];

        //Riempiamo l'array dei nostri nemici con delle copie del prefab "enemy" e disattiviamo gli oggetti una volta instanziati
        for (int i = 0; i < coinsListLength; i++)
        {
            int randNum = Random.Range(0, 2);
            GameObject temp = null;
            switch (randNum)
            {
                case 0:
                    temp = goldCoins;
                    break;
                case 1:
                    temp = silverCoins;
                    break;
                case 2:
                    temp = bronzeCoins;
                    break;
                default:
                    break;
            }
            coinsList[i] = GameObject.Instantiate(temp);
            coinsList[i].SetActive(false);
        }
    }

    public GameObject createGoldCoins(Transform trans)
    {
        return GameObject.Instantiate(goldCoins, trans.position, trans.rotation);
    }
    public GameObject createSilverCoins(Transform trans)
    {
        return GameObject.Instantiate(silverCoins, trans.position, trans.rotation);
    }
    public GameObject createBronzeCoins(Transform trans)
    {
        return GameObject.Instantiate(bronzeCoins, trans.position, trans.rotation);
    }

    public int addcoins(int _coins)
    {
        totalCoins += _coins;
        return totalCoins;
    }
    public int getTotalCoins()
    {
        return totalCoins;
    }
    public void setTotalCoins(int coin)
    {
        totalCoins = coin;
    }
}
