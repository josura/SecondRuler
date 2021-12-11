using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oculusHeli : flyingEnemy {

    
    private void Start()
    {
        enemyType = "oculus";
        enemyScore = 60;
    }



    protected override GameObject dropCoins(Transform trans)
    {
        trans.position.Set(trans.position.x, 0, trans.position.z);
        return CoinManager.Instance.createSilverCoins(trans);
        //Debug.Log( temp);
    }

    
}
