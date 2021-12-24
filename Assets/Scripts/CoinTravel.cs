using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinTravel : MonoBehaviour
{
    [SerializeField]Image coinPf;

  
    public void CoinCollectEffect(RectTransform coinImage, Vector3 destPos)
    {
        StartCoroutine(SpawnCoins(coinImage,destPos));
    }

    IEnumerator SpawnCoins(RectTransform coinImage,Vector3 destPos)
    {
     
        Debug.Log(coinImage.position);
        for (int i = 0; i < 10; i++)
        {
            Image img = Instantiate(coinPf, coinImage.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))),transform);
          
            
          //  img.transform.position = Vector3.Lerp(img.transform.position, Random.insideUnitCircle * 40, 0.4f);
            img.rectTransform.DOAnchorPos(Random.insideUnitCircle * 300, 0.5f, false).SetEase(Ease.InOutCirc);
        }
        yield return new WaitForSeconds(0.8f);
        MoveCoin(destPos);
    }
    private void MoveCoin(Vector3 pos)
    {
       
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childT = transform.GetChild(i);
            childT.DOMove(pos, 0.7f, false).SetEase(Ease.InOutQuart).OnComplete(Dest);
            childT.GetComponent<Image>().DOFade(0, 0.7f);
        }
        
    }

    private void Dest()
    {
       
        Destroy(this.gameObject);
    }

}
