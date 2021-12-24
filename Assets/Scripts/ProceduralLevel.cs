using UnityEngine;
using System.Collections.Generic;
public class ProceduralLevel : MonoBehaviour
{
    public static ProceduralLevel instance;

    [SerializeField] private Transform groundChunk;
  
    [SerializeField] private Transform endChunk;
    [SerializeField] private EnemyTypes[] enemies;
    [SerializeField] private List<EnemyTypes> popUpEnemies;
    [SerializeField] private EnemyTypes[] militaryEnemies;
    [SerializeField] private EnemyTypes popupMilitaryEnemies;

    [SerializeField] private int minGroundUnit = 8;
    [SerializeField] private int maxGroundUnit = 15;

    [SerializeField] private int minNumberOfGroundChunk = 7;
    [SerializeField] private int maxNumberOfGroundChunk = 10;


    int totalGroundChunk;

    [HideInInspector]public int nextXSize;
    [HideInInspector]public int totalNumberOfUnitBlock;
   

    private PlatformMovement levelPlatform;

    private int popupChance;

    private bool military;
    private bool isMilitaryEligible;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
      
        Init();

    }

    public void Init()
    {

        levelPlatform = GetComponent<PlatformMovement>();
        nextXSize = 0;
        totalNumberOfUnitBlock = 0;
        military = false;
        isMilitaryEligible = false;
        totalGroundChunk = Random.Range(minNumberOfGroundChunk, maxNumberOfGroundChunk);
        if(GameManager.currentLevel <= 3)
        {
            popupChance = 0;
        }
        else if (GameManager.currentLevel > 3 && GameManager.currentLevel <= 18) // 5 to 19
        {
            
            popupChance = 20;
            
            
        }
        else if(GameManager.currentLevel > 18 && GameManager.currentLevel <= 48) //20 to 49
        {
            popupChance = 30;
           
        }
        else                                    // more than 50
        {
            popupChance = 40;
           
        }

        if(GameManager.currentLevel > 8)
        {
            isMilitaryEligible = true;
        }
        if (GameManager.currentLevel > 13)
        {
            popUpEnemies.Add(popupMilitaryEnemies);
        }
        if (GameManager.currentLevel > 23)
        {
            popUpEnemies.Add(popupMilitaryEnemies);
           
        }
        if(GameManager.currentLevel > 33)
        {
            popUpEnemies.Add(popupMilitaryEnemies);
        }
        float halfWay = Mathf.Floor(totalGroundChunk / 2);
        float firstHalfRand = Random.Range(0, halfWay);
        float SecondHalfRand = Random.Range(halfWay, totalGroundChunk);
        float militaryRand = Random.Range(0, totalGroundChunk);
       
        for (int i = 0; i < totalGroundChunk; i++)
        {
            if (i == (int)militaryRand && isMilitaryEligible)
            {
                military = true;
            }
            else
            {
                military = false;
            }
            if (i < halfWay)
            {
               
                if (i == (int)firstHalfRand)
                {
                    
                    Spawn(GROUNDTYPES.COIN);
                }

                else
                {
                    
                    Spawn(GROUNDTYPES.NORMAL);
                }
            }
            else
            {
                
                if (i == (int)SecondHalfRand)
                {
                   
                    Spawn(GROUNDTYPES.COIN); 
                }

                else
                {
                   
                    Spawn(GROUNDTYPES.NORMAL);
                }
            }
        }
        SpawnEndPart();

    }

  
    private void Spawn(GROUNDTYPES groundType)
    {
        Transform GroundPart = Instantiate(groundChunk, new Vector3(nextXSize,0,0), Quaternion.identity, levelPlatform.transform);
        GroundPart.GetComponent<ProceduralGround>().numberOfUnit = Random.Range(minGroundUnit, maxGroundUnit);
        GroundPart.GetComponent<ProceduralGround>().SpawnGroundChunk(groundType);

        nextXSize += GroundPart.GetComponent<ProceduralGround>().numberOfUnit;
        AccumulateGroundBlock(GroundPart.GetComponent<ProceduralGround>().numberOfUnit) ;   // to calculate total unit for score
    
        int randomNumber = Random.Range(0, 101);

        if(randomNumber < popupChance)
        {
            int randomEnemy = Random.Range(0, popUpEnemies.Count);
            Transform EnemyPart = Instantiate(popUpEnemies[randomEnemy].enemyPrefab, new Vector3(nextXSize, 0, 0), Quaternion.identity, levelPlatform.transform);
            if(EnemyPart.GetComponent<PopupEnemy>() != null)
            {
                nextXSize += EnemyPart.GetComponent<PopupEnemy>().xScale;
              
            }
            else if(EnemyPart.GetComponent<MilitaryPopup>() != null)
            {
                nextXSize += EnemyPart.GetComponent<MilitaryPopup>().xScale;
               
            }
          
        }
        else
        {
            if (military)
            {
                Transform MEnemyPart = Instantiate(militaryEnemies[0].enemyPrefab, new Vector3(nextXSize, 0, 0), Quaternion.identity, levelPlatform.transform);
                nextXSize += MEnemyPart.GetComponent<MilitaryBase>().xScale;
              
            }
            else
            {
                int randomEnemy = Random.Range(0, enemies.Length);
                Transform EnemyPart = Instantiate(enemies[randomEnemy].enemyPrefab, new Vector3(nextXSize, 0, 0), Quaternion.identity, levelPlatform.transform);
                nextXSize += EnemyPart.GetComponent<Enemy>().xScale;
            }
         
        }
        
    }

  
    private void SpawnEndPart()
    {
         Instantiate(endChunk, new Vector3(nextXSize,0,0), Quaternion.identity,levelPlatform.transform);

    }

    private void AccumulateGroundBlock(int numberOfUnitsInSpawnedChunk)
    {
         totalNumberOfUnitBlock += numberOfUnitsInSpawnedChunk;

    }

    public int TotalGroundBlock()
    {
        return totalNumberOfUnitBlock;
    }


}
