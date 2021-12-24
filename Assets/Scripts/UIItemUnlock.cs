using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
public class UIItemUnlock : MonoBehaviour
{
    [SerializeField]private Image unlockedIcon;
 
    [SerializeField]GameObject closePopupbutton;
    public void UnlockEffect(Item item)
    {
        StartCoroutine(UnlockItem(item));
    }
     IEnumerator UnlockItem(Item item)
    {
        unlockedIcon.sprite = item.UnlockedIcon;
        unlockedIcon.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
       
        
        yield return new WaitForSeconds(0.3f);

        closePopupbutton.SetActive(true);
        closePopupbutton.GetComponent<RectTransform>().DOScale(1f, 0.3f).SetEase(Ease.OutBack);

    }
    public void RemovePopup()
    {
        unlockedIcon.transform.DOScale(0f, 0.2f).SetEase(Ease.OutBack);
   
        Destroy(this.gameObject);
    }



}
