using UnityEngine;
using System.Collections;
using x16;

public enum PlAYSTATUS
{
    NOTANSWER,
    CORRECT,
    WRONG,
    TIMEOUT
};

    
public class MatchPlayScript : PanelScript {

	// Use this for initialization
    public PlayScript PlayGame;
    public UILabel LabelTime;
    public UILabel LabelTopic;
    public UILabel LabelQuestion;
    public UILabel LabelAnswer;
    public UIButton ButtonContinue;
    public UIButton ButtonFailed;
    public UIButton ButtonWon;
    public ButtonOptionScript [] OptionButtons;

    public float TimeToAnswer = 25F;
    public AudioClip tickSound;
    private float endTime;
    private int timeLeft;
    private PlAYSTATUS CurrentStatus = PlAYSTATUS.NOTANSWER;
    private AudioSource audiosource=null;
	private int CurrentIndexSelected= -1;

	void Start ()
	{
		TimeToAnswer=25F;
        Messenger.AddListener<int>("CorrectOptionPressed", CorrectOptionHandler);
        Messenger.AddListener<int>("WrongOptionPressed", WrongOptionHandler);
	}

    void OnEnable()
    {
		
		
        TimeToAnswer = 25F;
        CurrentStatus = PlAYSTATUS.NOTANSWER;
		CurrentIndexSelected= -1;
		endTime = Time.time + TimeToAnswer;
		timeLeft = (int)TimeToAnswer;

		CleanUI();
    
        ShowInitialInfo();

        if (tickSound != null)
            audiosource = Managers.Audio.Play(tickSound, transform.position, TimeToAnswer, true);

    }
	void CleanUI()
	{
		if (LabelTopic != null)
		{
			LabelTopic.text = "";
		}
		
		if (LabelTime != null)
		{
			LabelTime.text = "00";
			LabelTime.color = Color.red;
		}
		if ( LabelAnswer!=null)
			LabelAnswer.text ="";

		for(int i = 0 ; i<OptionButtons.Length;i++)
		{
			OptionButtons[i].SpriteOption.spriteName = "Background-03-Round";

		}
		if (ButtonContinue != null)
			NGUITools.SetActive(ButtonContinue.gameObject, false);
		if (ButtonFailed != null)
			NGUITools.SetActive(ButtonFailed.gameObject, false);
		if (ButtonWon != null)
			NGUITools.SetActive(ButtonWon.gameObject, false);
	}
    void DisableOptions()
    {
        for (int i = 0; i < OptionButtons.Length; i++)
        {
            OptionButtons[i].EnableOption = false;
        }
    }
    void CorrectOptionHandler(int index)
    {
		CurrentStatus = PlAYSTATUS.CORRECT;
        DisableOptions();
		CurrentIndexSelected = index;
        if (LabelAnswer != null)
        {
         
			OptionButtons[CurrentIndexSelected].SpriteOption.spriteName = "Button-03-Accept";
           
            LabelAnswer.color = Color.green;
            LabelAnswer.text = Localization.Localize("correctanswer");
    		
        }
        PlayGame.IncrementCorrectAnswers();
        PlayGame.IncrementCurrentConsecutiveAnswers();
        CloudManager.Instance.UpdateTopicsAnswers(Managers.Trivia.CurrentTopicIndexSelected, 1);

        TurnOffTimerSound();
        bool IsWinner = PlayGame.CheckWinner(); // Managers.Social.CheckWinner();
        if (IsWinner)
        {
           // Managers.Social.FinishMatch();
            PlayGame.FinishMatch();

            LabelAnswer.color = Color.white;
            LabelAnswer.text = Localization.Localize("youwon");
            if (ButtonWon != null)
                NGUITools.SetActive(ButtonWon.gameObject, true);
        }
        else
        {
            if (PlayGame.GetCurrentConsecutiveAnswers() == Globals.Constants.IntervalAnswers)
            {
        
              
                PlayGame.TriggerNextTurn();

                if (ButtonFailed != null)
                    NGUITools.SetActive(ButtonFailed.gameObject, true);
       
                 LabelAnswer.color = Color.white;
                LabelAnswer.text = Localization.Localize("givechancetootherplayer");
            }
            else
            {
                PlayGame.TriggerMyTurnAgain();
                if (ButtonContinue != null)
                    NGUITools.SetActive(ButtonContinue.gameObject, true);

            }
        }

    }

    void WrongOptionHandler(int index)
    {
		CurrentStatus = PlAYSTATUS.WRONG;
        DisableOptions();
		CurrentIndexSelected = index;
        if (LabelAnswer != null)
        {
           // Invoke("ChangeColor", 0.1F);
			OptionButtons[CurrentIndexSelected].SpriteOption.spriteName = "Button-03-Death";
            LabelAnswer.color = Color.red;
            LabelAnswer.text = Localization.Localize("wronganswer");
	     }
        TurnOffTimerSound();
    
        PlayGame.TriggerNextTurn();
        if (ButtonFailed != null)
            NGUITools.SetActive(ButtonFailed.gameObject, true);



    }
	void ChangeColor ()
	{

	    if (CurrentStatus == PlAYSTATUS.CORRECT)
	    {
			OptionButtons[CurrentIndexSelected].SpriteOption.spriteName = "Button-03-Accept";
	    }
	    else
	    {
            OptionButtons[CurrentIndexSelected].SpriteOption.spriteName = "X";
	    }

	
	}

    void TurnOffTimerSound()
    {
        if (audiosource != null)
        {
            audiosource.Stop();
            Destroy(audiosource.gameObject);
            audiosource = null;
        }
    }
    void TimeOutHandler()
    {
        CurrentStatus= PlAYSTATUS.TIMEOUT;
        DisableOptions();
        if (LabelAnswer != null)
        {
            LabelAnswer.color = Color.red;
            LabelAnswer.text = Localization.Localize("timeout");

        }
        TurnOffTimerSound();
    
        PlayGame.TriggerNextTurn();

        if (ButtonFailed != null)
            NGUITools.SetActive(ButtonFailed.gameObject, true);

        
    }
    void ShowInitialInfo()
    {
        if (LabelQuestion != null)
        {
            Debug.Log(Managers.Trivia.lastDebugMessage);
            TriviaQuestion q = Managers.Trivia.GetCachedQuestion();
            q.Shuffle();

            LabelQuestion.text = q.Question;
            for (int i=0 ; i < q.options.Length; i++)
            {
                //TODO setear los botones de options
                OptionButtons[i].LabelOption.text = q.options[i];
                OptionButtons[i].IndexAnswer = q.indexAnswer;
                OptionButtons[i].IndexOption = i;
                OptionButtons[i].EnableOption = true;

            }
        }
        if (LabelTopic != null)
        {
            string topickey = Managers.Trivia.GetTopicName(Managers.Trivia.CurrentTopicIndexSelected);
            LabelTopic.text = Localization.Localize(topickey);
        }

        if (LabelTime != null)
            LabelTime.text = timeLeft.ToString();
	    
        
    }
    void RefreshGameTime()
    {
        timeLeft = (int)(endTime - Time.time);


        if (timeLeft <= 0)
        {
         
            timeLeft = 0;

        }
     


    }

	// Update is called once per frame
	void Update () {

	    if (CurrentStatus == PlAYSTATUS.NOTANSWER)
	    {
	        if (timeLeft > 0)
			{
				RefreshGameTime();
				if (LabelTime != null)
				{
					LabelTime.text = timeLeft.ToString();
					
				}
			}
	        else
	        {

               TimeOutHandler();
	        }
	    }
	}
}
