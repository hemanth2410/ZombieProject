using UnityEngine;

public class ChangeTint : MonoBehaviour
{
    Material mat;

    private void Awake()
    {
        mat = GetComponent<ParticleSystemRenderer>().material;  
    }

    private void OnEnable()
    {
        GameEvents.current.OnBGChange += UpdateTint;
    }

    private void OnDisable()
    {
        GameEvents.current.OnBGChange -= UpdateTint;
    }

    public void UpdateTint(Color tint,Color cloudTint)
    {
        mat.SetColor("_color", cloudTint);
       
    }
}
