using UnityEngine;
using DG.Tweening;

public class TreeAnim : MonoBehaviour
{

    void Start()
    {

        transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);

    }

 
}
