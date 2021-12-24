using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassBed : MonoBehaviour
{
    Renderer rend;
    Material mat;
    [SerializeField]float growSpeed;
    float clip;
    void Start()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;
        clip = 1f;
        Destroy(this.gameObject, 5f);
    }

    
    void Update()
    {
        clip -= Time.deltaTime * growSpeed;

        clip = Mathf.Clamp(clip, 0, 1);
        mat.SetFloat("_clip", clip);
    }

    
}
