using System;
using Ooui;
using WebAssembly;
using zmachine;
using ZorkConsole;


namespace OouiConsole
{
    class Program
    {
        /* TO RUN THIS LOOK IN THE FOLLOWING FOLDER...
         * tools\OouiConsole\bin\Debug\netcoreapp2.0\dist
         * Load the index.html in a web browser.  It compiles down to a WASM.
         */
        private static Span _area;
        
        static void Main()
        {
            //Start Game
            var io = new SimpleInputOutput();
            var machine = new Machine(Zork1.GetData(), io);

            _area = new Span();

            var frm = new Form();

            var textInput = new TextInput();

            _area.Text = "";

            frm.Submit += (sender, eventArgs) =>
            {
                DisplayOutput(ProcessCommand(machine, io, textInput.Value));
                //textInput.Value = ""; <== DOESN'T WORK RIGHT
                //value doesn't change so...
                var js = string.Format("document.getElementById(\"{0}\").value = \"\";window.scrollTo(0,document.body.scrollHeight);", textInput.Id);
                Runtime.InvokeJS(js);
            };
            var container = new Paragraph();
            var p1 = new Paragraph();
            var p2 = new Paragraph();

            p1.AppendChild(_area);
            frm.AppendChild(textInput);
            p2.AppendChild(frm);

            container.AppendChild(p1);
            container.AppendChild(p2);
                     
            // Publish a root element to be displayed
            UI.Publish("/", container);
            DisplayOutput(WaitForOutput(machine, io));
        }

        private static string ProcessCommand(Machine machine, SimpleInputOutput io, string text)
        {
            io.Output = "";
            io.WaitingForInput = false;
            var p = new Paragraph {Text = string.Format(">{0}", text.Replace("\r\n", "<br/>"))};
            _area.AppendChild(p);
            machine.processText(text);
            return WaitForOutput(machine, io);
        }

        private static string WaitForOutput(Machine machine, SimpleInputOutput io)
        {
            while (!io.WaitingForInput && !machine.isFinished())
            {
                machine.processInstruction();
            }
            return io.Output;
        }

        private static void DisplayOutput(string s)
        {
            var stringSeparators = new[] { Environment.NewLine };
            var lines = s.Substring(0, s.Length - 1).Split(stringSeparators, StringSplitOptions.None);
            foreach (var line in lines)
            {
                var p = new Paragraph(line);
                _area.AppendChild(p);
            }
        }
    }
}
