using UnityEngine;
using System.Collections;

namespace x16
{
    public class ActionCommand : Action
    {


        public CommandType Command;
        public string Argument;


        public override void ActionPerformed()
        {

            //	print (Command.ToString());
            Commands.Instance.Command(Command.ToString(), Argument);

        }

    }
}
