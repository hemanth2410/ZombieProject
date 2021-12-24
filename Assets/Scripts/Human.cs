using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Human : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Image emoji;
    [SerializeField] Animator Jumpanim;
    [SerializeField] Sprite angryEmoji;
    bool happy;
    [SerializeField] Transform GFX;
    
    private void Start()
    {
        happy = true; 
    }
    public void GetAngry()
    {
        happy = false;
        anim.SetBool("angry",true);
        emoji.sprite = angryEmoji;
        emoji.rectTransform.DOShakePosition(1f, 0.5f, 30, 10, false, true);
        Jumpanim.SetBool("Jump", false);
    }

}
