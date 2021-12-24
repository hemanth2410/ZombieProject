using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private RectTransform SettingsPanel;
    [SerializeField] private RectTransform ShopPanel;
    [SerializeField] private RectTransform Treepanel;
    [SerializeField] private RectTransform registerPanel;
    [SerializeField] private RectTransform profilePanel;
    [SerializeField] private RectTransform leaderboardPanel;
    [SerializeField] private RectTransform CreditsPanel;

    [SerializeField] private GameObject black;
    [SerializeField] float panelTransitionTime = 0.1f;

    [SerializeField] List<TreeTypes> treeList;
    [SerializeField] List<TreeTypes> unlockedTreeList;

    [SerializeField] Image[] levelImagePoints;

    [SerializeField] TextMeshProUGUI levelText;

    [SerializeField] TextMeshProUGUI playerDisplay;
    [SerializeField] Image playerPicDisplay;
    
     
    GameManager gmInst;
    RegisterPanel registerPanelScript;
    
    private void OnEnable()
    {
        GameEvents.current.OnCoinChanged += UpdateCoins;
        GameEvents.current.OnGameInitialize += Init;
        GameEvents.current.OnFirstTimeMenu += ShowRegisterPanel;
        GameEvents.current.OnNameChange += NameChanged;
        GameEvents.current.OnAvatarSkinSelect += SetProfilePic;
      
    }
    private void OnDisable()
    {
        GameEvents.current.OnCoinChanged -= UpdateCoins;
        GameEvents.current.OnGameInitialize -= Init;
        GameEvents.current.OnFirstTimeMenu -= ShowRegisterPanel;

        GameEvents.current.OnNameChange -= NameChanged;
        GameEvents.current.OnAvatarSkinSelect -= SetProfilePic;
      
    }
    
    private void Start()
    {
    
        GameEvents.current.EnterMainMenu();
        
        gmInst = GameManager.current;
        registerPanelScript = registerPanel.GetComponent<RegisterPanel>();
        playerDisplay.text = RegisterPanel.playerName;
        SetProfilePic();

        AudioManager.current.PlayMainMenuAudio();
        
    }

   

    public void Init(List<TreeTypes> treeList , List<TreeTypes> unlockedTreeList,int totalCoin,int currentLevel)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)SCENEINDEXES.MAINMENU));
        UpdateCoins(totalCoin);
        Treepanel.GetComponent<TreeGallery>().CreateTreeUI(treeList,unlockedTreeList);
        LevelProgress(currentLevel);
       
    }

    private void LevelProgress(int currentLevel)
    {

        StartCoroutine(LevelProgressVisual(currentLevel));
        levelText.text = "LEVEL " + (currentLevel+1).ToString();

    }

   
    IEnumerator LevelProgressVisual(int currentLevel)
    {
        
        for (int i = 0; i < levelImagePoints.Length; i++)
        {
            
            if(i == ((currentLevel) % 10))
            {
                yield return new WaitForSeconds(0.5f);
                levelImagePoints[i].rectTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutBack);
                levelImagePoints[i].color = Color.white;


            }
            else
            {

                levelImagePoints[i].enabled = !displayLevelPoint(currentLevel, i);
               
                levelImagePoints[i].rectTransform.localScale = Vector3.one;
                
            }
            
        }
    }

   

    public void UpdateCoins(int amount)
    {
        StartCoroutine(UpdateCoinsEffect(amount));
    }
    IEnumerator UpdateCoinsEffect(int amount)
    {
        coinText.text = amount.ToString();
        coinText.rectTransform.DOScale(1.25f, 0.3f).SetEase(Ease.InOutBounce);
        yield return new WaitForSeconds(0.5f);
        coinText.rectTransform.DOScale(1f, 0.3f).SetEase(Ease.InOutBounce);
    }

    private bool displayLevelPoint(int level,int points)
    {
        return (((level) % 10) <= points); 
    }

    private void ShowRegisterPanel()
    {
        ShowPanel(registerPanel);
    }

    public void ShowSettingsPanel()
    {
        ShowPanel(SettingsPanel);

    }
    public void ShowShopPanel()
    {
        
        ShowPanel(ShopPanel);

    }
    
    public void ShowLeaderboardPanel()
    {
        ShowPanel(leaderboardPanel);
    }
    public void ShowTreepanel()
    {
        ShowPanel(Treepanel);
    }

    public void ShowProfilePanel()
    {
        ShowPanel(profilePanel);
    }
    public void ShowCreditsPanel()
    {
        ShowPanel(CreditsPanel);
    }
    private void ShowPanel(Transform panel)
    {
        black.SetActive(true);
        panel.gameObject.SetActive(true);
        panel.DOScale(1, panelTransitionTime);
        
    }

    public void GoToGameScene()
    {
        GameEvents.current.SaveGame();
        GameInitializer.instance.LoadGame();
    }

    public void HideRegisterPanel()
    {
        if (registerPanelScript.canAccept)
        {

            StartCoroutine(ClosePanel(registerPanel));
        }
        else { return; }
    }
    public void HideShopPanel()
    {
        StartCoroutine(ClosePanel(ShopPanel));
    }
    public void HideTreePanel()
    {
        StartCoroutine(ClosePanel(Treepanel));
    }
    public void HideSettingPanel()
    {
        StartCoroutine(ClosePanel(SettingsPanel));
    }
    public void HideProfilePanel()
    {
        StartCoroutine(ClosePanel(profilePanel));
    }
    public void HideLeaderboardPanel()
    {
        StartCoroutine(ClosePanel(leaderboardPanel));
    }
    public void HideCreditsPanel()
    {
        StartCoroutine(ClosePanel(CreditsPanel));
    }
    IEnumerator ClosePanel(Transform panel)
    {

        panel.DOScale(0.95f, panelTransitionTime);

        yield return new WaitForSeconds(panelTransitionTime);

        panel.gameObject.SetActive(false);
        black.SetActive(false);

    }

    public void NameChanged(string playerName)
    {
        playerDisplay.text = playerName;
    }

    public void SetProfilePic()
    {
        playerPicDisplay.sprite = gmInst.avatars[ShopManager.selectedAvatarIndex].UnlockedIcon;
       
    }


}
