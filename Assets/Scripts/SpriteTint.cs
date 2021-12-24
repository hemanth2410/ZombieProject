
using UnityEngine;

public class SpriteTint : MonoBehaviour
{
    Material mat;

    private void Awake()
    {
        mat = GetComponent<SpriteRenderer>().material;
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
        mat.SetColor("_color", tint);
        
    }
}
