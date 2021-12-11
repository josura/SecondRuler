using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitOrbital : MonoBehaviour {

    float damage = 30;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.layer);
        if (other.gameObject.layer == 9)
        {  //collision with enemy
            other.GetComponent<Enemy>().Damage(damage);

        }
    }
}
