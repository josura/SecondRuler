using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoins : Coins {

    private void Awake()
    {
        coinsAmount = (int)coinType.gold;
        coinscore = (int)coinScore.gold;
    }
}
