using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
	
	[SerializeField] private TextMeshProUGUI scoreText;         
	[SerializeField] private TextMeshProUGUI winScoreText;         
	[SerializeField] private TextMeshProUGUI levelText;         
	[SerializeField] private TextMeshProUGUI treeProgressText;         
	[SerializeField] private Image fullTree;         
	[SerializeField] private Image emptyTree;         

	[SerializeField] private TextMeshProUGUI shopCoinText;
	
	[SerializeField] private GameObject levelWinScreen;  
	
	[SerializeField] private GameObject levelLooseScreen;
	[SerializeField] private TextMeshProUGUI looseMessage;
	[SerializeField] private GameObject levelPauseScreen;
	[SerializeField] private Transform ShopPanel;
	[SerializeField] private GameObject PauseButton;
	[SerializeField] private Button shopButton;
	
	[SerializeField] private GameObject coinWinScreen;   
	[SerializeField] private GameObject continueButton;  
	[SerializeField] private GameObject continueButtonAfterUnlock;  
	
	[SerializeField] private GameObject treeWinScreen;
	[SerializeField] private Animator progressAnim;
	
	[SerializeField] private GameObject Hud;

	[SerializeField] private GameObject[] stars;

	[SerializeField] Image progressBar;

	[SerializeField] private RectTransform endPos;
	LevelManager lmInst;
	[SerializeField]UnlockedTreePopup unlockedPopup;
	[SerializeField]Transform starContainer;

	[SerializeField] GameObject instructionText;
	[SerializeField] GameObject instructionTextshadow;

	IEnumerator starEffect;
	int starsLeft;
	int timesIndex = 0;

	[SerializeField] GameObject StarUIPrefab;
	UnlockedTreePopup _unlockedPopup;
	GameObject starForTreeCollected;

	[SerializeField]Transform blackCover;



	void OnEnable()
    {

	
		GameEvents.current.OnTreeGrow += UpdateHUDScoreUI;
		GameEvents.current.OnLevelLoose += ShowLevelLooseScreen;
		GameEvents.current.OnLevelWin += ShowLevelWinScreen;
		GameEvents.current.OnSceneRestart += Init;
		GameEvents.current.OnShowInstruction += ShowInstruction;
		GameEvents.current.OnRemoveInstruction += RemoveInstruction;
		
	}


	private void OnDisable()
    {
		
		GameEvents.current.OnTreeGrow -= UpdateHUDScoreUI;
		GameEvents.current.OnLevelLoose -= ShowLevelLooseScreen;
		GameEvents.current.OnLevelWin -= ShowLevelWinScreen;
		GameEvents.current.OnSceneRestart -= Init;
		GameEvents.current.OnShowInstruction -= ShowInstruction;
		GameEvents.current.OnRemoveInstruction -= RemoveInstruction;
	
	}
	private void Start()
    {
			
		   lmInst = LevelManager.current;
		blackCover.gameObject.SetActive(false);
	}

	private void ShowInstruction(string inst)
    {
		instructionText.GetComponent<TextMeshProUGUI>().text = inst;
		instructionTextshadow.GetComponent<TextMeshProUGUI>().text = inst;
		instructionTextshadow.SetActive(true);
		instructionText.SetActive(true);
		Time.timeScale = 0f;
		
	}
	
	private void RemoveInstruction()
    {
		Time.timeScale = 1f;
		instructionText.SetActive(false);
		instructionTextshadow.SetActive(false);
		
	}
	

	private void Update()
    {
	
		progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, LevelManager.current.CalcProgress(), 0.1f);

	}

    public void Init(int level,int levelScore)   // add level score?
    {
	
		Hud.SetActive(true);
		levelWinScreen.SetActive(false);
		levelLooseScreen.SetActive(false);
		levelPauseScreen.SetActive(false);

		
		levelText.text = "LEVEL " + (level + 1);
		scoreText.text = levelScore.ToString();               // ui start value(this is hard coded..see gamemanager start() levelScore
													 //also hard coded both are diffrent but should be one)
   
	}
	public void UpdateHUDScoreUI(int score)
	{
		
		scoreText.text = score.ToString();
	    

	}

	public void ShowLevelWinScreen(int starsAchieved, int totalStars, TreeTypes currentTree,int score,int totalScore,int currentLevel)
	{
		PauseButton.GetComponent<Button>().interactable = false;
		StartCoroutine(LevelWinScreen(starsAchieved, totalStars, currentTree, score));

	}
	IEnumerator LevelWinScreen(int starsAchieved, int totalStars, TreeTypes currentTree, int score)
	{
		yield return new WaitForSeconds(2f);


		Hud.SetActive(false);
		coinWinScreen.SetActive(false);
		if (GameManager.allTreeUnlocked)
		{
			treeWinScreen.SetActive(false);
		}
		else
		{
			treeWinScreen.SetActive(true);

		}

		//levelWinScreen.SetActive(true);
		ShowPanel(levelWinScreen.transform);
		// Analytics  _ SUCCESS
		Debug.Log("ANALYTICS_SUCCESS" + GameManager.currentLevel + "LEVEL");
		Analytics.CustomEvent("Level " + GameManager.currentLevel + " Completed", new Dictionary<string, object>
				{
					{ "Level completed", GameManager.currentLevel
		}
				});

		//--------------------------------
		winScoreText.text = "SCORE " + score;
		starEffect = StarEffect(starsAchieved, totalStars, currentTree);
		StartCoroutine(starEffect);
		// coin reward for level and extra coins both alreay added in gamemanager. but we want to show total coin wihout those.
		shopCoinText.text = (GameManager.totalCoins - lmInst.coinReward - lmInst.totalExtraCoins).ToString();
	}
	IEnumerator StarEffect(int starsAchieved,int totalStars , TreeTypes currentTree)  
	{
		starsLeft = starsAchieved;
		int startingStar = totalStars;
		int times = 0;
		if (!GameManager.allTreeUnlocked)
        {
			fullTree.sprite = currentTree.fullTree;
			emptyTree.sprite = currentTree.emptyTree;

			fullTree.fillAmount = (float)startingStar / currentTree.starsRequiredToUnlock;
			treeProgressText.text = (startingStar.ToString() + " / " + currentTree.starsRequiredToUnlock.ToString());
		}
		for (int i = 0; i < starsAchieved; i++)
		{
			LevelManager.current.IncrementStars();
			totalStars++;
			startingStar++;
			starsLeft--;
			
			yield return new WaitForSeconds(0.25f);
		
			GameObject starCollected = Instantiate(StarUIPrefab, stars[i + timesIndex].transform);
			
			starForTreeCollected = starCollected.transform.GetChild(0).gameObject;
			starForTreeCollected.transform.DOScale(1, 0.25f).SetEase(Ease.OutBack);
			times++;
			AudioManager.PlayStarAudio();
			yield return new WaitForSeconds(0.5f);
			
			if (!GameManager.allTreeUnlocked)
			{
				bool status = CheckTreeUnlock(currentTree.starsRequiredToUnlock - totalStars);
				starForTreeCollected.transform.DOScale(0, 1f).SetEase(Ease.OutBack);
				starForTreeCollected.transform.DOMove(endPos.position, 0.25f, false).OnComplete(() => DestroyStar(starForTreeCollected));
				fullTree.fillAmount = (float)startingStar / currentTree.starsRequiredToUnlock;
				treeProgressText.text = (startingStar.ToString() + " / " + currentTree.starsRequiredToUnlock.ToString());
				if (status && starsLeft >= 0)
				{
					if (times == 1)
					{
						timesIndex = 1;
					}
					if (times == 2)
					{
						timesIndex = 2;
					}

					_unlockedPopup = Instantiate(unlockedPopup, levelWinScreen.transform);
					_unlockedPopup.UnlockEffect(currentTree);
					treeWinScreen.SetActive(false);
					GameManager.current.AllTreeStatus();
					SetNextTree();

					yield break;
				}


			}
			//         else
			//         {
			//	continueButtonAfterUnlock.SetActive(true);
			//	continueButtonAfterUnlock.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
			//}
			//		bool status = CheckTreeUnlock(currentTree.starsRequiredToUnlock - totalStars);



		}

		yield return new WaitForSeconds(1f);  
								
		continueButton.SetActive(true);
		continueButton.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);


	}

	private void DestroyStar(GameObject starCollected)
    {
		Destroy(starCollected);
		
	}

   
	void SetNextTree()
    {
	   
		LevelManager.current.SetNextTree();
	
	}

	public void ContinueUnlockTree()
	{

		_unlockedPopup.RemovePopup();
		treeWinScreen.SetActive(true);
		PopUpData data =  LevelManager.current.GetCurrentData();
		starEffect = StarEffect(starsLeft, data.totalStars, data.currentTree);
		StartCoroutine(starEffect);
	
	}
	private bool CheckTreeUnlock(int starsLeftToUnlocks)
	{

		if (starsLeftToUnlocks == 0)     // NEW
		{
		
			GameManager.currentTreeToUnlockIndex++;
			LevelManager.current.UpdateStars();
			return true;

		}


		return false;

	}


	public void ContinueToCoinScreen(Transform button)
    {
		treeWinScreen.SetActive(false);

		if(GameManager.totalCoins >= 300)
        {
			shopButton.gameObject.SetActive(true);

		}
        else
        {
			shopButton.gameObject.SetActive(false);
		}
		coinWinScreen.SetActive(true);
		coinWinScreen.GetComponent<CoinPanel>().ShowCoinWinScreen(lmInst.coinReward, lmInst.totalExtraCoins);
		button.GetComponent<Button>().interactable = false;
		button.transform.DOScale(0f, 0.5f).SetEase(Ease.OutBack);

	}
	



	public void ShowLevelLooseScreen(int score,string msg)
	{
		StartCoroutine(LevelLooseScreen(score, msg));
	}

	IEnumerator LevelLooseScreen(int score,string message)
    {
		yield return new WaitForSeconds(2f);
		looseMessage.text = message;
		//levelLooseScreen.SetActive(true);
		//levelLooseScreen.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
		ShowPanel(levelLooseScreen.transform);

		//Analytics ------------ FAILED
		Debug.Log("ANALYTICS_FAILED" + GameManager.currentLevel + "LEVEL");
		Analytics.CustomEvent("Level " + GameManager.currentLevel + " Failed", new Dictionary<string, object>
				{
					{ "Level Failed", GameManager.currentLevel
		}
				});

		//----------------------------------

		Hud.SetActive(false);
		levelLooseScreen.GetComponent<LoosePanel>().UpdateScoreText(score);
	}
	
	
	public void ShowPauseScreen()
	{

		StartCoroutine(PauseWait());
		
	}
	IEnumerator PauseWait()
    {
		levelPauseScreen.SetActive(true);
		blackCover.gameObject.SetActive(true);
		levelPauseScreen.transform.DOScale(1, 0.1f).SetEase(Ease.OutBack);
		yield return new WaitForSeconds(0.1f);
		Time.timeScale = 0;
	}

	public void NextLevel()
    {
		GameEvents.current.SaveGame();
		GameInitializer.instance.ReloadGameScene();
	}

	public void GoToHomeScreen()
    {
		GameEvents.current.SaveGame();
		GameInitializer.instance.LoadMainMenu();
	}

	//public void ShowShopPanel()
 //   {
	//	ShopPanel.gameObject.SetActive(true);
	//	ShopPanel.DOScale(1, 0.5f).SetEase(Ease.OutBack);
	//}

	//public void HideShopPanel()
	//{
	//	StartCoroutine(CloseShopPanel());
	//}
	//IEnumerator CloseShopPanel()
	//{

	//	ShopPanel.DOScale(0.95f, 0.1f);

	//	yield return new WaitForSeconds(0.1f);

	//	ShopPanel.gameObject.SetActive(false);
	

	//}

	public void ShowPanel(Transform panel)
	{
		panel.gameObject.SetActive(true);
		blackCover.gameObject.SetActive(true);
		panel.DOScale(1, 0.1f).SetEase(Ease.OutBack);
	}

	public void HidePanel(Transform panel)
	{
		StartCoroutine(ClosePanel(panel));
	}
	IEnumerator ClosePanel(Transform panel)
	{

		panel.DOScale(0.9f, 0.1f).SetEase(Ease.OutBack);
		blackCover.gameObject.SetActive(false);
		yield return new WaitForSeconds(0.1f);

		panel.gameObject.SetActive(false);


	}

}




