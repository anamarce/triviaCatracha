using System;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine;
using System.Collections;

[Serializable]
public class InfoPlayers
{
    public UILabel PlayerNameLabel;
    public UILabel PlayerStatusLabel;
    public UILabel PlayerScoreLabel;
    public UISprite PlayerSprite;

}

public class MatchLobbyScript : PanelScript {

	// Use this for initialization
    public PlayScript PlayGame;

    public UILabel MatchStatusLabel;
    public UILabel TurnStatusLabel;
    public UILabel ScoreAnswersToWin;
    public UIButton PlayGameButton;
    public InfoPlayers[] infoPlayers;

    public List<Participant> participants=null;

	void OnEnable ()
	{
    
		CleanUI();
	
        RenderInfo();
        RenderParticipantsInfo();
        RenderTurnStatus();

	   
	}

	void CleanUI()
	{
		if( MatchStatusLabel!=null)
			MatchStatusLabel.text ="";

		if (TurnStatusLabel!=null)
			TurnStatusLabel.text="";


		if (ScoreAnswersToWin!=null)
			ScoreAnswersToWin.text="";

		if (PlayGameButton != null)
			PlayGameButton.isEnabled = false;

		for(int i=0; i<infoPlayers.Length;i++)
		{
			infoPlayers[i].PlayerNameLabel.text="";
			infoPlayers[i].PlayerScoreLabel.text="";
			infoPlayers[i].PlayerStatusLabel.text="";
		}
	}
    void RenderInfo()
    {
       
        Debug.Log("En Renderinfo iniciando...");
        if (PlayGameButton != null)
            PlayGameButton.isEnabled = false;
        if (MatchStatusLabel != null)
            MatchStatusLabel.text = Localization.Localize(PlayGame.GetCurrentMatchStatus());

      
    }

  
    private void RenderParticipantsInfo()
    {
        if (infoPlayers != null && infoPlayers.Length > 0)
        {
           
            Debug.Log("Iniciando PArtcipants info...");

            string MyParticipantID = PlayGame.GetCurrentMatchParticipantID();  // Managers.Social.GetCurrentMatchParticipantID();
            participants = PlayGame.GetCurrentMatchParticipants(); //   Managers.Social.GetCurrentMatchParticipants();


            int TotalAnswers = PlayGame.GetCurrentTotalAnswers(); // Managers.Social.GetCurrentTotalAnswers();
            if (ScoreAnswersToWin != null)
            {
                ScoreAnswersToWin.text = TotalAnswers.ToString();
            }
          
            if (participants == null)
            {
                Debug.Log("Participants null..");
                
            }
            int i = 0;
            foreach (Participant participant in participants)
            {
                Debug.Log("Dentro de foreach..." + i.ToString());
                if (infoPlayers[i].PlayerNameLabel != null)
                {
                    if (MyParticipantID == participant.ParticipantId)
                        infoPlayers[i].PlayerNameLabel.text = "*" + participant.DisplayName;
                    else
                    {
                        infoPlayers[i].PlayerNameLabel.text = participant.DisplayName;
                    }
                }
                Debug.Log("Dentro de foreach...Mitad");
                if (infoPlayers[i].PlayerStatusLabel != null)
                {
                    string partstatus = "participant" +  participant.Status.ToString();
                    infoPlayers[i].PlayerStatusLabel.text = Localization.Localize(partstatus);
                }
                if (infoPlayers[i].PlayerScoreLabel != null)
                {
                    int score = PlayGame.GetCurrentMatchScoreParticipantID(participant.ParticipantId); //Managers.Social.GetCurrentMatchScoreParticipantID(participant.ParticipantId);
                    infoPlayers[i].PlayerScoreLabel.text = System.Convert.ToString(score);
                }
                i++;
               
            }
        }
    }
    private void RenderTurnStatus()
    {
        if (PlayGame.CanIPlayCurrentMatch())
        {
           if (TurnStatusLabel != null)
            {
                TurnStatusLabel.text = Localization.Localize("matchisyourturn");
                if (PlayGameButton != null)
                {
                    PlayGameButton.isEnabled = true;
                }
            }

        }
        else
        {
            if (PlayGameButton != null)
            {
                PlayGameButton.isEnabled = false;
            }
            if (TurnStatusLabel != null)
            {
                string status = PlayGame.ExplainWhyICantPlay();
                if (status != "")
                    TurnStatusLabel.text = Localization.Localize(status);
                else
                {
                    TurnStatusLabel.text = "";
                }
            }

        }
    }

   
}
