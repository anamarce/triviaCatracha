using UnityEngine;
using System.Collections;
using System;
using x16;

[Serializable]
public class Preferences
{


  
   
    
	public bool EnableSound;



    public Preferences()
    {
	
		EnableSound = true;
    }

    public void LoadPreferences()
    {
        //Sound
        if (PlayerPrefs.HasKey("EnableSound"))
        {
            if (PlayerPrefs.GetString("EnableSound") == "ON")
                EnableSound = true;
            else
                EnableSound = false;
        }
        else
            EnableSound = true;
        
	
         

    }

    public void SavePreferences()
    {
        if (EnableSound)
            PlayerPrefs.SetString("EnableSound","ON");
        else
            PlayerPrefs.SetString("EnableSound", "OFF");

      
      
        PlayerPrefs.Save();
    }

    
}
