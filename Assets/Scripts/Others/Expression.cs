using UnityEngine;

public class Expression : MonoBehaviour
{
   
    [SerializeField] private GameObject happyEye;
    [SerializeField] private GameObject sadEye;
   
    [SerializeField] private GameObject happyMouth;
    [SerializeField] private GameObject sadMouth;
 

    private int sadParamID;
    private int happyParamID;

    Expressions expression;
    Animator anim;
    public enum Expressions
    {
        
        Happy,
        Sad
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();

        sadParamID = Animator.StringToHash("Sad");
        happyParamID = Animator.StringToHash("Happy");
    }

    private void Start()
    {
    
        expression = Expressions.Happy;
        ChangeExpression(expression);

    }

    public void ChangeExpression(Expressions exp)
    {
 

        switch (exp)
        {
           
            case Expressions.Happy:


            
                happyEye.SetActive(true);
                happyMouth.SetActive(true);
                sadEye.SetActive(false);
                sadMouth.SetActive(false);
                anim.SetBool(happyParamID, true);
                anim.SetBool(sadParamID, false);
                break;

            case Expressions.Sad:

                happyEye.SetActive(false);
                happyMouth.SetActive(false);
                sadEye.SetActive(true);
                sadMouth.SetActive(true);
                anim.SetBool(happyParamID, false);
                anim.SetBool(sadParamID, true);
                break;
        }
    }

    public void SadExp()
    {
        expression = Expressions.Sad;
        ChangeExpression(expression);
    }
    public void HappyExp()
    {
        expression = Expressions.Happy;
        ChangeExpression(expression);
    }
 

}
