
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using x16;

public class SocialManager : MonoBehaviour {

	// Use this for initialization

	private bool userAuthenticated =false;
	// the match the player is being offered to play right now
	
    private string matchLanguage = "ES";
	

  
  
	public bool IsAuthenticated()
	{
		return Social.localUser.authenticated;
	}
	public void SignOut()
	{
		if (Social.localUser.authenticated) 
		{
			((PlayGamesPlatform) Social.Active).SignOut();
			userAuthenticated = false;
		}
	}


	

    public void SetMatchLanguage(string language)
    {
        matchLanguage = language;

    }

    public string GetMatchLanguage()
    {
        return matchLanguage;
    }


	public void ShowLeaderboardUI() 
    {
	
			Social.ShowLeaderboardUI();
			((PlayGamesPlatform) Social.Active).ShowLeaderboardUI(Globals.LeaderBoards.TopGeekLeaderBoard);
		
	
	}

	public void ShowAchievementsUI() 
    {
			Social.ShowAchievementsUI();
    }
    
    public void UpdateAchievements()
    {

        //((PlayGamesPlatform)Social.Active).IncrementAchievement(
        //  Globals.Achievements.GeekStarter, 1, (bool success) =>
        //  {
        //      // handle success or failure
        //  });
    }
		


   
  

  


   

   
    
  

 

   
   
}
