using System.Collections.Generic;
using System.Threading;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine;
using System.Collections;
using x16;

public class PlayScript : MonoBehaviour {

	// Use this for initialization


    public TurnBasedMatch mMatch = null;
    public MatchData mMatchData = null;
    [HideInInspector]
    public bool CanPlayCurrentPlayer = false;
     [HideInInspector]
    public string mFinalMessage = null;

    private string mErrorMessage = null;
    private string matchLanguage = "ES";
    private int CurrentConsecutiveAnswers = 0;

    private const float MaxTurnTime = 8.0f;
    private float mEndTurnCountdown = MaxTurnTime;
    private bool mEndingTurn = false;

    private void Reset()
    {
        mMatch = null;
        mMatchData = null;
        mFinalMessage = null;
        mEndingTurn = false;
        mEndTurnCountdown = MaxTurnTime;
       
    }
    public void LaunchMatch(TurnBasedMatch match)
    {
        Debug.Log("Iniciando LaunchMatch");
        Reset();
        mMatch = match;
      

        if (mMatch == null)
        {
            Debug.Log("mMatch es null");
            throw new System.Exception("Can't be started without a match!");

        }
        try
        {
            // Note that mMatch.Data might be null (when we are starting a new match).
            // MatchData.MatchData() correctly deals with that and initializes a
            // brand-new match in that case.

            Debug.Log("mMatchData se crea");
            mMatchData = new MatchData(mMatch.Data,mMatch);
            Debug.Log("Player1:" + mMatchData.Player1 + "-Player2:" + mMatchData.Player2);

        }
        catch (MatchData.UnsupportedMatchFormatException ex)
        {
            mFinalMessage = "Your game is out of date. Please update your game\n" +
                             "in order to play this match.";
       
            Debug.LogWarning("Failed to parse match data: " + ex.Message);
            return;
        }




        bool canPlay = (mMatch.Status == TurnBasedMatch.MatchStatus.Active &&
                mMatch.TurnStatus == TurnBasedMatch.MatchTurnStatus.MyTurn);

        
        // if the match is in the completed state, acknowledge it
        if (mMatch.Status == TurnBasedMatch.MatchStatus.Complete)
        {
            
            Debug.Log("Ak MatchComplete");
            PlayGamesPlatform.Instance.TurnBased.AcknowledgeFinished(mMatch.MatchId,
                    (bool success) =>
                    {
                        if (success)
                        {
                            // Update leaderboards, achievements, progress for the
                            // winner and the loser
                            if (mMatchData.PlayerWon == mMatch.SelfParticipantId)
                            {
                                // Increments by one the number of matches won
                                // and also checks if got achiviements and new position in leaderboard
                                // and saves correct questions in app state
                                CloudManager.Instance.FinishMatch(1); // The winner 
                            }
                            else
                            {
                                // and also checks if got achiviements and new position in leaderboard
                                // and saves correct questions in app state
                                CloudManager.Instance.FinishMatch(0); // The loser
                            }
                        }
                        else
                        {
                            Debug.LogError("Error acknowledging match finish.");
                            
                        }
                    });
        }

        SetupObjects(canPlay);
    }

    public void SetupObjects(bool canPlay)
    {
        CanPlayCurrentPlayer = canPlay;
        mFinalMessage = ExplainWhyICantPlay();
        Managers.SceneManager.LoadLevel("MatchLobby");

    }
    public string ExplainWhyICantPlay()
    {
        if (mMatch != null)
        {
            switch (mMatch.Status)
            {
                case TurnBasedMatch.MatchStatus.Active:
                    break;
                case TurnBasedMatch.MatchStatus.Complete:

                    return mMatchData.PlayerWon == mMatch.SelfParticipantId
                      ? "matchfinishedwon"
                      : "matchfinishedlost";
                       break;

                case TurnBasedMatch.MatchStatus.Cancelled:
                case TurnBasedMatch.MatchStatus.Expired:
                    return "matchcancelled";
                case TurnBasedMatch.MatchStatus.AutoMatching:
                    return "matchawaiting";
                default:
                    return "matcherror";
            }

            if (mMatch.TurnStatus != TurnBasedMatch.MatchTurnStatus.MyTurn)
            {
                return "matchnotyourturn";
            }
        }
        return "";
    }

    public string GetCurrentMatchStatus()
    {
        if (mMatch != null)
            return mMatch.Status.ToString();
        else
        {
            return "";
        }
    }

    public string GetCurrentMatchParticipantID()
    {
        if (mMatch != null)
            return mMatch.SelfParticipantId;
        else
        {
            return "";
        }
    }

    public List<Participant> GetCurrentMatchParticipants()
    {
        if (mMatch != null)
        {
            Debug.Log("No es null");
            return mMatch.Participants;
        }
        else
        {
            Debug.Log("es null");
            return null;
        }
    }
    public int GetCurrentTotalAnswers()
    {
        if (mMatchData != null)
            return mMatchData.topanswers;

        return 0;
    }
    public int GetCurrentMatchScoreParticipantID(string participantId)
    {
        if (mMatchData != null)
            if (participantId == mMatchData.Player1)
            {
                return mMatchData.Player1Answers;
            }
            else
            {
                return mMatchData.Player2Answers;
            }

            
        else
        {
            return 0;
        }
    }
    public bool CanIPlayCurrentMatch()
    {
        bool canPlay = false;
        if (mMatch != null)
        {
            canPlay = (mMatch.Status == TurnBasedMatch.MatchStatus.Active &&
                            mMatch.TurnStatus == TurnBasedMatch.MatchTurnStatus.MyTurn);
        }
        return canPlay;
    }
    public int GetCurrentMatchScore()
    {
        if (mMatch != null)
            return GetCurrentMatchScoreParticipantID(mMatch.SelfParticipantId);
        else
            return 0;
    }

    public void IncrementCurrentConsecutiveAnswers()
    {
        if (mMatch != null)
        {
            if (mMatchData != null)
            {
                mMatchData.AddConsecutiveScoreParticipantID(mMatch.SelfParticipantId, 1);
            }
        }
    }

    public void IncrementCorrectAnswers()
    {

        if (mMatch != null)
        {
            if (mMatchData != null)
            {
                mMatchData.AddScoreParticipantID(mMatch.SelfParticipantId, 1);

            }

        }
    }

    public void ResetConsecutiveAnswers()
    {
        if (mMatch != null)
        {
            if (mMatchData != null)
            {
                mMatchData.Player1ConsecutiveAnswers = 0;
                mMatchData.Player2ConsecutiveAnswers = 0;

            }

        }
    }

    public int GetCurrentConsecutiveAnswers()
    {
        int cant = -1;
        if (mMatch != null)
        {
            if (mMatchData != null)
            {
                cant = mMatchData.GetCurrentConsecutivesAnswers(mMatch.SelfParticipantId);
            }
        }
        return cant;
    }

    public bool CheckWinner()
    {
        if (mMatch != null)
        {
            if (mMatchData != null)
            {
                int currentScore = mMatchData.GetScoreParticipantID(mMatch.SelfParticipantId);
                if (currentScore == mMatchData.topanswers)
                    return true;
                else
                    return false;

            }
        }
        return false;
    }
    public void FinishMatch()
    {
        // define the match's outcome
        MatchOutcome outcome = new MatchOutcome();
        outcome.SetParticipantResult(mMatch.SelfParticipantId, MatchOutcome.ParticipantResult.Win);
       if (mMatch.SelfParticipantId==mMatchData.Player1)
            outcome.SetParticipantResult(mMatchData.Player2, MatchOutcome.ParticipantResult.Loss);
       else
       {
           outcome.SetParticipantResult(mMatchData.Player1, MatchOutcome.ParticipantResult.Loss);
           
       }
        mMatchData.PlayerWon = mMatch.SelfParticipantId;

        PlayGamesPlatform.Instance.TurnBased.Finish(mMatch.MatchId, mMatchData.ToBytes(),
                   outcome, (bool success) =>
                  {
                      if (success)
                      {
                          mFinalMessage = "YOU WON!!";
                        
                      }
                      else
                      {
                          mFinalMessage = "Error winning";
                      }
                  });
        Debug.Log(mFinalMessage);
    }

    public void TriggerNextTurn()
    {
        if (mMatch != null)
        {
            if (mMatch.AvailableAutomatchSlots == 0)
            {
                mMatchData.SelectOpponent(mMatch.SelfParticipantId);
                this.ResetConsecutiveAnswers();

                PlayGamesPlatform.Instance.TurnBased.TakeTurn
                    (mMatch.MatchId, mMatchData.ToBytes(),
                        mMatchData.CurrentPlayer,
                        (bool success) =>
                        {
                            mFinalMessage = success ? "Done for now!" : "ERROR sending turn.";
                            if (!success) // Handle error
                            {
                                
                            }
                        }
                    );
                Debug.Log(mFinalMessage);
            }
            else // is a quick match
            {
                mMatchData.CurrentPlayer = "";
                this.ResetConsecutiveAnswers();
                PlayGamesPlatform.Instance.TurnBased.TakeTurn
                (mMatch.MatchId, mMatchData.ToBytes(), null
                   ,
                  (bool success) =>
                  {
                      mFinalMessage = success ? "Done for now!" : "ERROR sending turn.";
                      if (!success) // Handle error
                      {
                          
                      }
                  }
                );
                Debug.Log(mFinalMessage);


            }



        }
    }

    public void TriggerMyTurnAgain()
    {
        if (mMatch != null)
        {

            PlayGamesPlatform.Instance.TurnBased.TakeTurn
                (mMatch.MatchId, mMatchData.ToBytes(),
                    mMatch.SelfParticipantId,
                    (bool success) =>
                    {
                        mFinalMessage = success ? "Again!" : "ERROR sending turn.";
                        if (!success) // Handle error
                        {
                            
                        }
                    }
                );
            Debug.Log(mFinalMessage);

        }
    }

    public string GetCurrentMatchID()
    {
        if (mMatch != null)
        {
            return mMatch.MatchId;
        }
        return "";
    }
}
