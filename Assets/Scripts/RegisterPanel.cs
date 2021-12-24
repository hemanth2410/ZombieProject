using UnityEngine;
using TMPro;

public class RegisterPanel : MonoBehaviour
{
    [SerializeField] TMP_InputField nameField;
    public static string playerName;
    [HideInInspector]public bool canAccept; 
    public void RegisterName()
    {
        if(nameField.text == "")
        {
            Debug.Log("isnull");
            canAccept = false;
            return;
        }
        else
        {
            canAccept = true;
            Debug.Log("registered");
            playerName = nameField.text;
            GameManager.registered = true;
            GameEvents.current.NameChange(playerName);
            GameEvents.current.SaveGame();
            
        }
       
    }
}
