using UnityEngine;
using System.Collections;
using GooglePlayGames;

public class PanelXtudio16 : PanelScript
{
    public bool mAuthOnStart = true;

    System.Action<bool> mAuthCallback;

    void Start()
    {
       
        mAuthOnStart = true;
        Messenger.Cleanup();
        Messenger.AddListener("SignInMessage", SigInMethod);

        mAuthCallback = (bool success) =>
        {
          
            if (success)
            {
                Debug.Log("PanelXtudio16.cs:Start:Success Login");
            
                CloudManager.Instance.LoadFromCloud();
               // SwitchToMain();
                Managers.SceneManager.LoadLevel("MainScene");
            }
        };

        // make Play Games the default social implementation

        PlayGamesPlatform.Activate();
     
       
        PlayGamesPlatform.DebugLogEnabled = false;

        // try silent authentication
        if (mAuthOnStart)
        {
            PlayGamesPlatform.Instance.Authenticate(mAuthCallback, true);
        }
    }

    void OnEnable()
    {
        Debug.Log("Enable auth false");
        mAuthOnStart = false;
    }
    void SigInMethod()
    {
       
            PlayGamesPlatform.Instance.localUser.Authenticate(mAuthCallback);
    }

    void OnDisable()
    {
       
    }
}
