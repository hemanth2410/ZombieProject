using System;
using UnityEngine;
using System.Collections.Generic;


public class GameEvents : MonoBehaviour
{
	public static GameEvents current;

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

	

	public event Action<int,int> OnSceneRestart;
	public event Action<Vector3> OnUFOShoot;
	
	public event Action<int> OnTreeGrow;
	public event Action OnLevelEndTrigger;

	public event Action<int, int, TreeTypes,int,int,int> OnLevelWin;
	public event Action<int,string> OnLevelLoose;
	public event Action OnMissileHit;
	public event Action OnHouseHit;

	public event Action OnSaveGame;
	public event Action OnLoadGame;
	public event Action<List<TreeTypes>, List<TreeTypes>, int,int> OnGameInitialize;
	public event Action<int> OnCoinChanged;

	public event Action OnAvatarSkinSelect;
	public event Action OnUFOSkinSelect;
	public event Action OnBeamSkinSelect;

	public event Action OnEnterMainMenu;

	public event Action<string> OnShowInstruction;

	public event Action OnRemoveInstruction;

	public event Action OnFirstTimeMenu;  // resigter
	public event Action<string> OnNameChange;

	//public event Action OnGameRewardSuccess;
	public event Action<Color,Color> OnBGChange;

	public void BGChange(Color envColor,Color cloudColor)
    {
		OnBGChange?.Invoke(envColor,cloudColor);
    }

	//public void GameRewardSuccess()
   // {
	//	OnGameRewardSuccess?.Invoke();
   // }

    public void NameChange(string name)
    {
		OnNameChange?.Invoke(name);
    }

	public void FirstTimeMenu()
    {
		OnFirstTimeMenu?.Invoke();
    }

	public void LevelEndTrigger()
    {
	
	  OnLevelEndTrigger?.Invoke();

		
    }

	public void RemoveInstruction()
    {
		OnRemoveInstruction?.Invoke();
    }

	public void ShowInstruction(string inst)
    {
		OnShowInstruction?.Invoke(inst);
    }

    public void TreeGrow(int levelScore)
    {

        OnTreeGrow?.Invoke(levelScore);

    }

    public void CoinChanged(int totalCoins)
    {
   
        OnCoinChanged?.Invoke(totalCoins);
    }
  
	public void EnterMainMenu()
    {
		OnEnterMainMenu?.Invoke();
    }

	public void GameInitialize(List<TreeTypes> treeList,List<TreeTypes> unlockedTreeList, int totalCoin, int currentLevel)
    {
		OnGameInitialize?.Invoke(treeList,unlockedTreeList,totalCoin, currentLevel);
    }

	public void LevelLoose(int score,string message)
    {
		OnLevelLoose?.Invoke(score,message);
    }
	public void LevelWin(int starsAchieved, int starsAccum,TreeTypes currentTree,int score,int totalScore,int currentLevel)
    {

		OnLevelWin?.Invoke(starsAchieved, starsAccum, currentTree, score,totalScore,currentLevel);
    }

	public void MissileHit()
    {
		OnMissileHit?.Invoke();
    }
	public void HouseHit()
	{
		OnHouseHit?.Invoke();
	}
	public void SceneRestart(int currentLevel,int levelScore)
	{
	
			OnSceneRestart?.Invoke(currentLevel,levelScore);

	}
	public void UFOShoot(Vector3 spawnPos)
	{
	
			OnUFOShoot?.Invoke(spawnPos);
	
	}

	public void SaveGame()
	{
	
			OnSaveGame?.Invoke();
		
	}
	public void LoadGame()
	{
	
			OnLoadGame?.Invoke();
		
	}

	public void ShowMMAvatar()
    {
		OnAvatarSkinSelect?.Invoke();
    }

	public void ShowMMUFO()
    {
		OnUFOSkinSelect?.Invoke();
    }
	public void ShowMMBeam()
    {
		OnBeamSkinSelect?.Invoke();
    }
	


}
