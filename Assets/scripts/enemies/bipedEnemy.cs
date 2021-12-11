using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bipedEnemy : walkingEnemy {

    private WaitForSeconds attackWaitingTime = new WaitForSeconds(1f);
    GameObject target;
    bool inRange=false;
    [SerializeField]int damage = 3;

    private void Start()
    {
        enemyType = "biped";
        enemyScore = 40;
    }



    protected override GameObject dropCoins(Transform trans)
    {
        GameObject temp = base.dropCoins(trans);
        //Debug.Log( temp);
        return temp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            //collision with player
            //other.gameObject.GetComponent<PlayerManager>().Damage(damage);
            StartCoroutine(tryAttack());
            inRange = true;
            

        }
        if (other.gameObject.layer == 12)
        {  //collision with coins
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            //collision with player
            //other.gameObject.GetComponent<PlayerManager>().Damage(damage);
            inRange = false;
            StopCoroutine(tryAttack());


        }
        if (other.gameObject.layer == 12)
        {  //collision with coins

        }
    }

    IEnumerator tryAttack()
    {
        while (true)
        {
            if (inRange)
            {
                PlayerManager.Instance.Damage(damage);
                //Debug.Log("player colpito");
                SoundManager.Instance.PlayEnemyClip(enemyType);
                launchBatons();
            }
            yield return attackWaitingTime;
        }
    }

    protected void launchBatons()
    {
        //find a way to create a boomerang effect using the two batons
    }
}
