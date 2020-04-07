﻿using P07.MilitaryElit.Contracts;

namespace P07.MilitaryElit.IO.Contracts
{
    public interface IWriter
    {
        void Write(string text);
        void WriteLine(string text);
    }
}
