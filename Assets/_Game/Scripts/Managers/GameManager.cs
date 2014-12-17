using System;
using UnityEngine;



    public  class GameManager : MonoBehaviour {

	    public Preferences preferences = new Preferences();

      
        void Start()
        {
     
           
         
		    preferences.LoadPreferences();
     
        }

   
	
	
    }
