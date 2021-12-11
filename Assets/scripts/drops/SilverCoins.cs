using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverCoins : Coins {

    private void Awake()
    {
        coinsAmount = (int)coinType.silver;
        coinscore = (int)coinScore.silver;
    }
}
