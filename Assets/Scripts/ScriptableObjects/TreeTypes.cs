using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Tree" ,menuName = "ScriptableObject/TreeTypes")]
public class TreeTypes : ScriptableObject
{
    public string treeName;
    public GameObject treePrefab;
    public Sprite emptyTree;
    public Sprite fullTree;
    public int starsRequiredToUnlock;
    
}
