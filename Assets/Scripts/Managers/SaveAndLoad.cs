using UnityEngine;
using System.Collections.Generic;

public class SaveAndLoad : MonoBehaviour
{
	public static SaveAndLoad current;
   

    void Awake()
	{
		if (current != null && current != this)
		{

			Destroy(gameObject);
			return;
		}


		current = this;
		DontDestroyOnLoad(gameObject);
        GameEvents.current.OnSaveGame += Save;
        GameEvents.current.OnLoadGame += Load;

    }

    private void OnDisable()
    {
        GameEvents.current.OnSaveGame -= Save;
        GameEvents.current.OnLoadGame -= Load;
    }

    private void OnApplicationQuit()
    {
        Save();       /// TOOOOOOOOO CHECKKKKKKKKk
    }
    public void Save()
    {

        SaveObject saveObject = new SaveObject
        {
            _coins = GameManager.totalCoins,
            _level = GameManager.currentLevel,
            _totalStars = LevelManager.totalStars,
            _currentTreeIndex = GameManager.currentTreeToUnlockIndex,
            _unlockedAvatars = ShopManager.unlockedAvatars,
            _unlockedUFOs = ShopManager.unlockedUFOs,
            _unlockedBeams = ShopManager.unlockedBeams,
            _selectedAvatarIndex = ShopManager.selectedAvatarIndex,
            _selectedUFOIndex = ShopManager.selectedUFOIndex,
            _selectedBeamIndex = ShopManager.selectedBeamIndex,
            _currentGiftNumber = GameManager.numberOfGift,
            _registered = GameManager.registered,
           // _reset = GameInitializer.reset,
            _playerName = RegisterPanel.playerName,
            _totalScore = GameManager.totalScore,
            _numberOfGameBeforeAd = LevelManager.numberOfGameStarted


        };

        PlayerPrefs.SetInt("level", saveObject._level);
        PlayerPrefs.SetInt("coin", saveObject._coins);
        PlayerPrefs.SetInt("totalStars", saveObject._totalStars);
        PlayerPrefs.SetInt("currentTree", saveObject._currentTreeIndex);
      
        PlayerPrefs.SetInt("avatarIndex", saveObject._selectedAvatarIndex);
        PlayerPrefs.SetInt("UFOIndex", saveObject._selectedUFOIndex);
        PlayerPrefs.SetInt("beamIndex", saveObject._selectedBeamIndex);
        Serializer.instance.SerializeAvatars(saveObject._unlockedAvatars);
        Serializer.instance.SerializeBeams(saveObject._unlockedBeams);
        Serializer.instance.SerializeUFOs(saveObject._unlockedUFOs);
        PlayerPrefs.SetInt("giftNumber", saveObject._currentGiftNumber);
        PlayerPrefs.SetInt("totalScore", saveObject._totalScore);
        PlayerPrefs.SetInt("registered", saveObject._registered ? 1 : 0);
       // PlayerPrefs.SetInt("reset", saveObject._reset ? 1 : 0);
        PlayerPrefs.SetString("playername", saveObject._playerName);
        PlayerPrefs.SetInt("noOfGameBeforeAd", saveObject._numberOfGameBeforeAd);
     
      //  PlayerPrefs.SetInt("MasterVolume", saveObject._masterVolumeDB);

        // PlayerPrefs.SetInt("firstRunDone", saveObject._firstRunDone?1:0);
   
        //PlayerPrefs.SetInt("storyComplete", saveObject._storycomplete ? 1 : 0);
        // -----------------------------------------//


        //SaveObject saveObject = new SaveObject
        //{
        //    _coins = GameManager.coins,
        //    _level = GameManager.currentLevel,
        //    _totalStars = GameManager.totalStars,
        //    _currentTreeIndex = GameManager.currentTreeToUnlockIndex,
        //    _firstRunDone = GameManager.firstRunDone,
        //    _unlockedTrees = GameManager.unlockedTrees,


        //};
        //string json = JsonUtility.ToJson(saveObject);

        //File.WriteAllText(Application.dataPath + "/save.text", json);



    }
        
    public void Load()
    { 
      
        GameManager.totalCoins = PlayerPrefs.GetInt("coin",0);
        GameManager.currentLevel = PlayerPrefs.GetInt("level",0);
        LevelManager.totalStars = PlayerPrefs.GetInt("totalStars", 0);
        GameManager.currentTreeToUnlockIndex = PlayerPrefs.GetInt("currentTree",0);  // default is index 0 tree
        ShopManager.selectedAvatarIndex = PlayerPrefs.GetInt("avatarIndex", 0);
        ShopManager.selectedUFOIndex = PlayerPrefs.GetInt("UFOIndex", 0);
        ShopManager.selectedBeamIndex = PlayerPrefs.GetInt("beamIndex", 0);
        ShopManager.unlockedAvatars =  Serializer.instance.GetUnlockedAvatars();
        ShopManager.unlockedUFOs =  Serializer.instance.GetUnlockedUFOs();
        ShopManager.unlockedBeams =  Serializer.instance.GetUnlockedBeams();
        GameManager.numberOfGift = PlayerPrefs.GetInt("giftNumber",0);
        GameManager.totalScore = PlayerPrefs.GetInt("totalScore",0);
        GameManager.registered = PlayerPrefs.GetInt("registered", 0) == 1 ? true : false;
       
        RegisterPanel.playerName = PlayerPrefs.GetString("playername", "");
        LevelManager.numberOfGameStarted = PlayerPrefs.GetInt("noOfGameBeforeAd", 0);
      
      //  AudioManager.masterValue = PlayerPrefs.GetInt("MasterVolume", 0);
        // GameManager.firstRunDone = PlayerPrefs.GetInt("firstRunDone",0) == 1?true:false;
       
       
      
       

        //------------------------------------------------//
        //if (File.Exists(Application.dataPath + "/save.text"))
        //{
        //    string saveString = File.ReadAllText(Application.dataPath + "/save.text");

        //    SaveObject SaveObject = JsonUtility.FromJson<SaveObject>(saveString);

        //    GameManager.coins = SaveObject._coins;
        //    GameManager.currentLevel = SaveObject._level;
        //    GameManager.currentTreeToUnlockIndex = SaveObject._currentTreeIndex;
        //    GameManager.firstRunDone = SaveObject._firstRunDone;
        //    GameManager.unlockedTrees = SaveObject._unlockedTrees;
        //    GameManager.totalStars = SaveObject._totalStars;
        //    path.text = Application.dataPath;

        //}
        //else { return; }
    }

    public class SaveObject
    {
        public int _coins;
        public int _level;
        public int _totalStars;
        public int _currentTreeIndex;
        public int _currentGiftNumber;
        public bool _registered;
       // public bool _reset;
        public string _playerName;
        public int _totalScore;
        public int _numberOfGameBeforeAd;

        public List<int> _unlockedAvatars;
        public List<int> _unlockedUFOs;
        public List<int> _unlockedBeams;

        public int _selectedAvatarIndex;
        public int _selectedUFOIndex;
        public int _selectedBeamIndex;

    }


}

