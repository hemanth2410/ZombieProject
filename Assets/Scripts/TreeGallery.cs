using System.Collections.Generic;
using UnityEngine;

public class TreeGallery : MonoBehaviour
{
    [SerializeField] GameObject treeUIPrefab;
    [SerializeField] Transform treeContainer;
    [SerializeField] GameObject indicator;
    [SerializeField] Transform indicatorContainer;
    [SerializeField] ScrollSnapRect scrollManager;
    int currentItemNumber;
    private void OnEnable()
    {
       
        scrollManager.Initalize(currentItemNumber);
    }

    public void CreateTreeUI(List<TreeTypes> treeList, List<TreeTypes> unlockedTreeList)
    {

        for (int i = 0; i < treeList.Count; i++)
        {
            GameObject obj = Instantiate(treeUIPrefab, treeContainer);
            TreeUI _tree = obj.GetComponent<TreeUI>();
            _tree.tree = treeList[i];

            TREESTATUS status;

            if (i < unlockedTreeList.Count)
            {
              
                status = TREESTATUS.UNLOCKED;     // unlocked

            }
            else if( i == unlockedTreeList.Count)
            {
              
                status = TREESTATUS.CURRENT;     //current
                Debug.Log("CURRENT" + i);
                currentItemNumber = i;
            }
            else
            {
               
                status = TREESTATUS.LOCKED;   //locked

            }
            _tree.Initialize(status);
         
            Instantiate(indicator, indicatorContainer);


        }
    }
}
