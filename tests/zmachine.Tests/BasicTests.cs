using System;
using Xunit;
using Xunit.Abstractions;
using ZorkConsole;

namespace zmachine.Tests
{
    public class BasicTests
    {
        private readonly ITestOutputHelper _output;


        public BasicTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void PlayZork()
        {
            var io = new SimpleInputOutput();
            var machine =  new Machine(Zork1.GetData(), io);
            WaitForOutput(machine, io);
            Assert.True(ProcessCommand(machine, io, "look").Contains("West of House"));
            Assert.True(ProcessCommand(machine, io, "open mailbox").Contains("leaflet"));
            Assert.True(ProcessCommand(machine, io, "read leaflet").Contains("(Taken)"));
            Assert.True(ProcessCommand(machine, io, "inventory").Contains("A leaflet"));
        }

        private string ProcessCommand(Machine machine, SimpleInputOutput io, string text)
        {
            io.Output = "";
            io.WaitingForInput = false;
            _output.WriteLine(">{0}", text);
            machine.processText(text);
            return WaitForOutput(machine, io);
        }

        private string WaitForOutput(Machine machine, SimpleInputOutput io)
        {
            while (!io.WaitingForInput && !machine.isFinished())
            {
                machine.processInstruction();
            }
            _output.WriteLine(io.Output.Substring(0, io.Output.Length-1));
            return io.Output;
        }

    }
}
