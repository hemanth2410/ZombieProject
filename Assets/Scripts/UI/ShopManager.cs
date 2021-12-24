using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class ShopManager : MonoBehaviour
{
    public static ShopManager current;
    GameManager gmInst;
    private int skinCost = 300;

    void Awake()
    {
        if (current != null && current != this)
        {

            Destroy(gameObject);
            return;
        }


        current = this;
        gmInst = GameManager.current;
    }

    [SerializeField] private GameObject uiItemPrefab;

    private List<Item> playerUI;
    private List<Item> uFOUI;
    private List<Item> beamUI;

    public static List<int> unlockedAvatars;
    public static List<int> unlockedUFOs;
    public static List<int> unlockedBeams;

    public static int selectedAvatarIndex;
    public static int selectedUFOIndex;
    public static int selectedBeamIndex;


    [SerializeField] public ScrollRect scrollRect;
    [SerializeField] private ShopTabButton[] tabButtons;
    [SerializeField] private GameObject[] tabPanels;
    [SerializeField] private GameObject[] purchaseButtons;

    [SerializeField] private Sprite[] tabNotSelectedIcons;
    [SerializeField] private Sprite[] tabSelectedIcons;

    [SerializeField] private TextMeshProUGUI coinText;

    [SerializeField] private TextMeshProUGUI catagoryNameText;
    int index;

    [SerializeField] ItemType defaultSelectedShopItemType;


    private void OnEnable()
    {
        GameEvents.current.OnCoinChanged += UpdateCoinText;
    }

    private void OnDisable()
    {
        GameEvents.current.OnCoinChanged -= UpdateCoinText;
        
    }

    public void Start()
    {
        playerUI = gmInst.avatars;
        uFOUI = gmInst.uFOs;
        beamUI = gmInst.beams;
        foreach (Item item in playerUI)   // for each always iterate in order (sure for a list dont know about gameobject children transform)
        {
            CreateShopItem(item);

        }
        foreach (Item item in uFOUI)
        {
            CreateShopItem(item);
        }
        foreach (Item item in beamUI)
        {
            CreateShopItem(item);
        }

        OnTabSelected(defaultSelectedShopItemType);

        if(unlockedAvatars.Count == 0)
        {
            UnlockAvatar(0);
        }
        if(unlockedBeams.Count == 0)
        {
            UnlockBeam(0);
        }
        if(unlockedUFOs.Count == 0)
        {
            UnlockUFO(0);
            
        }

        UpdateCoinText(GameManager.totalCoins);
       
        ShowUnlockedItems();
        HighlightSelectedAvatar();
        HighlightSelectedUFO();
        HighlightSelectedBeam();
  
    }

    void ShowUnlockedItems()
    {
      
        foreach (int item in unlockedAvatars)
        {
            tabPanels[0].transform.GetChild(item).GetComponent<ItemUI>().Unlocked();
        }
        foreach (int item in unlockedUFOs)
        {
            tabPanels[1].transform.GetChild(item).GetComponent<ItemUI>().Unlocked();
        }
        foreach (int item in unlockedBeams)
        {
            tabPanels[2].transform.GetChild(item).GetComponent<ItemUI>().Unlocked();
        }
    }

    void HighlightSelectedAvatar()
    {

        for (int i = 0; i < tabPanels[0].transform.childCount; i++)
        {
            if(i == selectedAvatarIndex)
            {
                tabPanels[0].transform.GetChild(i).GetComponent<ItemUI>().Selected();
            }
            else
            {
                tabPanels[0].transform.GetChild(i).GetComponent<ItemUI>().Deselect();
            }
           
        }

    }
    void HighlightSelectedUFO()
    {

        for (int i = 0; i < tabPanels[1].transform.childCount; i++)
        {
            if (i == selectedUFOIndex)
            {
                tabPanels[1].transform.GetChild(i).GetComponent<ItemUI>().Selected();
            }
            else
            {
                tabPanels[1].transform.GetChild(i).GetComponent<ItemUI>().Deselect();
            }

        }

    }
    void HighlightSelectedBeam()
    {

        for (int i = 0; i < tabPanels[2].transform.childCount; i++)
        {
            if (i == selectedBeamIndex)
            {
                tabPanels[2].transform.GetChild(i).GetComponent<ItemUI>().Selected();
            }
            else
            {
                tabPanels[2].transform.GetChild(i).GetComponent<ItemUI>().Deselect();
            }

        }

    }

    public void CreateShopItem(Item uiItemSO)
    {
        GameObject shopItem = Instantiate(uiItemPrefab);
        shopItem.GetComponent<ItemUI>().item = uiItemSO;   //Link UIprefab with ItemSO 
        SetShopInfo(uiItemSO, shopItem); 
    }

    public void SetShopInfo(Item uiItemSO, GameObject shopItem)
    {

        int i = (int)uiItemSO.ItemType.Category;

            shopItem.transform.SetParent(tabPanels[i].transform);
            shopItem.transform.localPosition = new Vector3(shopItem.transform.localPosition.x, shopItem.transform.localPosition.y, 0.0f);
            shopItem.transform.localScale = Vector3.one;
    
       
      
    }

    public void OnTabSelected(ItemType itemType)
    {
        ResetTabs();
        index = (int)itemType.Category;
        tabButtons[index].img.sprite = tabSelectedIcons[index];
        tabButtons[index].GetComponent<RectTransform>().sizeDelta = new Vector2(152, tabButtons[index].GetComponent<RectTransform>().sizeDelta.y);
        scrollRect.content = tabPanels[index].GetComponent<RectTransform>();
        catagoryNameText.text = itemType.Category.ToString();           
     
   
        for (int i = 0; i < tabPanels.Length; i++)
        {
            if (i == index)
            {
                tabPanels[i].SetActive(true);
                purchaseButtons[i].SetActive(true);  // only if we didnt make purchase
            }
            else
            {
                tabPanels[i].SetActive(false);
                purchaseButtons[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
     
        for (int i = 0; i < tabPanels.Length; i++)
        {
            tabButtons[index].GetComponent<RectTransform>().sizeDelta = new Vector2(152, tabButtons[index].GetComponent<RectTransform>().sizeDelta.y);
            tabButtons[index].img.sprite = tabNotSelectedIcons[index];
        }
    }

    //add random no to list 
    private List<int> GetUnlockedIntAvatarList(out bool status,int cost)
    {
        System.Random rand = new System.Random();
        int number;
        status = false;
        
        if ( unlockedAvatars.Count < tabPanels[0].transform.childCount)
        {
         
            do
            {
                if (unlockedAvatars.Count == 0)
                {
                    number = 0;
                }
                else
                {
                    number = rand.Next(1, tabPanels[0].transform.childCount);
                }
                
            } while (unlockedAvatars.Contains(number));
            {
                if (GameManager.current.TrySpendCoinAmount(cost))
                {
                    status = true;

                    unlockedAvatars.Add(number);
                }
             
            
            }
        }
      

        return unlockedAvatars;


    }
    private List<int> GetUnlockedIntUFOList(out bool status,int cost)
    {
        System.Random rand = new System.Random();
        status = false;
        int number;
        if(unlockedUFOs.Count < tabPanels[1].transform.childCount)
        {
            do
            {
                if (unlockedUFOs.Count == 0)
                {
                    number = 0;
                }
                else
                {
                    number = rand.Next(1, tabPanels[1].transform.childCount);
                }
            } while (unlockedUFOs.Contains(number));
            {
                if (GameManager.current.TrySpendCoinAmount(cost))
                {
                    status = true;
                    unlockedUFOs.Add(number);
                }
         
          
            }
        }
        
       
        return unlockedUFOs;

    }

    private List<int> GetUnlockedIntBeamList(out bool status, int cost)
    {
        System.Random rand = new System.Random();
        status = false;
        int number;
        if(unlockedBeams.Count < tabPanels[2].transform.childCount)
        {
            do
            {
                if (unlockedBeams.Count == 0)
                {
                    number = 0;
                }
                else
                {
                    number = rand.Next(1, tabPanels[2].transform.childCount);
                }
            } while (unlockedBeams.Contains(number));
            {
                if (GameManager.current.TrySpendCoinAmount(cost))
                {
                    status = true;
                    unlockedBeams.Add(number);
                }
         
             
            }
        }
        
        return unlockedBeams;

    }

    private void UnlockAvatar(int cost)
    {
        bool unlockStatus;
        List<int> FloatingList = GetUnlockedIntAvatarList(out unlockStatus,cost);
        
        if(unlockStatus && FloatingList.Count == 1)
        {
            tabPanels[0].transform.GetChild(0).GetComponent<ItemUI>().Unlocked();
           // tabPanels[0].transform.GetChild(0).GetComponent<ItemUI>().UnlockPopUp();
           // AudioManager.PlayUnlockAudio();
        }

        if (unlockStatus && FloatingList.Count > 1)
        {

            int latestItemIndex = FloatingList[FloatingList.Count - 1];
    

            tabPanels[0].transform.GetChild(latestItemIndex).GetComponent<ItemUI>().Unlocked();
            tabPanels[0].transform.GetChild(latestItemIndex).GetComponent<ItemUI>().UnlockPopUp();

            AudioManager.PlayUnlockAudio();

        }
      

    }

   

    private void UnlockUFO(int cost)
    {
        bool unlockStatus;
        List<int> FloatingList = GetUnlockedIntUFOList(out unlockStatus,cost);
        if (unlockStatus && FloatingList.Count == 1)
        {
            tabPanels[1].transform.GetChild(0).GetComponent<ItemUI>().Unlocked();
          //  tabPanels[1].transform.GetChild(0).GetComponent<ItemUI>().UnlockPopUp();
           // AudioManager.PlayUnlockAudio();
        }
        if (unlockStatus && FloatingList.Count > 1)
        {
            
            int latestItemIndex = FloatingList[FloatingList.Count - 1];
            tabPanels[1].transform.GetChild(latestItemIndex).GetComponent<ItemUI>().Unlocked();
            tabPanels[1].transform.GetChild(latestItemIndex).GetComponent<ItemUI>().UnlockPopUp();
            AudioManager.PlayUnlockAudio();

        }
        
        
    }
    private void UnlockBeam(int cost)
    {
        bool unlockStatus;
        List<int> FloatingList = GetUnlockedIntBeamList(out unlockStatus,cost);

        if (unlockStatus && FloatingList.Count == 1)
        {
            tabPanels[2].transform.GetChild(0).GetComponent<ItemUI>().Unlocked();
          //  tabPanels[2].transform.GetChild(0).GetComponent<ItemUI>().UnlockPopUp();
           // AudioManager.PlayUnlockAudio();
        }

        if (unlockStatus && FloatingList.Count > 1)
        {
            
            int latestItemIndex = FloatingList[FloatingList.Count - 1];
            tabPanels[2].transform.GetChild(latestItemIndex).GetComponent<ItemUI>().Unlocked();
            tabPanels[2].transform.GetChild(latestItemIndex).GetComponent<ItemUI>().UnlockPopUp();
            AudioManager.PlayUnlockAudio();

        }
        
    }
  
    public void Unlock()
    {

        switch (index)
        {
            case 0: UnlockAvatar(skinCost); break;
            case 1: UnlockUFO(skinCost); break;
            case 2: UnlockBeam(skinCost); break;
        }
      
        GameEvents.current.SaveGame();

    }

   

    public void OnItemClick(Item item)
    {
        switch ((int)item.ItemType.Category)
        {
            case 0: SelectAvatar(item); break;
            case 1: SelectUFO(item); break;
            case 2: SelectBeam(item); break;
        }
    
        
    }

    private void SelectAvatar(Item avatarSkin)
    {
      
        selectedAvatarIndex = avatarSkin.IndexPosition;
        HighlightSelectedAvatar();
        GameEvents.current.SaveGame();
        GameEvents.current.ShowMMAvatar();

    }
    private void SelectUFO(Item uFOSkin)
    {
       
        selectedUFOIndex = uFOSkin.IndexPosition;
        HighlightSelectedUFO();
        GameEvents.current.SaveGame();
        GameEvents.current.ShowMMUFO();
    }
    private void SelectBeam(Item BeamSkin)
    {
      
        selectedBeamIndex = BeamSkin.IndexPosition;
        HighlightSelectedBeam();
        GameEvents.current.SaveGame();
        GameEvents.current.ShowMMBeam();
    }



    public void UpdateCoinText(int amount)
    {
        coinText.text = amount.ToString();
    }

    public void UnlockAllAvatars()
    {
        for (int i = 0; i < tabPanels[0].transform.childCount; i++)
        {
            tabPanels[0].transform.GetChild(i).GetComponent<ItemUI>().Unlocked();
    
            AudioManager.PlayUnlockAudio();
        }

    }
    public void UnlockAllUFOs()
    {
        for (int i = 0; i < tabPanels[1].transform.childCount; i++)
        {
            tabPanels[1].transform.GetChild(i).GetComponent<ItemUI>().Unlocked();
         
            AudioManager.PlayUnlockAudio();
        }

    }
    public void UnlockAllBeams()
    {
        for (int i = 0; i < tabPanels[2].transform.childCount; i++)
        {
            tabPanels[2].transform.GetChild(i).GetComponent<ItemUI>().Unlocked();
         
            AudioManager.PlayUnlockAudio();
        }

    }

}
