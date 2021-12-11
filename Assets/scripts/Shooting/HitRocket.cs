using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitRocket : MonoBehaviour {

    [SerializeField] int damage;

    public void SetRocketType(int damageAmount)
    {
        damage = damageAmount;
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.layer);
        if (other.gameObject.layer == 9)
        {  //collision with enemy
           //Debug.Log ("Colpito");
            other.GetComponent<Enemy>().Damage(damage);
            transform.gameObject.SetActive(false);

        }
    }

    void OnBecameInvisible()
    {
        //Debug.Log ("Invisibile");
        gameObject.SetActive(false);
    }
}
