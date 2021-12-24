using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPortal : MonoBehaviour
{
    Material portalMat;
    float radius;
    bool levelStart;
    //private void Awake()
    //{
    //    GameManager.OnLevelStart += GameManager_OnLevelStart;
    //}

    //private void GameManager_OnLevelStart(object sender, System.EventArgs e)
    //{
    //    levelStart = true;


    //}

    void Start()
    {
        portalMat = GetComponent<Renderer>().sharedMaterial;

    }

    // Update is called once per frame
    void Update()
    {
        //if(levelStart == true)
        //{
        //    radius = Mathf.Lerp(0, 1, 0.8f);
        //    portalMat.SetFloat("_radius", radius);
        //}
        


    }
}
