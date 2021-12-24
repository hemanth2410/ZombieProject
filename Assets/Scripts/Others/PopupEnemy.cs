using UnityEngine;
using DG.Tweening;
public class PopupEnemy : Enemy
{
    [SerializeField] Ease showEase = Ease.OutBack;
    [SerializeField] Transform popTransform;
    private void Start()
    {
        popTransform.localScale = Vector3.zero;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.GetComponent<Ufo1>() != null)
        {
            AudioManager.PlayBuildingPopup();
          
            popTransform.DOScale(1, 0.5f).SetEase(showEase);
            popTransform.DOPunchPosition(new Vector3(0, 1, 0), 0.5f, 10, 1, false).SetEase(Ease.OutQuart);
            
        }
    }
}
