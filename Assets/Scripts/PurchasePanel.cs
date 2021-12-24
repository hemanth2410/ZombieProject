using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePanel : MonoBehaviour
{
    [SerializeField] ShopManager shopMan;
    [SerializeField] Button purchaseAllCharBtn;
    [SerializeField] Button purchaseAllUFOBtn;
    [SerializeField] Button purchaseAllBeamBtn;
   //public void PurchaseAllChar()
   // {
   //     if(PlayerPrefs.HasKey("allChars") == false)
   //     {
   //         PlayerPrefs.SetInt("allChar", 0);
   //         shopMan.UnlockAllAvatars();
   //         purchaseAllCharBtn.interactable = false;
   //     }
   // }
   // public void PurchaseAllUfos()
   // {
   //     if (PlayerPrefs.HasKey("allUfos") == false)
   //     {
   //         PlayerPrefs.SetInt("allUfos", 0);
   //         shopMan.UnlockAllUFOs();
   //         purchaseAllUFOBtn.interactable = false;
   //     }
   // }

   // public void PurchaseAllBeams()
   // {
   //     if (PlayerPrefs.HasKey("allBeams") == false)
   //     {
   //         PlayerPrefs.SetInt("allBeams", 0);
   //         shopMan.UnlockAllBeams();
   //         purchaseAllBeamBtn.interactable = false;
   //     }
   // }

    public void PurchaseAllChar()
    {
            shopMan.UnlockAllAvatars();
            purchaseAllCharBtn.interactable = false;
        GameEvents.current.SaveGame();
        // unlocked avatar   int list in shop manager ...add all remaining ints to this 
    }
    public void PurchaseAllUfos()
    {
     
            shopMan.UnlockAllUFOs();
            purchaseAllUFOBtn.interactable = false;
        GameEvents.current.SaveGame();
    }

    public void PurchaseAllBeams()
    {
       
            shopMan.UnlockAllBeams();
            purchaseAllBeamBtn.interactable = false;
        GameEvents.current.SaveGame();
    }

}
