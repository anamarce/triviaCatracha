using UnityEngine;
using System.Collections;

namespace x16
{
    public class UIBehaviourCommandLabel : MonoBehaviour
    {
        public UILabel Label = null;
        public CommandType Command;


        // Use this for initialization
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {
            Label.text = Commands.Instance.Command(Command.ToString(), "").ToString();
        }
    }
}
