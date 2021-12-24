using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    #region Singleton
    public static SceneFader fadeinstance;


    private void Awake()
    {
        if (fadeinstance != null && fadeinstance != this)
        {

            Destroy(gameObject);
            return;
        }

        fadeinstance = this;


    }
    #endregion

    Animator anim;
    int FadeInParamID;
    
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        FadeInParamID = Animator.StringToHash("FadeIn");
        
    }

    //public IEnumerator FadeIn(int buildIndex)
    //{
    //    anim.SetTrigger(FadeInParamID);

    //    yield return new WaitForSeconds(1f);

    //    SceneManager.LoadScene(buildIndex);
    //}

    public void FadeIn()
    {
        anim.SetBool(FadeInParamID,true);
    }
    public void FadeOut()
    {
        anim.SetBool(FadeInParamID, false);
    }


}
