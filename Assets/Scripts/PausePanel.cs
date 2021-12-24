using UnityEngine;
public class PausePanel : MonoBehaviour
{
  
	
    public void ResumeLevel()
	{
		
		Time.timeScale = 1;
		
		

	}

	public void GoToHomeScreen()
	{
		GameEvents.current.SaveGame();
		GameInitializer.instance.LoadMainMenu();
	
	}
	 
}
