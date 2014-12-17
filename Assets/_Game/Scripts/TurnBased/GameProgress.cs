using UnityEngine;
using System.Collections;
using x16;

public class GameProgress  {

    private const string PlayerPrefsKey = "triviageek-game-progress";
    private int mHighestPostedScore = 0;

    public const int TopicCount = 8;
    private int [] AnswersByTopics = new int[TopicCount];

  // do we have modifications to write to disk/cloud?
    private bool mDirty = false;

    public GameProgress()
    {
        mHighestPostedScore = 0;
        for (int i = 0; i < TopicCount; i++)
        {
            AnswersByTopics[i] = 0;
        }
    }
    public bool Dirty
    {
        get
        {
            return mDirty;
        }
        set
        {
            mDirty = value;
        }
    }
    public int TotalScore
    {
        get
        {
            return mHighestPostedScore;
        }
        set { mHighestPostedScore = value; }
    }

    public void SaveToDisk()
    {
        PlayerPrefs.SetString(PlayerPrefsKey, ToString());
        mDirty = false;
    }
    public static GameProgress LoadFromDisk()
    {
        string s = PlayerPrefs.GetString(PlayerPrefsKey, "");
        if (s == null || s.Trim().Length == 0)
        {
            return new GameProgress();
        }
        return GameProgress.FromString(s);
    }

    public static GameProgress FromBytes(byte[] b)
    {
        return GameProgress.FromString(System.Text.ASCIIEncoding.Default.GetString(b));
    }

    public static GameProgress FromString(string s)
    {
        //Formato es
        // Version:HighestScore:RespTopic1:RespTopic2 .... etc
        // Highest score no es mas que todos los matchs ganados y lo que sigue
        // es la cantidad de preguntas que ha contestado correctamente por cada topic, por ejemplo
        // Anime esta en indice cero , se ha contestado 3 preguntas de ese tema aparecera 3.
        // Eje :   TGV1:10:3:0:0:0:0   Version1, 10 matches ganados, 3 correctas en anime

        GameProgress gp = new GameProgress();
        string[] p = s.Split(new char[] { ':' });
        if (!p[0].Equals(Globals.Constants.CurrentVersion))
        {
            Debug.LogError("Failed to parse game progress from: " + s);
            return gp;
        }
        gp.mHighestPostedScore = System.Convert.ToInt32(p[1]);
        for (int i = 2; i < p.Length; i++)
        {
            gp.AnswersByTopics[i - 2] = System.Convert.ToInt32(p[i]);
        }
      
        return gp;
    }
    public void MergeWith(GameProgress other)
    {
        int i;
        for (i = 0; i < TopicCount; i++)
        {
            if (other.AnswersByTopics[i] > AnswersByTopics[i])
            {
                AnswersByTopics[i] = other.AnswersByTopics[i];
                mDirty = true;
            }
        }
        if (other.mHighestPostedScore > mHighestPostedScore)
        {
            mHighestPostedScore = other.mHighestPostedScore;
            mDirty = true;
        }
    }
   
    public override string ToString()
    {
        string s = "TGV1:" + mHighestPostedScore.ToString();
        
        for (int i = 0; i < TopicCount; i++)
        {
            s += ":" + AnswersByTopics[i];
        }
        return s;
    }

    public byte[] ToBytes()
    {
        return System.Text.ASCIIEncoding.Default.GetBytes(ToString());
    }

    public void IncrementTopicCorrectAnswers(int topicindex, int val)
    {
        AnswersByTopics[topicindex] += val;
    }
}
