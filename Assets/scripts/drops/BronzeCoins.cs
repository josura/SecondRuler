using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BronzeCoins : Coins {

    private void Awake()
    {
        coinsAmount = (int)coinType.bronze;
        coinscore = (int)coinScore.bronze;
    }
}
