
using UnityEngine;

public class AvatarEffect : MonoBehaviour
{
    Ufo1 parentScript;
 
    void Start()
    {
        parentScript = transform.parent.parent.GetComponent<Ufo1>();
    }

    public void Effect()
    {

        parentScript.FlashIntro();
    }
    public void JumpAudio()
    {
        AudioManager.PlayCharJumpAudio();
    }


}
