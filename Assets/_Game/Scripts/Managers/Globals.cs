using System.Text.RegularExpressions;

namespace x16
{
    //Most of the Enums of the Game
  

    public enum GraphicsQuality { VERYLOW, LOW, MEDIUM, HIGH, VERYHIGH };
    public enum RuntimePlatform { PC, IOS, ANDROID, WP8 }
    public enum CameraType { FIXED, FOLLOWUP }


 

    public static class Globals
    {

		
        public class LeaderBoards
        {
            public static string TopGeekLeaderBoard = "CgkIwdz54IMaEAIQAg";

        }

        public class Achievements 
        {
            public static string GeekStarter     = "CgkIwdz54IMaEAIQCA";
            public static string GeekPro         = "CgkIwdz54IMaEAIQCQ";
            public static string GeekMaster      = "CgkIwdz54IMaEAIQBQ";
            public static string ChosenOne       = "CgkIwdz54IMaEAIQBg";
            public static string PeacockGeek     = "CgkIwdz54IMaEAIQBw";
            public static string[] AchievementsIDS = 
            {
            "CgkIwdz54IMaEAIQCA", 
            "CgkIwdz54IMaEAIQCQ",
            "CgkIwdz54IMaEAIQBQ" ,
            "CgkIwdz54IMaEAIQBg",
            "CgkIwdz54IMaEAIQBw"
            };

            public static int[] TotalMatchesRequired = { 29, 71, 173, 281, 541 };

        }

        public class Tags
        {
            public static string Player = "Player";
        }

        public class Constants
        {
            public static string MainScene = "MainScene";
          

            public static readonly string[] Languages = {"English","Espanol"};


            public static int MaxAnswers = 20;
            public static int IntervalAnswers = 5;

            public static int Adinterval = 3;
            public static int MaxTopics = 8;
            public static string CurrentVersion ="TGV1";

            
        }
       
      


    }
}

