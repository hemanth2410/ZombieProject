using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class CoinPopUp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] Image coinImage;
    [SerializeField] RectTransform coinPanelTransform;

    public void PopUp(int amount)
    {
        coinText.text = "+ " + amount.ToString();
        coinPanelTransform.DOAnchorPos(new Vector2(5, 7), 2f).SetEase(Ease.OutBack);
        coinText.DOFade(0, 1f);
        coinImage.DOFade(0, 1f);
        

    }



}
