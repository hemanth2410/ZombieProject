using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int xScale;
    [SerializeField] Human[] humans;
    [SerializeField] GameObject house;
    [SerializeField] GameObject destroyedHouse;
    [SerializeField] GameObject impactEffect;
    public void GotHit()
    {
        foreach (Human human in humans)
        {
            human.GetAngry();
        }

        house.SetActive(false);
        destroyedHouse.SetActive(true);
        GameObject effectInst = Instantiate(impactEffect, house.transform.position, transform.rotation,transform);
        AudioManager.PlayExplosionAudio();
        Destroy(effectInst, 2f);
     
    }

}
