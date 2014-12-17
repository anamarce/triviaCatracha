using UnityEngine;
using System.Collections;

namespace x16
{

    [System.Serializable()]
    public class ImageButtonListItem
    {
        public string ItemValue;
        public string SpriteValue;
    }


    public class UIBehaviourImageButtonList : MonoBehaviour
    {


        public UIButton Button = null;
        public CommandType command;
        public ImageButtonListItem[] ListValues;

        private int currentIndex = -1;

        private UISprite bgSprite;
        // Use this for initialization
        private void Start()
        {

            if (Button == null)
                Debug.LogError("Button label not found!");
            if (ListValues == null || ListValues.Length < 2)
                Debug.LogError("List Values needs a least 2 items");

            bgSprite = Button.GetComponentInChildren<UISprite>();
            if (bgSprite == null)
                Debug.LogError("Sprite not found in button");

            string currentItem = (string) Commands.Instance.Command(command.ToString(), "");

            for (int i = 0; i < ListValues.Length; i++)
                if (ListValues[i].ItemValue.ToUpper() == currentItem.ToUpper())
                {
                    currentIndex = i;
                    bgSprite.spriteName = ListValues[i].SpriteValue;
                }
            if (currentIndex == -1)
                Debug.Log(currentItem + " not found on list!");

        }

        //177 129 129
        private void OnClick()
        {
            currentIndex = (currentIndex + 1)%ListValues.Length;
            Commands.Instance.Command(command.ToString(), ListValues[currentIndex].ItemValue);
            bgSprite.spriteName = ListValues[currentIndex].SpriteValue;

        }



        // Update is called once per frame
        private void Update()
        {

        }
    }
}
