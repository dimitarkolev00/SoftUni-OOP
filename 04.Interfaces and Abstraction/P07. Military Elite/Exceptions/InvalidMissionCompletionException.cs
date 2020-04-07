using System;
using System.Collections.Generic;
using System.Text;

namespace P07.MilitaryElit.Exceptions
{
    public class InvalidMissionCompletionException : Exception
    {
        private const string DEF_EXC_MSG = "Mission already completed!";
        public InvalidMissionCompletionException()
            :base(DEF_EXC_MSG)
        {

        }

        public InvalidMissionCompletionException(string message) : base(message)
        {
        }
    }
}
