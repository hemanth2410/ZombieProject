using UnityEngine;
using System.Collections;
using DG.Tweening;
public class Set01 : MonoBehaviour
{
    [SerializeField] private Transform[] treePoints;

    GameManager gmInst;
    LevelManager lmInst;
    float[] scales = new float[5] { 0.8f, 0.9f, 1.0f, 1.1f, 1.2f };
    float[] rots = new float[5] {0f, 45f, 90, 135f, 180f };
    void Awake()
    {
       
        gmInst = GameManager.current;
        lmInst = LevelManager.current;
        //foreach (Transform treePoint in treePoints)
        //{
        //    int randNum = Random.Range(0, rots.Length);
        //    float randRot = rots[randNum];
        //    Instantiate(gmInst.GetRandomUnlockedTree(), treePoint.position, Quaternion.Euler(0,randRot,0), treePoint);
            
        //}
  
    }
    private void Start()
    {
  
        //Start won't call if we instantiate and setactive false immediately,
    }
   
    private void OnEnable()
    {
        StartCoroutine(InactiveGO());
       
        Grow();
        
    }

    private void OnDisable()
    {
        foreach (Transform treePoint in treePoints)
        {
            if(treePoint.GetChild(0) != null)
            {
                Destroy(treePoint.GetChild(0).gameObject);
            }

           
        }


    }
    public void Grow()
    {
        foreach (Transform treePoint in treePoints)
        {
            treePoint.localScale = Vector3.zero;

        }
        foreach (Transform treePoint in treePoints)
        {
            int randNum = Random.Range(0, scales.Length);
            float randScale = scales[randNum];
            int randNums = Random.Range(0, rots.Length);
            float randRot = rots[randNums];
            Instantiate(gmInst.GetRandomUnlockedTree(), treePoint.position, Quaternion.Euler(0, randRot, 0), treePoint);
            treePoint.DOScale(randScale, 0.5f).SetEase(Ease.OutBack);
            Debug.Log("call");
        }
       
        
    }

    IEnumerator InactiveGO()
    {
        yield return new WaitForSeconds(1.5f);
       
        lmInst.ReturnTreeSet(this.gameObject);
        gameObject.SetActive(false);
    }


}
