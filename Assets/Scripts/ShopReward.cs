using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopReward : MonoBehaviour
{
    AdMobInitializer admobInst;

    private void Awake()
    {
        admobInst = AdMobInitializer._instance;
    }


    public void ShowAd()
    {

        admobInst.ShowShopRewardedAd();
        
    }
}
