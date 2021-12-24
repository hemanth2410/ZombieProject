using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int coinAmount;
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject coinEffect;
    [SerializeField] private CoinPopUp coinPopup;

    public void AddCoinFlash(Vector3 pos)
    {

        LevelManager.current.AddSingleCoin(coinAmount);
        GameObject _coinEffect =  Instantiate(coinEffect,pos,Quaternion.identity,transform);
        AudioManager.PlayCoinCollectAudio();
        CoinPopUp _coinPop = Instantiate(coinPopup, pos, Quaternion.identity, this.transform);
        _coinPop.PopUp(coinAmount);
        Destroy(coin);
        Destroy(_coinEffect, 2f);
        Destroy(_coinPop, 2f);
    
    }
  
  

}
