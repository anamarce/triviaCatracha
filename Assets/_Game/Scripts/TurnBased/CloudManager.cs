using System;
using GooglePlayGames.BasicApi;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using x16;

[Serializable]
class MyAchievement
{
    public string AchievementID;
    public int Goal;

}
public class CloudManager : GooglePlayGames.BasicApi.OnStateLoadedListener
{
    private static CloudManager sInstance = new CloudManager();

    private GameProgress mProgress;
  

    // list of achievements we know we have unlocked (to avoid making repeated calls to the API)
    private Dictionary<string, bool> mUnlockedAchievements = new Dictionary<string, bool>();
   
    // what is the highest score we have posted to the leaderboard?
    private int mHighestPostedScore = 0;  // In this case the amount of matchs the player wins


    private GooglePlayGames.BasicApi.OnStateLoadedListener mAppStateListener;
    public static CloudManager Instance
    {
        get
        {
            return sInstance;
        }
    }
    public bool Authenticated
    {
        get
        {
            return Social.Active.localUser.authenticated;
        }
    }
   
    private CloudManager()
    {
        mProgress = GameProgress.LoadFromDisk();
       
    }
    public void LoadFromCloud()
    {
        // Cloud save is not in ISocialPlatform, it's a Play Games extension,
        // so we have to break the abstraction and use PlayGamesPlatform:
        Debug.Log("Loading game progress from the cloud.");
        ((PlayGamesPlatform)Social.Active).LoadState(0, this);
    }

    public void UpdateTopicsAnswers(int topicindex, int val)
    {
        mProgress.IncrementTopicCorrectAnswers(topicindex, val);
    }
    public void FinishMatch(int score)
    {
        Debug.Log("FinishMatch:ActualScore:" + mProgress.TotalScore);
        mProgress.TotalScore += score;
        mProgress.Dirty = true;
        SaveProgress();
        ReportAllProgress();
        
    }
    public void SaveProgress()
    {
        if (mProgress.Dirty)
        {
            mProgress.SaveToDisk();
            SaveToCloud();
        }
    }
    void ReportAllProgress()
    {
        UnlockAchievements();
        PostToLeaderboard();
        SaveToCloud();
    }

    private void UnlockAchievements()
    {
        int totalMatches = mProgress.TotalScore;
    
        for (int i = 0; i < Globals.Achievements.AchievementsIDS.Length; i++)
        {
            int matchesRequired = Globals.Achievements.TotalMatchesRequired[i];
            if (totalMatches >= matchesRequired)
            {
                UnlockAchievement(Globals.Achievements.AchievementsIDS[i]);
            }
        }

       }

    public void UnlockAchievement(string achId)
    {
        if (Authenticated && !mUnlockedAchievements.ContainsKey(achId))
        {
            Social.ReportProgress(achId, 100.0f, (bool success) => { });
            mUnlockedAchievements[achId] = true;
        }
    }

    void PostToLeaderboard()
    {
        int score = mProgress.TotalScore;
        if (Authenticated && score > mHighestPostedScore)
        {
            // post score to the leaderboard
            Social.ReportScore(score, Globals.LeaderBoards.TopGeekLeaderBoard, (bool success) => { });
            mHighestPostedScore = score;
        }

    }

    void SaveToCloud()
    {
        if (Authenticated)
        {
            // Cloud save is not in ISocialPlatform, it's a Play Games extension,
            // so we have to break the abstraction and use PlayGamesPlatform:
            Debug.Log("Saving progress to the cloud...");
            ((PlayGamesPlatform)Social.Active).UpdateState(0, mProgress.ToBytes(), this);
        }
    }
     public void OnStateLoaded(bool success, int slot, byte[] data) 
     {
         Debug.Log("Cloud load callback, success=" + success);
         if (success)
         {
             ProcessCloudData(data);
         }
         else
         {
             Debug.LogWarning("Failed to load from cloud. Network problems?");
         }

       // report any progress we have to report
         ReportAllProgress();
     }
     void ProcessCloudData(byte[] cloudData)
     {
         if (cloudData == null)
         {
             Debug.Log("No data saved to the cloud yet...");
             return;
         }
         Debug.Log("Decoding cloud data from bytes.");
         GameProgress progress = GameProgress.FromBytes(cloudData);
         Debug.Log("Merging with existing game progress.");
         mProgress.MergeWith(progress);
     }
    public byte[] OnStateConflict(int slot, byte[] local, byte[] server)
    {
        Debug.Log("Conflict callback called. Resolving conflict.");

        // decode byte arrays into game progress and merge them
        GameProgress localProgress = local == null ?
            new GameProgress() : GameProgress.FromBytes(local);
        GameProgress serverProgress = server == null ?
            new GameProgress() : GameProgress.FromBytes(server);
        localProgress.MergeWith(serverProgress);

        // resolve conflict
        return localProgress.ToBytes();
    }

    public void OnStateSaved(bool success, int slot)
    {
        if (!success)
        {
            Debug.LogWarning("Failed to save state to the cloud.");

            // try to save later:
            mProgress.Dirty = true;
        }
    }


}
