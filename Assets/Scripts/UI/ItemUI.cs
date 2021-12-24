using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class ItemUI : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector]
    public Item item;
    [HideInInspector]
    public bool unlocked;
    [SerializeField] Image icon;
    [SerializeField] GameObject highlighter;
    [SerializeField] UIItemUnlock unlockItemUI;
    Transform parentPanel;
    ShopManager shopManager;
    Image bg;
    [SerializeField]Color unlockBGColor;
    [SerializeField]Color lockBGColor;
  
    private void Awake()
    {
      
        shopManager = ShopManager.current;
        bg = GetComponent<Image>();
       
       

    }
    private void Start()
    {
        parentPanel = this.transform.parent.parent.parent.parent.parent.parent.parent;

        if (unlocked == false)
        {
            icon.sprite = item.LockedIcon;
           
            bg.color = lockBGColor;
            if (item.ItemType.Category == CATEGORY.BEAM)
            {
                icon.transform.localScale = Vector3.one * 0.5f;
            }

        }
        else
        {
           
            icon.sprite = item.UnlockedIcon;
          
            bg.color = unlockBGColor;
            
        }
    }

    public void Unlocked()
    {
      
      unlocked = true;
        icon.transform.localScale = Vector3.one;
        icon.sprite = item.UnlockedIcon;
        bg.color = unlockBGColor;


    }

    public void UnlockPopUp()
    {
        UIItemUnlock _unlockedItemUI = Instantiate(unlockItemUI, parentPanel);
        _unlockedItemUI.UnlockEffect(item);
    }
  
    public void Selected()
    {
        highlighter.SetActive(true);
    }
    public void Deselect()
    {
        highlighter.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!unlocked)
        {
            return;
        }
        shopManager.OnItemClick(item);
       
    }
}
