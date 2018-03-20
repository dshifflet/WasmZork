using System;

namespace zmachine
{
    public class ConsoleInputOutput : IZmachineInputOutput
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
        public void Write(String str)
        {
            Console.Write(str);
        }

        public void WriteLine(String str)
        {
            Console.WriteLine(str);
        }
    }
}