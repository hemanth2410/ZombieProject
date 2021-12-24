using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameInitializer : MonoBehaviour
{
    public static GameInitializer instance;
    [SerializeField] GameObject loadingScreen;
   
    List<AsyncOperation> sceneLoading = new List<AsyncOperation>();
    
    public static bool storyComplete;
    [SerializeField] Slider bar;

    public static bool reset;
    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        //reset = PlayerPrefs.GetInt("reset", 0) == 1 ? true : false;
        //if (!reset)
        //{
        //    PlayerPrefs.DeleteAll();

        //    reset = true;
        //    PlayerPrefs.SetInt("reset", reset ? 1 : 0);
        //}
       

        storyComplete = PlayerPrefs.GetInt("storyComplete", 0) == 1 ? true : false;

        if (!storyComplete)
        {
          
            sceneLoading.Add(SceneManager.LoadSceneAsync((int)SCENEINDEXES.STORY, LoadSceneMode.Additive));
        }
        else
        {
            
            loadingScreen.SetActive(true);
            
           
            StartCoroutine(GetFakeLoadProgress());
        }
    }

    public void LoadGame()   // go to game scene from main menu
    {
       

        sceneLoading.Add(SceneManager.UnloadSceneAsync((int)SCENEINDEXES.MAINMENU));
        sceneLoading.Add(SceneManager.LoadSceneAsync((int)SCENEINDEXES.GAME,LoadSceneMode.Additive));
       
        
    }

    public void LoadMainMenu()  // game to mainmenu
    {
       
        sceneLoading.Add(SceneManager.UnloadSceneAsync((int)SCENEINDEXES.GAME));
        sceneLoading.Add(SceneManager.LoadSceneAsync((int)SCENEINDEXES.MAINMENU, LoadSceneMode.Additive));
       
    }

    public void LoadMenu()    // story to main menu
    {
        loadingScreen.SetActive(true);
        sceneLoading.Add(SceneManager.UnloadSceneAsync((int)SCENEINDEXES.STORY));
       // sceneLoading.Add(SceneManager.LoadSceneAsync((int)SCENEINDEXES.MAINMENU,LoadSceneMode.Additive));
        StartCoroutine(GetFakeLoadProgress());
    }


    public void ReloadGameScene()  // reload game scene
    {
       
        sceneLoading.Add(SceneManager.UnloadSceneAsync((int)SCENEINDEXES.GAME));
        sceneLoading.Add(SceneManager.LoadSceneAsync((int)SCENEINDEXES.GAME, LoadSceneMode.Additive));
      
       
    }
    //float totalSceneProgress;

    //public IEnumerator GetSceneLoadProgress()
    //{
    //    for (int i = 0; i < sceneLoading.Count; i++)
    //    {
    //        while (!sceneLoading[i].isDone)
    //        {
    //            totalSceneProgress = 0;

    //            foreach (AsyncOperation operation in sceneLoading)
    //            {
    //                totalSceneProgress += operation.progress;

    //            }

    //            totalSceneProgress = totalSceneProgress / sceneLoading.Count;

    //            bar.fillAmount = totalSceneProgress;
    //            yield return null;
    //        }
    //    }

    //    loadingScreen.gameObject.SetActive(false);


    //}


    int steps = 3;

    public IEnumerator GetFakeLoadProgress()
    {
        //for (int i = 0; i <= steps; i++)
        //{
        //    yield return new WaitForEndOfFrame();
        //    bar.value = i + Time.deltaTime;
        //    if ((int)i == 3)
        //    {
        //        yield return new WaitForSeconds(0.5f);
        //    }
        //}
        float j = 0;
        while(j <= steps)
        {
            yield return new WaitForEndOfFrame();
            bar.value = j + Time.deltaTime;
            j += Time.deltaTime;
            if((int)j == steps)
            {
                yield return new WaitForSecondsRealtime(0.5f);
            }
        }

       // loadingScreen.gameObject.SetActive(false);
        sceneLoading.Add(SceneManager.LoadSceneAsync((int)SCENEINDEXES.MAINMENU, LoadSceneMode.Additive));
        for (int i = 0; i < sceneLoading.Count; i++)
        {
          //  Debug.Log(sceneLoading.Count);
          //  Debug.Log(sceneLoading[i].isDone);
            while (!sceneLoading[i].isDone)
            {
                yield return null;
            }
           
        }
        
        loadingScreen.SetActive(false);

       
    }

   

}
