using System;

namespace OrderProcessor
{
    internal class InputReader : IInputReader
    {

        public string ReadLine()
        {
            
            return Console.ReadLine();
        }
    }
}