
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Transform target;
    [SerializeField] float speed;
    [SerializeField] GameObject impactEffect;
    [SerializeField] string looseMessage = "Watch out for military base!";
    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= 20*distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject effectInst = Instantiate(impactEffect, transform.position, transform.rotation);
        AudioManager.PlayExplosionAudio();
        GameEvents.current.MissileHit();
        Destroy(effectInst, 2f);
        DestroyPlayer(target);
        Destroy(gameObject);
        
    }
    void DestroyPlayer(Transform player)
    {
        Ufo1 _player = player.GetComponent<Ufo1>();

        if (_player != null)
        {
            _player.Dead(looseMessage);
        }

    }
}
