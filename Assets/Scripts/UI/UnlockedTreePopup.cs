using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
public class UnlockedTreePopup : MonoBehaviour
{
    
    [SerializeField]Image unlockedTreeIcon;
    UIManager uiManager;
    private void Start()
    {
        uiManager = transform.parent.parent.GetComponent<UIManager>();
    }
    public void UnlockEffect(TreeTypes unlockedTree)
    {
        unlockedTreeIcon.sprite = GameManager.current.currentTreeToUnlock.fullTree;
        transform.DOScale(1f, 0.2f);
        AudioManager.PlayUnlockAudio();

    }

    public void continueToNextTree()
    {
        uiManager.ContinueUnlockTree();
    }

    public void RemovePopup()
    {
        transform.DOScale(0f, 0.2f);
      
        Destroy(this.gameObject,0.3f);
    }

   
}
