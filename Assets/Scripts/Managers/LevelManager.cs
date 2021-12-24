using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{

    public static LevelManager current;
    GameManager gmInst;

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

   

    int randTreeSet;
    [SerializeField] private GameObject[] treeSetPrefab;
    private int totalNumberOfUnits;
    [HideInInspector] public int levelScore;
    private int scorePercentage;
    [SerializeField] private int scoreFor1Star;
    [SerializeField] private int scoreFor2Star;
    [SerializeField] private int scoreFor3Star;

    ProceduralLevel proceduralLevel;
    PlatformMovement levelPlatform;
    GameObject levelObject;
    Transform levelObjectTransform;

    public static int totalStars;
    [HideInInspector] public int coinReward;
     [HideInInspector]public int totalExtraCoins;
    [HideInInspector]public int totalLevelCoinsGained;
    [SerializeField] private GameObject player;
  
   
    [SerializeField]private int adAfterThisNumberOfLevel = 2;
    public static int numberOfGameStarted;

    AdMobInitializer admob;
    [SerializeField] string message = "NOT ENOUGH TREES PLANTED";


    private Queue<GameObject> treeSetPool = new Queue<GameObject>();
    private int poolStartSize = 10;

    private void OnEnable()
    {

        GameEvents.current.OnLevelEndTrigger += LevelFinish;
        GameEvents.current.OnUFOShoot += GrowTreePatch;
       


    }
   

    private void OnDisable()
    {

        GameEvents.current.OnLevelEndTrigger -= LevelFinish;
        GameEvents.current.OnUFOShoot -= GrowTreePatch;
       

    }
   
    
    public void Start()
    {
        admob = AdMobInitializer._instance;
        GameEvents.current.LoadGame();
        AudioManager.PlayInGameBG();
        numberOfGameStarted++;
    

        if (numberOfGameStarted > adAfterThisNumberOfLevel)
        {

            //Show interstitial add after each 3 game attempts

            admob.ShowGameInterstitialAd();
            numberOfGameStarted = 0;
        }

        levelScore = 0;
        coinReward = 40;
        totalExtraCoins = 0;
        
        Init();
        RestartScene(GameManager.currentLevel);

        StartCoroutine(Intro());
    }
    private void Init()
    {
   
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)SCENEINDEXES.GAME));
        
    
       
        GameObject _levelObject = Resources.Load("Prefabs/Level") as GameObject;
        levelObject = Instantiate(_levelObject, Vector3.zero, Quaternion.identity);
        levelObjectTransform = levelObject.transform;
          proceduralLevel = levelObject.GetComponent<ProceduralLevel>();
        levelPlatform = levelObject.GetComponent<PlatformMovement>();
        totalNumberOfUnits = proceduralLevel.TotalGroundBlock();

        gmInst.SetUnlockedTree();
        gmInst.SetCurrentTree();

        for (int i = 0; i < poolStartSize; i++)
        {
            randTreeSet = Random.Range(0, treeSetPrefab.Length);
            GameObject treeSet = Instantiate(treeSetPrefab[randTreeSet], levelObjectTransform);
            treeSetPool.Enqueue(treeSet);
            treeSet.SetActive(false);

        }


    }

    IEnumerator Intro()
    {    

       
        yield return new WaitForSeconds(1.5f);   // match with camera handheld duration
        
      
        Instantiate(player, new Vector3(-17f, 13f, 0f), Quaternion.identity);
      
        levelPlatform.StartLevelMovement();
     
    }

    public void StopLevelMove()
    {
        levelPlatform.StopMovement("a");
    }

    public void GrowTreePatch(Vector3 spawnPosition)
    {

        //randTreeSet = Random.Range(0, treeSetPrefab.Length);
        //GameObject treeSet = Instantiate(treeSetPrefab[randTreeSet], spawnPosition, Quaternion.identity, levelObjectTransform);
        SpawnTreeSet(spawnPosition);
        
        
    }
    private void SpawnTreeSet(Vector3 pos)
    {
        GameObject treeSet = GetTreeSet();

        treeSet.transform.position = pos;
        
        levelScore++;

        GameEvents.current.TreeGrow(levelScore);
    }

    private GameObject GetTreeSet()
    {
    
            if (treeSetPool.Count > 0)
            {
                GameObject treeSet = treeSetPool.Dequeue();
                treeSet.gameObject.SetActive(true);

                return treeSet;
            }
            else
            {
                
                randTreeSet = Random.Range(0, treeSetPrefab.Length);
                GameObject treeSet = Instantiate(treeSetPrefab[randTreeSet], levelObjectTransform);
                return treeSet;
            }
       
    }
    public void ReturnTreeSet(GameObject treeSet)
    {
        treeSetPool.Enqueue(treeSet);
        treeSet.gameObject.SetActive(false);

    }
    public void LevelFinish()
    {


        if (ScoreNormalized() < scoreFor1Star)
        {

            GameEvents.current.LevelLoose(levelScore,message);
            AudioManager.PlayLooseAudio();

        }
        else
        {
            totalLevelCoinsGained = coinReward + totalExtraCoins;
            gmInst.IncrementLevel();
            gmInst.AddCoins(totalLevelCoinsGained);
            //gmInst.AddCoins(coinReward);
            //gmInst.AddCoins(totalExtraCoins);
            gmInst.AddScore(levelScore);
            AudioManager.PlayWinAudio();
            GameEvents.current.LevelWin(NumberOfStar(), totalStars , gmInst.currentTreeToUnlock,levelScore,GameManager.totalScore,GameManager.currentLevel);
    
        }

        Destroy(levelObject, 5f);

    }

    //public void AddExtraCoin()
    //{
    //    GameManager.totalCoins += totalExtraCoins;
        
    //}

    public void AddSingleCoin(int amount)
    {
        totalExtraCoins += amount;
       
    }

    public void IncrementStars()
    {
        totalStars++;
       
    }

    public void UpdateStars()
    {
        TreeTypes currentTree = gmInst.currentTreeToUnlock;
        totalStars -= currentTree.starsRequiredToUnlock;
      
    }
    private void RestartScene(int currentLevel)
    {
        GameEvents.current.SceneRestart(currentLevel,levelScore);
   
        
    }
    public void SetNextTree()
    {
        gmInst.SetCurrentTree();
    }

    public PopUpData GetCurrentData()
    {
        PopUpData data = new PopUpData();
        data.totalStars = totalStars;
       
        data.currentTree = gmInst.currentTreeToUnlock;
        return data;
    }

    private int ScoreNormalized()
    {
        scorePercentage = Mathf.CeilToInt((levelScore * 100) / totalNumberOfUnits);
        return scorePercentage;

    }

    public float CalcProgress()
    {
        return ((levelPlatform.DistanceCovered()) / proceduralLevel.nextXSize);

    }
    private int NumberOfStar()
    {
        if (ScoreNormalized() >= scoreFor1Star && ScoreNormalized() < scoreFor2Star)
        {
            return 1;
           
        }
        else if (ScoreNormalized() >= scoreFor2Star && ScoreNormalized() < scoreFor3Star)
        {
            return 2;
          
        }

        else if (ScoreNormalized() >= scoreFor3Star)
        {
            return 3;
            
        }
        else
        {
            return 0;
        }


    }
 
}


    

public class PopUpData
{
    
    public int totalStars; 
   
    public TreeTypes currentTree;
    public TreeTypes nextTree;


}