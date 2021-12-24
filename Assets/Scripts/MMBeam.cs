
using UnityEngine;

public class MMBeam : MonoBehaviour
{

   
    void Start()
    {
        GetComponent<Renderer>().material.SetFloat("_isActive", 1);
    }

}
