using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TreeUI : MonoBehaviour
{ 
    
    [HideInInspector]public TreeTypes tree;
    [SerializeField]private Image lockedImg;
    [SerializeField]private Image unlockedImg;
    [SerializeField] private TextMeshProUGUI stars;

    public void Initialize(TREESTATUS status)
    {
        lockedImg.sprite = tree.emptyTree;
        unlockedImg.sprite = tree.fullTree;
    
        switch (status)
        {
            case TREESTATUS.UNLOCKED : unlockedImg.fillAmount = 1f;
                stars.text = tree.starsRequiredToUnlock + " / " + tree.starsRequiredToUnlock;
                break;
            case TREESTATUS.CURRENT: unlockedImg.fillAmount = (float)LevelManager.totalStars / tree.starsRequiredToUnlock;
                stars.text = LevelManager.totalStars + " / " + tree.starsRequiredToUnlock;

                break;
            case TREESTATUS.LOCKED: unlockedImg.fillAmount = 0f;
                stars.text = "0" + " / " + tree.starsRequiredToUnlock;
                break;

                
        }

    }
}
