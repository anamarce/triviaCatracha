using UnityEngine;
using System.Collections;

public class TopicSelectScript : PanelScript
{

    public PlayScript PlayGame;
    public UILabel TopicNameLabel;
    public UISprite TopicSprite;
    public UILabel CurrentScore;
    public UIButton SpinButton;
    public UIButton PlayButton;

    public int MinTopicTimes = 10;
    public int MaxTopicTimes = 20;

    private bool SpinStarted = false;
    public float SelectionTopicTime = 1F;
   
    private int HowManyTimes = 0;
    private int CountTopicTimes = 0;
    private float endTime;
    private int timeLeft;

    private int CurrentTopicIndex = -1;
	// Use this for initialization

    void Start()
    {
        Messenger.AddListener("SpinButtonClicked", SpinHandler);

    }
    void OnEnable()
    {
       
        SpinStarted = false;
        Random.seed = (int)Time.time;
        SpinButton.isEnabled = true;
        PlayButton.isEnabled = false;

        if (CurrentScore != null)
        {
            string temp = string.Format("{0}/{1}",
                PlayGame.GetCurrentMatchScore(),
                PlayGame.mMatchData.topanswers);
            CurrentScore.text = temp;
        }

      
    }

    void SpinHandler()
    {
        SpinButton.isEnabled = false;
        SpinStarted = true;
        CountTopicTimes = 0;
        endTime = Time.time + SelectionTopicTime;
        HowManyTimes = Random.Range(MinTopicTimes, MaxTopicTimes);
    }
   
    void Update()
    {
        if (SpinStarted)
        {
             
            timeLeft = (int)(endTime - Time.time);
            if (timeLeft <= 0)
            {
                timeLeft = 0;
                endTime = Time.time + SelectionTopicTime;
                
                CurrentTopicIndex = Random.Range(0, Managers.Trivia.TopicsParseKey.Length);

                string topickey = Managers.Trivia.GetTopicName(CurrentTopicIndex);
                TopicNameLabel.text = Localization.Localize(topickey);
               
                CountTopicTimes = CountTopicTimes +1;

                TopicSprite.spriteName = Managers.Trivia.GetSpritName(CurrentTopicIndex);
                if (CountTopicTimes == HowManyTimes)
                {
                    PlayButton.isEnabled = true;
                    SpinButton.isEnabled = false;
                    Managers.Trivia.CurrentTopicIndexSelected = CurrentTopicIndex;
                    Managers.Trivia.CurrentTopicKey = topickey;
            
                    SpinStarted = false;
                }

            }
      
        }

    }
	
}
