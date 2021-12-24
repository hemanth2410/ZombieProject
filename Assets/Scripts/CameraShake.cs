using UnityEngine;
using DG.Tweening;
using System.Collections;
public class CameraShake : MonoBehaviour
{
    float handheldDuration = 1.5f;
    [SerializeField]Vector3 zoomPos;
    [SerializeField]Vector3 playPos;


    private void OnEnable()
    {
        GameEvents.current.OnLevelEndTrigger += ZoomIn;
        GameEvents.current.OnMissileHit += HitShake;
        GameEvents.current.OnHouseHit += HitShake;
    }

    private void OnDisable()
    {
        GameEvents.current.OnLevelEndTrigger -= ZoomIn;
        GameEvents.current.OnMissileHit -= HitShake;
        GameEvents.current.OnHouseHit -= HitShake;
    }
    private void Start()
    {
        transform.localPosition = zoomPos;

        StartCoroutine(Intro());
        
    }

    private  void Shake() 
    {
        transform.DOShakePosition(handheldDuration, 0.5f, 1, 100, false, true).SetEase(Ease.OutBack);   // match duration with levelmanager  Intro Ienumerator
    }

    private void HitShake()
    {
        Debug.Log("SHAKEE??");
        transform.DOShakePosition(0.5f, 2f, 50, 100, false, true).SetEase(Ease.OutBack);   // shake when hit by missile
    }

    IEnumerator Intro()
    {
        Shake();

        yield return new WaitForSeconds(handheldDuration);

        ZoomOut();
    }

    private void ZoomOut()
    {
        transform.DOLocalMove(playPos, 0.5f).SetEase(Ease.OutBack);
    }
    public void ZoomIn()
    {
        transform.DOLocalMove(zoomPos, 0.5f).SetEase(Ease.OutBack);
    }

}
