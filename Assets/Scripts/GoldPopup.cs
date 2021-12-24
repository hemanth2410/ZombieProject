using UnityEngine;
using TMPro;

public class GoldPopup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
   public void UpdateText(int coinAmount)
   {
        coinText.text = "+ " + coinAmount.ToString();
   }
}
