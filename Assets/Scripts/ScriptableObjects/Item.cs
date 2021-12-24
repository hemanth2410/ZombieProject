
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/Item")]

public class Item : ScriptableObject
{
	[Tooltip("The type of item")]
	[SerializeField]
	private string _itemName;
	[SerializeField]
	private int _indexPosition;
	[Tooltip("The type of item")]
	[SerializeField]
	private ItemType _itemType;

	[Tooltip("The Item's Prefab")]
	[SerializeField] private GameObject _itemPrefab;
	[SerializeField] private GameObject _mainMenuPrefab;
	[Tooltip("The Item's UI Icon")]
	[SerializeField] private Sprite _unlockedIcon;
	[SerializeField] private Sprite _lockedIcon;


	public ItemType ItemType => _itemType;
	public GameObject ItemPrefab => _itemPrefab;
	public GameObject MainMenuPrefab => _mainMenuPrefab;

	public string ItemName => _itemName;
	public Sprite UnlockedIcon => _unlockedIcon;
	public Sprite LockedIcon => _lockedIcon;

	public int IndexPosition => _indexPosition;
	



   
}
