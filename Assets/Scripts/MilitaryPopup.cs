using UnityEngine;
using System.Collections;
using DG.Tweening;
public class MilitaryPopup : MonoBehaviour
{
    [SerializeField] private GameObject missile;
    [SerializeField] private Transform firePoint;

    
    public int xScale;
    [SerializeField] Ease showEase = Ease.OutBack;
    [SerializeField] Transform popTransform;
    private void Start()
    {
        popTransform.localScale = Vector3.zero;
    }
    public void Shoot(Transform player)
    {
        StartCoroutine(ShootDelay(player));
    }
    IEnumerator ShootDelay(Transform player)
    {
        AudioManager.PlayMissileLaunchAudio();
        yield return new WaitForSeconds(0.5f);
        GameObject missile_ = Instantiate(missile, firePoint.position, firePoint.rotation, this.transform.parent);
       
        Missile missileScript_ = missile_.GetComponent<Missile>();

        if (missileScript_ != null)
            missileScript_.Seek(player);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Ufo1>() != null)
        {
            AudioManager.PlayBuildingPopup();

            popTransform.DOScale(1, 0.5f).SetEase(showEase);
            popTransform.DOPunchPosition(new Vector3(0, 1, 0), 0.5f, 10, 1, false).SetEase(Ease.OutQuart);

        }
    }
}
