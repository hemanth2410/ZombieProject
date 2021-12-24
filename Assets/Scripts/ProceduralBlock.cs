
using UnityEngine;

public abstract class ProceduralBlock : MonoBehaviour
{

    [HideInInspector]public int numberOfUnit;
    
    [SerializeField] protected GameObject unitGroundPrefab;
    [SerializeField] protected GameObject unitCoinGroundPrefab;
   // [SerializeField] protected GameObject unitDeathGroundPrefab;
    public virtual void Spawnblock(Vector3 spawnPosition,GameObject groundTypePrefab)
    {
        Instantiate(groundTypePrefab, spawnPosition, Quaternion.identity, this.transform);
    }
}
