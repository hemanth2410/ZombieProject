using UnityEngine;
using System.Collections;
public class PlatformMovement : MonoBehaviour
{

    private float moveSpeed;
    private float maxMoveSpeed;
    private float minMoveSpeed;
    Vector2 dir;
    float time1;
    float distanceCovered;
    [SerializeField] float acceleration;
    bool canMove = false;


    private void OnEnable()
    {
        GameEvents.current.OnLevelEndTrigger += StopMovement;
        GameEvents.current.OnLevelLoose += DestroySelf; 
     
    }

    private void OnDisable()
    {
        GameEvents.current.OnLevelEndTrigger -= StopMovement;
        GameEvents.current.OnLevelLoose -= DestroySelf;


    }
    void Start()
    {
       
        dir = Vector2.left;
        moveSpeed = 0f; 

        if(GameManager.currentLevel <= 31)
        {
            minMoveSpeed = 5;
            maxMoveSpeed = 10;
        }
        else
        {
            minMoveSpeed = 6;
            maxMoveSpeed = 13;
        }
     
    }

    public void StopMovement()
    {
      
        StartCoroutine(SlowMove());
    }
    public void StopMovement(string a)
    {
        canMove = false;
    }

    IEnumerator SlowMove()
    {
        yield return new WaitForSeconds(2f);
        moveSpeed = 5f;

        yield return new WaitForSeconds(1f);

        canMove = false;
    }
    
    public void StartLevelMovement()
    {

        moveSpeed = minMoveSpeed;
        canMove = true;


    }

    void FixedUpdate()
    {
        if (canMove)
        {
            time1 += Time.fixedDeltaTime;

            moveSpeed += acceleration * Time.fixedDeltaTime;

            transform.Translate(dir * moveSpeed * Time.fixedDeltaTime);

            if (moveSpeed >= maxMoveSpeed)
            {
                moveSpeed = maxMoveSpeed;
            }
        }
     
      
    }

    public float DistanceCovered()
    {

        distanceCovered = (maxMoveSpeed + minMoveSpeed) / 2 * time1;
        return distanceCovered;
    }

    public void DestroySelf(int a,string s)
    {
        Destroy(this.gameObject, 5f);
    }


}
