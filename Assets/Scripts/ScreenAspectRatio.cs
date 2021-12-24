using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenAspectRatio : MonoBehaviour
{
    
    [SerializeField] Image maskTransition;
    private RectTransform canvas;
    [SerializeField]private RectTransform startPos;
    [SerializeField]private RectTransform endPos;
    float radius;
    bool GameFadeIn;

    private void OnEnable()
    {
        GameEvents.current.OnLevelEndTrigger += FadeIn;
        GameEvents.current.OnMissileHit += FadeIn;
    }

    private void OnDisable()
    {
        GameEvents.current.OnLevelEndTrigger -= FadeIn;
        GameEvents.current.OnMissileHit -= FadeIn;
    }

    void Start()
    {
        canvas = GetComponent<RectTransform>();
        maskTransition.rectTransform.sizeDelta = new Vector2(canvas.rect.height, canvas.rect.height);


        FadeOut();


    }

    public void FadeIn()
    {
        StartCoroutine(FadeInDelay());
    }
    IEnumerator FadeInDelay()
    {
        yield return new WaitForSeconds(1f);
        maskTransition.enabled = true;
        EndTransition();

        yield return new WaitForSeconds(1f);

        maskTransition.enabled = false;

    }
    public void FadeOut()
    {
        StartCoroutine(FadeOutDelay());
    }
    IEnumerator FadeOutDelay()
    {
        maskTransition.enabled = true;
       
        StartTransition();

        yield return new WaitForSeconds(1f);

        maskTransition.enabled = false;
    }


    private void Update()
    {
    
        if (GameFadeIn)
        {
            if(radius < 1)
            {
                radius += Time.deltaTime;
                maskTransition.material.SetFloat("Radius", radius);
            }
        }
        else
        {
            if(radius > 0)
            {
                radius -= Time.deltaTime;
                maskTransition.material.SetFloat("Radius", radius);
            }
        }

       
    }
    void StartTransition()
    {
        

        float xPosRemap = Remap(startPos.anchoredPosition.x, -Screen.width / 2,Screen.width / 2,0,1);
        float yPosRemap = Remap(startPos.anchoredPosition.y, -Screen.height/2,Screen.height/2,0,1);
        
        maskTransition.material.SetFloat("Center_X", xPosRemap);
        maskTransition.material.SetFloat("Center_Y", yPosRemap);

        
        radius = 0;
        maskTransition.material.SetFloat("Radius", radius);
        GameFadeIn = true;

    }

    void EndTransition()
    {
        float xPosRemap = Remap(endPos.anchoredPosition.x, -Screen.width / 2, Screen.width / 2, 0, 1);
        float yPosRemap = Remap(endPos.anchoredPosition.y, -Screen.height / 2, Screen.height / 2, 0, 1);
        maskTransition.material.SetFloat("Center_X", xPosRemap);
        maskTransition.material.SetFloat("Center_Y", yPosRemap);

       
        radius = 1;
        maskTransition.material.SetFloat("Radius", radius);
        GameFadeIn = false;
    }

    float Remap(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }


   

}
