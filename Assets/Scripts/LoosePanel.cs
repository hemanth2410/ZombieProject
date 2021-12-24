using TMPro;
using UnityEngine;

public class LoosePanel : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoretext;
    
  
    //private void OnEnable()
    //{
    //   TouchEvents.OnTap += RestartScene;
    //}
    //private void OnDestroy()
    //{
    //    TouchEvents.OnTap -= RestartScene;
    //}
    


    public void UpdateScoreText(int score)
    {
        scoretext.text = score.ToString();
    }

    ////----------------to remove---------------//
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        RestartScene();
    //    }
    //}
    public void GoToHomeScreen()
	{
		GameEvents.current.SaveGame();
		GameInitializer.instance.LoadMainMenu();
	}
    public void RestartScene()
    {
    
        GameEvents.current.SaveGame();
        GameInitializer.instance.ReloadGameScene();
       
    }
}
