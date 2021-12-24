using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using TMPro;

public class StoryUI : MonoBehaviour
{
    [SerializeField] Image img;
    [SerializeField] Sprite[] images;
    [SerializeField] Dialogues[] dialogues;
    private int nextScreen = 0;
    private int nextDialogue = 0;
    SceneFader faderInst;
    [SerializeField] DialoguePopUp DialoguePopUp;
    DialoguePopUp _dialoguePopUp;
    bool canTapForScreen;
    bool canTapForDialogueChange;
    AudioSource dialogueTypeSource;

   

    private void Awake()
    {
        faderInst = SceneFader.fadeinstance;
        dialogueTypeSource = GetComponent<AudioSource>();

    }
    //private void OnEnable()
    //{
    //    TouchEvents.OnTap += StoryNext;


    //}
    //private void OnDisable()
    //{
    //    TouchEvents.OnTap -= StoryNext;
     

    //}

    void Start()
    {

        
        canTapForScreen = true;
        canTapForDialogueChange = false;
        NextStory();       
        // StoryNext(Vector2.zero);
    }
    //-___________________FOR TAP_________________//
    //public void StoryNext(Vector2 pos)
    //{
        
    //    if (canTapForScreen)
    //    {
    //        canTapForScreen = false;
    //        StartCoroutine(NextImage(pos));
    //    }

    //    if (canTapForDialogueChange)
    //    {
    //        canTapForDialogueChange = false;
    //        StartCoroutine(TypeSentence(dialogues[nextScreen].sentences[nextDialogue], _dialoguePopUp.dialogueText));
    //    }
 

    //}
    // ___________________FOR BUTTON __________________//
    public void NextStory()
    {
        if (canTapForScreen)
        {
            canTapForScreen = false;
            //StartCoroutine(NextImage(Vector2.zero));
            StartCoroutine(NextImage());
        }

        if (canTapForDialogueChange)
        {
            canTapForDialogueChange = false;
            StartCoroutine(TypeSentence(dialogues[nextScreen].sentences[nextDialogue], _dialoguePopUp.dialogueText));
        }

    }

   // IEnumerator NextImage(Vector2 pos)
    IEnumerator NextImage()
    {

        
        if (nextScreen < images.Length)
        {
            if(_dialoguePopUp != null)
            {
                _dialoguePopUp.transform.DOScale(0, 0.5f).SetEase(Ease.InOutQuart);
          
            }
     

            if(nextScreen != 0)
            {
                faderInst.FadeOut();
                yield return new WaitForSeconds(1f);
                img.sprite = images[nextScreen];
               
                faderInst.FadeIn();
                
                yield return new WaitForSeconds(1f);  // 1 sec is fadein animation time
                
            }
            else
            {
                faderInst.FadeIn();
                img.sprite = images[nextScreen];
              
            }
               
                //yield return new WaitForSeconds(1f);  // 1 sec is fadein animation time
            
            yield return new WaitForSeconds(0.2f);

            _dialoguePopUp = Instantiate(DialoguePopUp, this.transform);
            _dialoguePopUp.SetDialogue(dialogues[nextScreen]);
            _dialoguePopUp.transform.DOScale(1, 0.5f).SetEase(Ease.InOutQuart);
            yield return new WaitForSeconds(0.3f);
            if(nextDialogue == 0)
            {
                StartCoroutine(TypeSentence(dialogues[nextScreen].sentences[nextDialogue], _dialoguePopUp.dialogueText));
            }
            else
            {
                canTapForDialogueChange = true;
            }
        
                
         
           
          

            yield break;
        }
        if (nextScreen >= images.Length)
        {
            faderInst.FadeIn();
            GameInitializer.storyComplete = true;
            PlayerPrefs.SetInt("storyComplete", GameInitializer.storyComplete ? 1 : 0);
            yield return new WaitForSeconds(1f);

            GameInitializer.instance.LoadMenu();

        }

    }


    IEnumerator TypeSentence(string sentence,TextMeshProUGUI dialogueText)
    {
        
        if (nextDialogue < dialogues[nextScreen].sentences.Length)
        {
            int index = 0;

            dialogueText.text = "";
            char[] allChar = sentence.ToCharArray();
           
            foreach (char letter in allChar)
            {

                dialogueText.text += letter;
                dialogueTypeSource.Play();
                if (index == allChar.Length - 1)
                {
                    nextDialogue++;
                   
                }
                index++;
              
                yield return new WaitForSeconds(0.08f);
            }


            if (nextDialogue >= dialogues[nextScreen].sentences.Length)
            {
                nextDialogue = 0;
                nextScreen++;
                canTapForDialogueChange = false;
                canTapForScreen = true;
            }
            else
            {
                canTapForDialogueChange = true;
            }
          


        }
    }
       


}