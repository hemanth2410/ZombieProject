using UnityEngine;
using TMPro;
using System.Collections;
public class DialoguePopUp : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
   
    public void SetDialogue(Dialogues dialogue)
    {

      
        this.gameObject.GetComponent<RectTransform>().localPosition = dialogue.dialoguePos;
        this.gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.2f, 0.02f);

    }
    
   
}


