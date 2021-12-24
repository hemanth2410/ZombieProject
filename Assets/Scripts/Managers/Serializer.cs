using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serializer : MonoBehaviour
{
    public static Serializer instance;

    private void Awake()
    {

        if (instance != null && instance != this)
        {

            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

    }

   public void SerializeAvatars(List<int> unlockedAvatars)
    {
        //serializing avatar type

        string _convertedValue = string.Empty;
        string convertedValue = string.Empty;
       
            foreach (int avatar in unlockedAvatars)
            {
                int convertedByte = avatar;
                _convertedValue += convertedByte + ",";
                convertedValue = _convertedValue.Substring(0, _convertedValue.Length - 1);
              
            }
           
          PlayerPrefs.SetString("AVATAR_TYPE", convertedValue);
          PlayerPrefs.Save();
      
    }

    public List<int> GetUnlockedAvatars()
    {
        List<int> unlockedAvatars = new List<int>();

        if (PlayerPrefs.HasKey("AVATAR_TYPE"))
        {
            string formattedString = PlayerPrefs.GetString("AVATAR_TYPE", string.Empty);
            string[] convertedInts = formattedString.Split(',');
           
            foreach (string item in convertedInts)
            {
                int result;
                
                if (int.TryParse(item, out result))
                {
                    int temp = int.Parse(item);
                    int tempType = temp;
                    unlockedAvatars.Add(tempType);
                 
                }
             

            }
          
        }
      
        
        return unlockedAvatars;

    }
    public void SerializeUFOs(List<int> unlockedUFOs)
    {
        //serializing ufo type
        string _convertedValue = string.Empty;
        string convertedValue = string.Empty;

        foreach (int UFO in unlockedUFOs)
        {
            int convertedByte = UFO;
            _convertedValue += convertedByte + ",";
            convertedValue = _convertedValue.Substring(0, _convertedValue.Length - 1);
           
        }
       
        PlayerPrefs.SetString("UFO_TYPE", convertedValue);
        PlayerPrefs.Save();
    }

    public List<int> GetUnlockedUFOs()
    {
        List<int> unlockedUFOs = new List<int>();
        if (PlayerPrefs.HasKey("UFO_TYPE"))
        {
            string formattedString = PlayerPrefs.GetString("UFO_TYPE", string.Empty);
            string[] convertedInts = formattedString.Split(',');

            foreach (string item in convertedInts)
            {
                int result;

                if (int.TryParse(item, out result))
                {
                    int temp = int.Parse(item);
                    int tempType = temp;
                    unlockedUFOs.Add(tempType);
                }
                    
            }

        }


        return unlockedUFOs;
    }
    public void SerializeBeams(List<int> unlockedBeams)
    {
        //serializing beam type
        string _convertedValue = string.Empty;
        string convertedValue = string.Empty;

        foreach (int beam in unlockedBeams)
        {
            int convertedByte = beam;
            _convertedValue += convertedByte + ",";
            convertedValue = _convertedValue.Substring(0, _convertedValue.Length - 1);
         
        }

        PlayerPrefs.SetString("BEAM_TYPE", convertedValue);
        PlayerPrefs.Save();
    }

    public List<int> GetUnlockedBeams()
    {
        List<int> unlockedBeams = new List<int>();
        if (PlayerPrefs.HasKey("BEAM_TYPE"))
        {
            string formattedString = PlayerPrefs.GetString("BEAM_TYPE", string.Empty);
            string[] convertedInts = formattedString.Split(',');

            foreach (string item in convertedInts)
            {

                int result;

                if (int.TryParse(item, out result))
                {
                    int temp = int.Parse(item);
                    int tempType = temp;
                    unlockedBeams.Add(tempType);
                }
                    
            }

        }


        return unlockedBeams;
    }

}


