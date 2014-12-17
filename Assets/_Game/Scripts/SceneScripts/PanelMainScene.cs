using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine;
using System.Collections;
using x16;

public class PanelMainScene : PanelScript {

	// Use this for initialization

    public PlayScript PlayGame;
   
    private string mErrorMessage = null;

    private const int Opponents = 1;
    private const int Variant = 0;

  
    TurnBasedMatch mIncomingMatch = null;
    Invitation mIncomingInvite = null;

    public void Start()
    {
      
        Messenger.AddListener("ActionShowMatches" , ShowMatches);
        Messenger.AddListener("ActionSigInOut", signInOut);
        Messenger.AddListener("ActionCreateMatch", CreateMatch);
        Messenger.AddListener("ActionCreateQuickMatch", CreateQuickMatch);

        PlayGamesPlatform.Instance.TurnBased.RegisterMatchDelegate(OnGotMatch);
     
        PlayGamesPlatform.Instance.RegisterInvitationDelegate(OnGotInvitation);

       

       
    }

    void CreateMatch()
    {
        PlayGamesPlatform.Instance.TurnBased.CreateWithInvitationScreen(1, 1,0, OnMatchStarted);
    }

    void CreateQuickMatch()
    {
        PlayGamesPlatform.Instance.TurnBased.CreateQuickMatch(1, 1, 0, OnMatchStarted);
    }
    void ShowMatches()
    {
        PlayGamesPlatform.Instance.TurnBased.AcceptFromInbox(OnMatchStarted);
    }

    void signInOut()
    {
        PlayGamesPlatform.Instance.SignOut();
     
        Managers.SceneManager.LoadLevel("Xtudio16");

    }
   
    protected void OnMatchStarted(bool success, TurnBasedMatch match)
    {
     
        if (!success)
        {
            mErrorMessage = "There was a problem setting up the match.\nPlease try again.";
            Debug.Log(mErrorMessage);
            return;
        }
        Debug.Log("PlayGame LaunchMAtch");
        PlayGame.LaunchMatch(match);

     
    }
    protected void OnGotMatch(TurnBasedMatch match, bool shouldAutoLaunch)
    {
        Debug.Log("Ongotmatch, shouldAutoLaunch=" + shouldAutoLaunch.ToString());

        if (shouldAutoLaunch)
        {
            // if shouldAutoLaunch is true, we know the user has indicated (via an external UI)
            // that they wish to play this match right now, so we take the user to the
            // game screen without further delay:
            Debug.Log("MainMenu.cs:OnGotMatch:OngotMatchTrue");
            OnMatchStarted(true, match);
        }
        else
        {
            // if shouldAutoLaunch is false, this means it's not clear that the user
            // wants to jump into the game right away (for example, we might have received
            // this match from a background push notification). So, instead, we will
            // calmly hold on to the match and show a prompt so they can decide
            Debug.Log("MainMenu.cs:OnGotMatch:OngotMatchFalse");
            mIncomingMatch = match;
        }
    }

    protected void OnGotInvitation(Invitation invitation, bool shouldAutoAccept)
    {
        Debug.Log("MainMenu.cs:OnGotInvitation:Starting, ShouldAutoAccept = " + shouldAutoAccept.ToString());
        if (invitation.InvitationType != Invitation.InvType.TurnBased)
        {
            // wrong type of invitation!
            return;
        }

        if (shouldAutoAccept)
        {
            // if shouldAutoAccept is true, we know the user has indicated (via an external UI)
            // that they wish to accept this invitation right now, so we take the user to the
            // game screen without further delay:
          
            Debug.Log("MainMenu.cs:OnGotInvitation:Accepting invitation ");
            PlayGamesPlatform.Instance.TurnBased.AcceptInvitation(invitation.InvitationId, OnMatchStarted);
        }
        else
        {
            // if shouldAutoAccept is false, we got this invitation in the background, so
            // we should not jump directly into the game
            Debug.Log("MainMenu.cs:OnGotInvitation:False");
            mIncomingInvite = invitation;
        }
    }

}
