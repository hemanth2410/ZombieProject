using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testforeachorder : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(Order());
        Debug.Log("start");
    }
    IEnumerator Order()
    {
        foreach (Transform childTransform in transform)
        {
            Debug.Log(childTransform.GetSiblingIndex().ToString());
            yield return new WaitForSeconds(1);
        }

    }

    //-----------------Conclusion-------------------//


    // for each always iterate in order 
    // wether it is list or parent transform
    // List 
    // private List<Sprite> sprites;
    // foreach(Sprite sprite in Sprites)
    //{
    //  // do something
    //}


    //-----------------------Question - Does foreachloop wait execution----------------------------//


}
