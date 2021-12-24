using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnitA : MonoBehaviour
{
    //private Vector2 dimension;
    //[SerializeField] private TreeTypes[] trees; 
    //[SerializeField] private BushTypes[] bushes;
    //[SerializeField] private Vector2 treeXMinYMax;
    //[SerializeField] private Vector2 bushXMinYMax;

    public float radius = 1;
    public Vector2 regionSize = Vector2.one;
    public int rejectionSamples = 30;

    List<Vector2> points;
    public float displayRadius = 1;


    private void OnValidate()
    { 
        points = TreeSampler.GeneratePoints(radius, regionSize, rejectionSamples);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(regionSize / 2, regionSize);
        if(points != null)
        {
            foreach(Vector2 point in points)
            {
                Gizmos.DrawSphere(point, displayRadius);
            }
        }
    }
}
