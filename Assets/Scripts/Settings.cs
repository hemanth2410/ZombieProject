using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    //[SerializeField] string _volumeParameter = "MasterVolume";
   // [SerializeField] AudioMixer _mixer;
    [SerializeField] Toggle _soundToggle;
    [SerializeField] Toggle _vibrateToggle;


    void Awake()
    {
        _soundToggle.onValueChanged.AddListener(AudioManager.current.SwitchSound);
        _vibrateToggle.onValueChanged.AddListener(SwitchVibrate);
    }

    private void OnEnable()
    {
        

        if (AudioManager.masterValue == 0)
        {
          
            _soundToggle.isOn = true;
        }
        else
        {
          
            _soundToggle.isOn = false;
        }
        if (GameManager.enableVibrate)
        {
           
            _vibrateToggle.isOn = true;
        }
        else
        {
           
            _vibrateToggle.isOn = false;
        }
      
    }

    //void SwitchSound(bool enableSound)
    //{
      
    //    if (enableSound)
    //    {
           
    //        AudioManager.masterValue = 0;
         
    //    }
    //    else
    //    {
        
    //       AudioManager.masterValue = -80;
            
    //    }

    //    AudioManager.current.masterMixer.SetFloat("MasterVolume", AudioManager.masterValue);
    //    PlayerPrefs.SetInt("MasterVolume", AudioManager.masterValue);
       
    //}

    public void SwitchVibrate(bool enableVibrate)
    {
        if (enableVibrate)
        {
            GameManager.enableVibrate = true;
        
        }
        else
        {
            GameManager.enableVibrate = false;
          
        }
        PlayerPrefs.SetInt("vibration", GameManager.enableVibrate ? 1 : 0);
        Debug.Log("enable vibration" + GameManager.enableVibrate);
    }
    public void RateUs()
    {
        Application.OpenURL("market://details?id=" + Application.identifier);
    }
}
