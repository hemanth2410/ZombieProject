using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Image))]
public class ShopTabButton : MonoBehaviour,IPointerClickHandler
{
    
    [HideInInspector]public Image img;
    [SerializeField]ShopManager shopManager;
    [SerializeField]public ItemType itemType;
    
    
    private void Awake()
    {
        img = GetComponent<Image>();
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      //  img.GetComponent<RectTransform>().sizeDelta = new Vector2(260, 175);
        shopManager.OnTabSelected(itemType);
    }
}
