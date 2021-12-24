using DG.Tweening;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class CoinPanel : MonoBehaviour
{
    [SerializeField] RectTransform coinImage;
	[SerializeField] TextMeshProUGUI coinText1;
	[SerializeField] TextMeshProUGUI coinText2;
	[SerializeField] private Button nextButton;
	[SerializeField] private Button homeButton;
	[SerializeField] TextMeshProUGUI shopCoinText;
	[SerializeField] CoinTravel coinTravel;
	[SerializeField] RectTransform coinEndPos;

    private void OnEnable()
    {
		GameEvents.current.OnCoinChanged += UpdateCoin;
    }
    private void OnDisable()
    {
		GameEvents.current.OnCoinChanged -= UpdateCoin;
	}
    public void ShowCoinWinScreen(int coinGained, int extraCoin)
	{
		nextButton.interactable = false;
		homeButton.interactable = false;
		StartCoroutine(CoinEffect(coinGained, extraCoin));
	}
	IEnumerator CoinEffect(int coinGained, int extraCoin)
	{
		coinImage.DOScale(1f, 0.4f).SetEase(Ease.OutBounce);
		yield return new WaitForSeconds(0.5f);
		//coinText.text = coinGained.ToString() + "+" + extraCoin.ToString();
		StartCoroutine(UpdateCounter(coinGained, coinText1));
		AudioManager.PlayCoinCollectAudio();
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(UpdateCounter(extraCoin, coinText2));
		CoinTravel _coinTravel = Instantiate(coinTravel, coinImage.position, Quaternion.identity,this.transform);
		_coinTravel.CoinCollectEffect(coinImage,coinEndPos.position);
		yield return new WaitForSeconds(1f);  //coinpanel do move duration = 1s so wait for 1s
											  //coinTravel.transform.DOMove(coinEndPos.position, 1f, false).SetEase(Ease.OutBack);
											  // coin fly animation
											  // show total coin after level coin and extra coin addition here
		UpdateCoin(GameManager.totalCoins);
		
		nextButton.interactable = true;
		homeButton.interactable = true;

	}

	public void UpdateCoin(int amount)
    {
		StartCoroutine(UpdateCoinsEffect(amount));
	}
	IEnumerator UpdateCoinsEffect(int amount)
	{
		shopCoinText.text = amount.ToString();
		shopCoinText.rectTransform.DOScale(1.25f, 0.3f).SetEase(Ease.OutBack);
		yield return new WaitForSeconds(0.5f);
		shopCoinText.rectTransform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
	}

	private IEnumerator UpdateCounter(int coins,TextMeshProUGUI text)
	{
		int value = 0;
	
		while (value < coins)
        {
			value++;

			text.text = value.ToString();

			yield return new WaitForSeconds(0.02f);//Wait before next counter change
		}
	    
			
		
	}
}
