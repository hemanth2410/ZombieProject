using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassBedBase : MonoBehaviour
{
    Renderer rend;
    Material mat;
    [SerializeField] float growSpeed;
    float expand;
    void Start()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;
        expand = 0.5f;
        Destroy(this.gameObject, 5f);
    }


    void Update()
    {
        expand += Time.deltaTime * growSpeed;

        expand = Mathf.Clamp(expand, 0.5f, 1);
        mat.SetFloat("_expand", expand);
    }
}
