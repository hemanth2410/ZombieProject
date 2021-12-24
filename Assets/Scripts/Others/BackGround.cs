using System;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField] private Backgrounds[] bgs;

    private SpriteRenderer bgSprite;
    private Color envTint;
    private Color cloudTint;
    #region Singleton
    public static BackGround current;
    void Awake()
    {
        if (current != null && current != this)
        {

            Destroy(gameObject);
            return;
        }


        current = this;
    
    }
    #endregion

    private void OnEnable()
    {
        GameEvents.current.OnSceneRestart += ChangeBackground;
    }
    private void OnDisable()
    {
        GameEvents.current.OnSceneRestart -= ChangeBackground;
    }
    //private void Start()
    //{
    //    bgSprite = GetComponent<SpriteRenderer>();

    //}

    private void ChangeBackground(int level, int score)
    {
        bgSprite = GetComponent<SpriteRenderer>();
        int i = UnityEngine.Random.Range(0, bgs.Length);
        //try
        //{
        //    bgSprite.sprite = bgs[i].background;
        //}
        //catch(Exception ex)
        //{
        //    Debug.LogError(ex.Message);
        //}
        bgSprite.sprite = bgs[i].background;

        envTint = bgs[i].envTint;
        cloudTint = bgs[i].cloudTint;
        GameEvents.current.BGChange(envTint, cloudTint);



    }
}
