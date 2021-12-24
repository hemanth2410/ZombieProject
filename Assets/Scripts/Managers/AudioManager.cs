
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

	public static AudioManager current;

	[Header("BG")]
	public AudioClip menuBG;       
	public AudioClip gameBG;        

	[Header("sfx")]
	public AudioClip buildingPop;    
	public AudioClip buttonPress;         
	public AudioClip coinCollect;      
	public AudioClip charJump; 
	public AudioClip explosion; 
	public AudioClip missileLaunch; 
	public AudioClip levelStart; 
	public AudioClip levelWin; 
	public AudioClip levelLoose; 
	public AudioClip star; 
	public AudioClip stun; 
	public AudioClip shopUnlock;

    [Header("Mixer Groups")]

    public AudioMixerGroup bgGroup;
    public AudioMixerGroup uiGroup;
    public AudioMixerGroup playerGroup;
    public AudioMixerGroup effectGroup;


    AudioSource bgSource;            
	AudioSource uiSource;           
	AudioSource playerSource;           
	AudioSource effectSource;

	[SerializeField]public AudioMixer masterMixer;
	public static int masterValue;

	void Awake()
	{
		if (current != null && current != this)
		{
	
			Destroy(gameObject);
			return;
		}


		current = this;
		DontDestroyOnLoad(gameObject);


		bgSource = gameObject.AddComponent<AudioSource>() as AudioSource;
		uiSource = gameObject.AddComponent<AudioSource>() as AudioSource;
		playerSource = gameObject.AddComponent<AudioSource>() as AudioSource;
		effectSource = gameObject.AddComponent<AudioSource>() as AudioSource;

        bgSource.outputAudioMixerGroup = bgGroup;
        uiSource.outputAudioMixerGroup = uiGroup;
        playerSource.outputAudioMixerGroup = playerGroup;
        effectSource.outputAudioMixerGroup = effectGroup;

    }

    public void PlayMainMenuAudio()
	{
		GameManager.current.UpdateVibration();
		UpdateSound();
		current.effectSource.volume = 1f;
		current.bgSource.clip = current.menuBG;
		current.bgSource.loop = true;
		current.bgSource.Play();

	}

	public static void PlayInGameBG()
    {
		current.effectSource.volume = 1f;
		current.bgSource.clip = current.gameBG;
		current.bgSource.loop = true;
		current.bgSource.Play();
	}
	public static void PlayExplosionAudio()
	{

		if (current == null)
			return;
		current.effectSource.volume = 0.7f;
		current.playerSource.clip = current.explosion;
		current.playerSource.Play();

	}

	public static void PlayMissileLaunchAudio()
    {
		if (current == null)
			return;
		current.effectSource.volume = 0.7f;
		current.playerSource.clip = current.missileLaunch;
		current.playerSource.Play();
	}

	public static void PlayCharJumpAudio()
	{
	
		if (current == null)
			return;
		current.effectSource.volume = 0.7f;
		current.playerSource.clip = current.charJump;
		current.playerSource.Play();

	}


	public static void PlayBuildingPopup()
	{

		if (current == null)
			return;


		current.effectSource.volume = 1f;
		current.effectSource.clip = current.buildingPop;
		current.effectSource.Play();

	}


	public void playButtonPress()
	{
	
		if (current == null)
			return;
		current.effectSource.volume = 1f;
		current.uiSource.clip = current.buttonPress;
		current.uiSource.Play();

	}

	public static void PlayCoinCollectAudio()
	{
	
		if (current == null)
			return;
		current.effectSource.volume = 1f;
		current.effectSource.clip = current.coinCollect;
		current.effectSource.Play();
	}

	public static void PlayLevelStartAudio()
	{
		if (current == null)
			return;
		current.effectSource.volume = 0.7f;

		current.effectSource.clip = current.levelStart;
		current.effectSource.Play();
	}

	public static void PlayWinAudio()
	{
		
		if (current == null)
			return;

		//current.ambientSource.Stop();
		current.effectSource.volume = 1f;
		current.effectSource.clip = current.levelWin;
		current.effectSource.Play();

	}
	public static void PlayLooseAudio()
	{

		if (current == null)
			return;

		//current.ambientSource.Stop();
		current.effectSource.volume = 1f;
		current.effectSource.clip = current.levelLoose;
		current.effectSource.Play();


	}


	public static void PlayUnlockAudio()
	{

		if (current == null)
			return;

		//current.ambientSource.Stop();
		current.effectSource.volume = 1f;
		current.effectSource.clip = current.shopUnlock;
		current.effectSource.Play();


	}
	public static void PlayStunAudio()
	{

		if (current == null)
			return;

		//current.ambientSource.Stop();
		current.effectSource.volume = 1f;
		current.effectSource.clip = current.stun;
		current.effectSource.Play();


	}
	public static void PlayStarAudio()
	{

		if (current == null)
			return;

		//current.ambientSource.Stop();
		current.effectSource.volume = 0.7f;
		current.effectSource.clip = current.star;
		current.effectSource.Play();


	}
	public void UpdateSound()
	{
		masterValue = PlayerPrefs.GetInt("MasterVolume", 0);

		masterMixer.SetFloat("MasterVolume", masterValue);


	}

	public void Mute()
	{
		masterMixer.SetFloat("MasterVolume", -80);
	}

	public void Unmute()
	{
		masterMixer.SetFloat("MasterVolume", 0);
	}
	public void SwitchSound(bool enableSound)
	{

		if (enableSound)
		{

			masterValue = 0;

		}
		else
		{

			masterValue = -80;

		}

		masterMixer.SetFloat("MasterVolume", masterValue);
		PlayerPrefs.SetInt("MasterVolume", masterValue);

	}


}

