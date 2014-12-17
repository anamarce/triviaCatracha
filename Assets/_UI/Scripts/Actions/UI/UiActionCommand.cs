using UnityEngine;
using System.Collections;

namespace x16
{
    public class UiActionCommand : UiAction
    {
        public CommandType Command;
        public string Argument;



        public override void ActionPerformed()
        {
            Commands.Instance.Command(Command.ToString(), Argument);

        }

    }
}
