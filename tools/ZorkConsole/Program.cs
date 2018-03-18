using System.Diagnostics;
using zmachine;

namespace ZorkConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Machine machine = new Machine("ZORK1.DAT");

            int numInstructionsProcessed = 0;
            while (!machine.isFinished())
            {
                if (machine.debug)
                    Debug.Write("" + numInstructionsProcessed + " : ");
                machine.processInstruction();
                ++numInstructionsProcessed;
            }
            Debug.WriteLine("Instructions processed: " + numInstructionsProcessed);
        }
    }
}
