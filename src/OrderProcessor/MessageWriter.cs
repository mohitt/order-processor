using System;

namespace OrderProcessor
{
    internal class MessageWriter:IMessageWriter
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}