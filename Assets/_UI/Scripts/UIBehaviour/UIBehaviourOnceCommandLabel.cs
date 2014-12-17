using UnityEngine;
using System.Collections;

namespace x16
{
    public class UIBehaviourOnceCommandLabel : MonoBehaviour
    {
        public UILabel Label = null;
        public CommandType Command;


        // Use this for initialization
        private void Start()
        {
            Label.text = Commands.Instance.Command(Command.ToString(), "").ToString();
        }

    }
}