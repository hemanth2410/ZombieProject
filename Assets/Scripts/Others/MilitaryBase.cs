using UnityEngine;
using System.Collections;

public class MilitaryBase : MonoBehaviour
{
    [SerializeField] private GameObject missile;
    [SerializeField] private Transform firePoint;

    public int xScale;
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
}
