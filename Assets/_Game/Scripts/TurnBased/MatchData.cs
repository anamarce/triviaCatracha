


using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine;

using System.IO;




public class MatchData
{
    private const int Header = 201403; // Para numero de version

    public string CurrentPlayer = "";
    public string Player1 = "";
    public string Player2 = "";

    public int Player1Answers = 0;
    public int Player2Answers = 0;

    public  bool Player1Wins = false;
    public bool Player2Wins = false;


    public int topanswers = 20;
    public string PlayerWon = "";

    public int Player1ConsecutiveAnswers = 0;
    public int Player2ConsecutiveAnswers = 0;

    
    public MatchData()
    {

    }

    public int GetScoreParticipantID(string participantid)
    {
        if (participantid == Player1)
        {
            return Player1Answers;
        }
        else
        {
            return Player2Answers;
        }
        
    }


    public void AddScoreParticipantID(string participantid, int answers)
    {
        if (participantid == Player1)
        {
            Player1Answers+=answers;
        }
        else
        {
            Player2Answers+=answers;
        }
    }


    public MatchData(byte[] b, TurnBasedMatch match) : this()
    {
        if (b != null)
        {
            ReadFromBytes(b);
            if (this.Player2 == "")
            {
                if(match.AvailableAutomatchSlots==0)
                {
                    this.Player2 = match.Participants[1].ParticipantId;
                    Debug.Log("Opponent Updated:" + this.Player2);
                }
            }
        }
        else
        {
           
            this.Player1 = match.SelfParticipantId;
            Debug.Log("First Time:" + this.Player1);

            this.CurrentPlayer = match.SelfParticipantId;
            if (match.AvailableAutomatchSlots==0)
            {
              
                this.Player2 = match.Participants[1].ParticipantId;
                Debug.Log("Opponent:" + this.Player2);
            }

           
        }
    }

   
    public byte[] ToBytes()
    {
        MemoryStream memStream = new MemoryStream();
        BinaryWriter w = new BinaryWriter(memStream);
        w.Write(Header);
        w.Write(this.CurrentPlayer);
        w.Write(this.Player1);
        w.Write(this.Player2);
        w.Write(this.Player1Answers);
        w.Write(this.Player2Answers);
        w.Write(this.topanswers);
        w.Write(this.PlayerWon);
        w.Write(this.Player1ConsecutiveAnswers);
        w.Write(this.Player2ConsecutiveAnswers);
        
        
        w.Close();
        byte[] buf = memStream.GetBuffer();
        memStream.Close();
        return buf;
    }

    private void ReadFromBytes(byte[] b)
    {
        BinaryReader r = new BinaryReader(new MemoryStream(b));
        int header = r.ReadInt32();
        if (header != Header)
        {
            // Wrong header
            throw new UnsupportedMatchFormatException("Match data header " + header +
                                                      " not recognized.");
        }
        this.CurrentPlayer = r.ReadString();
        this.Player1 = r.ReadString();
        this.Player2 = r.ReadString();
        this.Player1Answers = r.ReadInt32();
        this.Player2Answers = r.ReadInt32();
        this.topanswers = r.ReadInt32();
        this.PlayerWon = r.ReadString();
        this.Player1ConsecutiveAnswers = r.ReadInt32();
        this.Player2ConsecutiveAnswers = r.ReadInt32();


    }


    public class UnsupportedMatchFormatException : System.Exception
    {
        public UnsupportedMatchFormatException(string message) : base(message)
        {
        }
    }

   

    public override string ToString()
    {
      
      
        return string.Format(@"Player1={0},AnswersPlayer1={1},Player2={2},AnswersPlayer2={3},
                              CurrentPlayer={4}",Player1,Player1Answers,Player2,Player2Answers,
                                                CurrentPlayer);

    }

    public void ResetCurrentConsecutiveAnswers(string selfParticipantId)
    {
        if (selfParticipantId == Player1)
        {
            Player1ConsecutiveAnswers = 0;
        }
        else
        {
            Player2ConsecutiveAnswers =0;
        }

    }
    public void AddConsecutiveScoreParticipantID(string selfParticipantId, int answers)
    {
        if (selfParticipantId == Player1)
        {
            Player1ConsecutiveAnswers += answers;
        }
        else
        {
            Player2ConsecutiveAnswers += answers;
        }
    }

    public int GetCurrentConsecutivesAnswers(string selfParticipantId)
    {
        if (selfParticipantId == Player1)
        {
            return Player1ConsecutiveAnswers;
        }
        else
        {
            return Player2ConsecutiveAnswers;
        }
    }

    public void SelectOpponent(string selfParticipantId)
    {
        if (selfParticipantId == Player1)
        {
            CurrentPlayer = Player2;
        }
        else
        {
            CurrentPlayer = Player1;
        }
    }
}
