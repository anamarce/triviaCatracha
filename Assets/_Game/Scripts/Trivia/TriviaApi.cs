using System;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Agregar el namespace de parse
using Parse;
using Random = UnityEngine.Random;

// PAra queries en parse https://parse.com/docs/unity_guide#queries

public class TriviaQuestion
{
	public string ObjectId;
	public int IdQuestion;
	public string Question;
	public int indexAnswer ;
	public string [] options = {"","","",""} ;  // siempre sera un array de 4 strings

    public TriviaQuestion()
    {
        // Just for testing
         ObjectId="1";
	     IdQuestion=0;
	     Question="What is the  name of this Game?";
	     indexAnswer=1 ;
        options[0] = "No name";
        options[1] = "Trivia Geek Match";
        options[2] = "Tetris";
        options[3] = "Who cares";
         
    }

    public void Shuffle()
    {
        int newindex = Random.Range(0, 4);
        if (newindex==indexAnswer)
            newindex = (newindex +1) % 4;
        string temp = options[indexAnswer];
        options[indexAnswer] = options[newindex];
        options[newindex] = temp;
        indexAnswer = newindex;
        
        return;
    }
}


public class TriviaApi : MonoBehaviour
{
    public Dictionary<string, int> TopicDictionary = new Dictionary<string, int>();
    public bool TopicsLoaded = false;

    public Dictionary<string, List<TriviaQuestion>> QuestionDictionary;

   // [HideInInspector]
    public string[] TopicsParseKey;
 //  There are set in the inspector = {"Anime","Books", "Comics","ComputerSystems","Movies",
	//                                      "Technology","TvSeries","VideoGames"};


    public string[] TopicsSprites;
  //  [HideInInspector]

	public  string[] LangParseCode = {"EN", "ES"};

    [HideInInspector]
    public int CurrentTopicIndexSelected = -1;
    [HideInInspector]
    public string CurrentMatchID = "";
    [HideInInspector]
    public string CurrentTopicKey = "";

    private TriviaQuestion CachedQuestion=null;
    [HideInInspector]
    public string lastDebugMessage="";

    void Start()
    {
		LoadTopicsStats();


    }

    public void  LoadTopicsStats()
    {
		Debug.Log("Loading Topics from Parse");
        try
        {

            TopicDictionary.Clear();
            TopicsLoaded = false;
            

           
            ParseQuery<ParseObject> query = ParseObject.GetQuery("Topics").WhereNotEqualTo("Name","");
          
            
            query.FindAsync().ContinueWith(t =>
            {
                IEnumerable<ParseObject> results = t.Result;
                foreach (ParseObject parseObject in results)
                {
                    string name = parseObject.Get<string>("Name");
                    int count = parseObject.Get<int>("Count");
                    TopicDictionary.Add(name,count);

                }
                TopicsLoaded = true;
				Debug.Log("Topics Loaded");
            });
        }
        catch (Exception ex)
        {
            Debug.Log("Mmm" + ex.StackTrace);
            TopicsLoaded = false;
        }
       
    }

	public TriviaQuestion GetCachedQuestion()
	{
	    if (CachedQuestion != null)
	    {
	        if (CachedQuestion.IdQuestion >= 0)
	        {
	            return CachedQuestion;
	        }

	    }
	    return null;

	}

    public void SetCachedQuestion(TriviaQuestion q)
    {
        CachedQuestion = q;
    }
  

    public string GetTopicName(int currentTopicIndex)
    {
        return TopicsParseKey[currentTopicIndex];
    }


    public int GetCountQuestion(string parseObjectId)
    {
      
        if (Managers.Trivia.TopicsLoaded)
        {   
			Debug.Log("Loaded Topics");
            if(Managers.Trivia.TopicDictionary.ContainsKey(parseObjectId))
                return Managers.Trivia.TopicDictionary[parseObjectId];
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;

        }
    }

    public string GetSpritName(int currentTopicIndex)
    {
        return  TopicsSprites[currentTopicIndex];
    }
}
