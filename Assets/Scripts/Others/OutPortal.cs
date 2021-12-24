using UnityEngine;
using DG.Tweening;
public class OutPortal : MonoBehaviour
{
    [SerializeField] Transform portal;
    private void OnEnable()
    {
        GameEvents.current.OnLevelWin += ScaleUpPortal;
        GameEvents.current.OnLevelLoose += ScaleUpPortal;
    }
    private void OnDisable()
    {
        GameEvents.current.OnLevelWin -= ScaleUpPortal;
        GameEvents.current.OnLevelLoose -= ScaleUpPortal;
    }

    private void ScaleUpPortal(int a,string msg)
    {

        portal.DOScale(10f, 8f).SetEase(Ease.OutBack);
    }

    private void ScaleUpPortal(int arg1, int arg2, TreeTypes arg3, int arg4,int arg5,int arg6)
    {
        portal.DOScale(10f, 10f).SetEase(Ease.OutBack);
    }

   
}
