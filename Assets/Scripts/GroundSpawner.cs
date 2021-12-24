using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] GameObject unitGroundPrefab;
    [SerializeField]public int numberOfUnit;
   // [SerializeField] int unitLength = 1;
    private void OnValidate()
    {
        //foreach (Transform child in transform)
        //{
        //    UnityEditor.EditorApplication.delayCall += () =>
        //    {

        //        DestroyImmediate(child.gameObject);
        //    };
        //}

        //for (int i = 0; i < numberOfUnit; i++)
        //{
        //    Spawnblock(new Vector3(transform.position.x + (unitLength * i), 0, 0));
        //}

    }

    private void Spawnblock(Vector3 spawnPosition)
    {
        Instantiate(unitGroundPrefab, spawnPosition, Quaternion.identity, this.transform);
    }
    //private void DeleteBlock()
    //{
    //    Destroy(this.transform.GetChild(this.transform.childCount - 1));
    //}
    


}
