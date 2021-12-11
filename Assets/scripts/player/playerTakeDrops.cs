using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTakeDrops : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            //collision with enemy
           
        }
        if (other.gameObject.layer == 12)
        {  //collision with coins
            int amount = other.GetComponent<Coins>().getAmount();
            //Debug.Log("adding " + amount + " coins");
            CoinManager.Instance.addcoins(amount);
            GUIManager.Instance.addScore(other.GetComponent<Coins>().getScore());
            //Debug.Log("destroying" + other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
