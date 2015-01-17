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
	public UILabel labelAnswer;
	public UILabel labelAnswerSpin;
	public float SelectionTopicTime = 2F;
    public int MinTopicTimes = 2;//10
    public int MaxTopicTimes = 3;//20

    private bool SpinStarted = false;
    private int HowManyTimes = 0;
    private int CountTopicTimes = 0;
    private float endTime;
    private float timeLeft;

    private int CurrentTopicIndex = -1;
	// Use this for initialization

    void Start()
    {
        Messenger.AddListener("SpinButtonClicked", SpinHandler);

    }
    void OnEnable()
    {
       
        SpinStarted = false;
		Random.seed = (int)Time.realtimeSinceStartup;
		NGUITools.SetActive (labelAnswerSpin.gameObject, true);
		NGUITools.SetActive (SpinButton.gameObject, true);
        //PlayButton.isEnabled = false;
		NGUITools.SetActive (labelAnswer.gameObject, false);
		NGUITools.SetActive (PlayButton.gameObject, false);
		SpinButton.isEnabled = true;
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
		endTime = Time.realtimeSinceStartup + SelectionTopicTime;
        HowManyTimes = Random.Range(MinTopicTimes, MaxTopicTimes);
    }
   
    void Update()
    {
        if (SpinStarted)
        {

			//Debug.Log("Real Time:" + Time.realtimeSinceStartup);
			timeLeft = (endTime - Time.realtimeSinceStartup);
            if (timeLeft <= 0)
            {

				Debug.Log("End time." + endTime);
				Debug.Log("Time left:"+ timeLeft);
				//Debug.Log("Real Time 2: " + Time.realtimeSinceStartup);
                timeLeft = 0;
				endTime = Time.realtimeSinceStartup + SelectionTopicTime;
                
                CurrentTopicIndex = Random.Range(0, Managers.Trivia.TopicsParseKey.Length);

                string topickey = Managers.Trivia.GetTopicName(CurrentTopicIndex);
                TopicNameLabel.text = Localization.Localize(topickey);
               
                CountTopicTimes = CountTopicTimes +1;

				Debug.Log("Count Topic Times: " +CountTopicTimes);
				Debug.Log("How many times: "+HowManyTimes);
                TopicSprite.spriteName = Managers.Trivia.GetSpritName(CurrentTopicIndex);
                if (CountTopicTimes == HowManyTimes)
                {
                    //PlayButton.isEnabled = true;
					NGUITools.SetActive (labelAnswer.gameObject, true);
					NGUITools.SetActive (PlayButton.gameObject, true);
					NGUITools.SetActive (labelAnswerSpin.gameObject, false);
					NGUITools.SetActive (SpinButton.gameObject, false);
                    SpinButton.isEnabled = false;
                    Managers.Trivia.CurrentTopicIndexSelected = CurrentTopicIndex;
                    Managers.Trivia.CurrentTopicKey = topickey;
            
                    SpinStarted = false;
                }

            }
      
        }


    }
	
}
