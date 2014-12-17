 using UnityEngine;
using System;
using x16;

public enum CommandType
{
    Camera, GameType
}

    public class Commands : MonoBehaviour
    {
      
        #region Singleton
        private static Commands instance;

  


        public static Commands Instance
        {
            get
            {
                if (instance == null)
                {
                    var obj = new GameObject("Commands");
                    var newInstance = obj.AddComponent<Commands>();
                    instance = newInstance;
                }
                return instance;
            }
        }
        #endregion

   

        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(this);
        }


        string ArgumentError(string msg)
        {

            Debug.LogWarning(msg);
            return "Error:" + msg;
        }

        public object Command(string command, string value)
        {
            try
            {
           
                CommandType cmd = (CommandType)Enum.Parse(typeof(CommandType), command, true);

                if (cmd == CommandType.Camera)
                    return CameraCommand(value);
           
           
            }
            catch
            {
                Debug.LogWarning(command + " not found");
                return "Command not found";
            }
            return "";
        }


        string CameraCommand(string value)
        {

            if (value == "" || value == null)
                //return Managers.Game.Preferences.CameraType.ToString();
                return "Default";
            try
            {
          
                CameraType argument = (CameraType)Enum.Parse(typeof(CameraType), value.ToUpper(), true);
                //return (Managers.Game.Preferences.CameraType = argument).ToString();
                return "Default";
            }
            catch
            {
                return value + " is not a valid camera argument";
            }
        }

 
    
    }


