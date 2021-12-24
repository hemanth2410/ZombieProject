using UnityEngine;

[CreateAssetMenu(fileName = "New ItemType", menuName = "ScriptableObject/ItemType")]
public class ItemType : ScriptableObject
{

    [Tooltip("The Item's Type")]
    [SerializeField]private CATEGORY _catergory;

    public CATEGORY Category => _catergory;

    
}

