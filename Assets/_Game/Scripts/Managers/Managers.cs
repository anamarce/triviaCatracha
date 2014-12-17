
using UnityEngine;
using System;
using System.Collections;


    public class Managers : MonoBehaviour
    {

	   private static SocialManager socialManager;
		public static SocialManager Social
		{
			get { return socialManager; }
		}

        private static GameManager gameManager;
        public static GameManager Game
        {
            get { return gameManager; }
        }
	
        private static AudioManager audioManager;
        public static AudioManager Audio
        {
            get { return audioManager; }
        }
	
        private static EffectsManager effectsManager;
        public static EffectsManager Effects
        {
            get { return effectsManager; }
        }
	
        private static DataManager dataManager;
        public static DataManager Data
        {
            get { return dataManager; }
        }

        private static PlatformManager platformManager;
        public static PlatformManager Platform
        {
            get {  return platformManager; }
        }

        private static TriviaApi triviaManager;
        public static TriviaApi Trivia
        {
            get {  return triviaManager; }
        }
	   private static PanelManager panelManager;
	   public static PanelManager SceneManager
 	   {
		get {  return panelManager; }
	   }
     
	
	
        void Start ()
        {
	    
		
            //Find the references
            socialManager = GetComponent<SocialManager>();
            gameManager = GetComponent<GameManager>();
            audioManager = GetComponent<AudioManager>();
            dataManager = GetComponent<DataManager>();
            effectsManager = GetComponent<EffectsManager>();
            platformManager = GetComponent<PlatformManager>();
            triviaManager = GetComponent<TriviaApi>();
		    panelManager = GetComponent<PanelManager>();
	  
             DontDestroyOnLoad(this);
	
		
        }





    }
