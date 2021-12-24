using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gift : MonoBehaviour
{
    [SerializeField] GameObject giftEffect;
    [SerializeField] GameObject giftCoinPopup;
    [SerializeField] TextMeshProUGUI giftAmountText;
    [SerializeField] int[] giftCoinAmount;
    GameManager gmInst;
    Animator anim;
    int enableParamID;
    Color color;
    private void Start()
    {
        gmInst = GameManager.current;
        anim = GetComponent<Animator>();
        color = GetComponent<Image>().color;
        enableParamID = Animator.StringToHash("enable");
        UpdateGiftButtonStatus();
    }

    private void UpdateGiftButtonStatus()
    {
  
        giftAmountText.text = GameManager.numberOfGift.ToString();
        if (GameManager.numberOfGift > 0)
        {

            EnableGiftButton();
        }
        else
        {
            DisableGiftButton();
        }
    }

    public void UnlockGift()    // On gift button click
    {
        gmInst.DecrementGift();

        UpdateGiftButtonStatus();
        int rand = Random.Range(0, giftCoinAmount.Length);
        GameManager.current.AddCoins(giftCoinAmount[rand]);
        GameObject _popUp = Instantiate(giftCoinPopup, this.transform);
        _popUp.GetComponent<GoldPopup>().UpdateText(giftCoinAmount[rand]);
        AudioManager.PlayCoinCollectAudio();
        GameEvents.current.SaveGame();

    }

    private void EnableGiftButton()
    {
         
        anim.SetBool(enableParamID, true);
        giftEffect.SetActive(true);
        color = Color.white;
        color.a = 0.9f;
        this.GetComponent<Button>().interactable = true;
    }
    private void DisableGiftButton()
    {

        anim.SetBool(enableParamID, false);
        giftEffect.SetActive(false);
        color = Color.black;
        color.a = 0.7f;
        this.GetComponent<Button>().interactable = false;
    }
}
