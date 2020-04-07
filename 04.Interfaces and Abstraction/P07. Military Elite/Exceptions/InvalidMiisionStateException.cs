using System;

namespace P07.MilitaryElit.Exceptions
{
    public class InvalidMiisionStateException : Exception
    {
        private const string DEF_MSG = "Invalid mission state!";
        public InvalidMiisionStateException()
            :base(DEF_MSG)
        {
        }

        public InvalidMiisionStateException(string message) 
            : base(message)
        {
        }
    }
}
