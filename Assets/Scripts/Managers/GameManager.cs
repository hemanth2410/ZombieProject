using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager current;
    void Awake()
    {
        if (current != null && current != this)
        {

            Destroy(gameObject);
            return;
        }


        current = this;
        DontDestroyOnLoad(gameObject);

    }
    #endregion

    public static int currentLevel;
    public static int numberOfGift;
    public static int totalCoins;
    public static int currentTreeToUnlockIndex;
    public static bool enableVibrate;

    public TreeTypes currentTreeToUnlock;
 
    public List<TreeTypes> unlockedTrees;
    public List<TreeTypes> treesInSequence;

    public List<Item> avatars;
    public List<Item> uFOs;
    public List<Item> beams;

    public static bool allTreeUnlocked;

    public static bool registered;
  
    public static int totalScore;
    bool onBanner;
    
    public static int NumberOfGift
    {
        get { return numberOfGift; }
        set
        {
            if (numberOfGift <= 0)
            {
                numberOfGift = 0;
            }
            else
            {
                numberOfGift = value;
            }
        }
    }
  
    private void OnEnable()
    {
        GameEvents.current.OnEnterMainMenu += MainMenuInit;
     
     
    }
    
    private void OnDisable()
    {
        GameEvents.current.OnEnterMainMenu -= MainMenuInit;
     
    }

    public void MainMenuInit()
    {
        Time.timeScale = 1f;
        GameEvents.current.LoadGame();
        if (!registered)
        {

            GameEvents.current.FirstTimeMenu();

        }
       
     
        SetUnlockedTree(); 
        SetCurrentTree();
        
        GameEvents.current.GameInitialize(treesInSequence, unlockedTrees, totalCoins, currentLevel);
        GameEvents.current.CoinChanged(totalCoins);
    }

    

    public void SetUnlockedTree()
    {
        unlockedTrees = new List<TreeTypes>();

        for (int i = 0; i < currentTreeToUnlockIndex + 1; i++)
        {

            unlockedTrees.Add(treesInSequence[i]);
           
        }
    }
    public void SetCurrentTree()
    {
        AllTreeStatus();
        if (allTreeUnlocked)   // NEW
        {
            //currentTreeToUnlock = null;
            return;
        }
        else
        {
            currentTreeToUnlock = treesInSequence[currentTreeToUnlockIndex + 1];
        }
       
      
    }

   
    public void AddCoins(int amount)
    {
        totalCoins += amount;
        GameEvents.current.CoinChanged(totalCoins);

    }

    public void AddScore(int amount)
    {
        totalScore += amount;
        
    }

    public bool TrySpendCoinAmount(int spendCoinAmount)
    {
        if(totalCoins >= spendCoinAmount)
        {
            totalCoins -= spendCoinAmount;
            GameEvents.current.CoinChanged(totalCoins);
            return true;
        }
        else { return false; }
    }
    public void AllTreeStatus()
    {
     
        if (currentTreeToUnlockIndex >= treesInSequence.Count - 1)
        {
            allTreeUnlocked = true;
           
        }
        else 
        {
            allTreeUnlocked = false;
           
        }
        ;
    }
    public void IncrementLevel()
    {
        currentLevel++;
      
        if((currentLevel+1) % 10 == 0)
        {
            IncrementGift();
         
        }
    }
    public void IncrementGift()
    {
        numberOfGift++;
    

    }
    public void DecrementGift()
    {
        numberOfGift--;
      
    }

    public void UpdateVibration()
    {
        enableVibrate = PlayerPrefs.GetInt("vibration", 1) == 0? false:true;

       // Debug.Log("update vibration" + enableVibrate);
    }

    public GameObject GetRandomUnlockedTree()
    {
        System.Random random = new System.Random();
        return unlockedTrees[random.Next(unlockedTrees.Count)].treePrefab;
    }
}
