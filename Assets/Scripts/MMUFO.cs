using UnityEngine;

public class MMUFO : MonoBehaviour
{
    Vector3 rotDir;
    
    private void Start()
    {
        rotDir = new Vector3(0, 1, 0);
    }
    void Update()
    {
     
        transform.Rotate(rotDir * 30f * Time.deltaTime);
    }
}
