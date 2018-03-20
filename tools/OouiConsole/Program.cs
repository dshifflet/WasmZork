using System;
using System.Xml;
using Ooui;
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

        private static string _lastText;
        static void Main(string[] args)
        {
            //Start Game
            var io = new SimpleInputOutput();
            var machine = new Machine(Zork1.GetData(), io);

            _area = new Span();
            //_area = new TextArea("TEXT AREA HERE");
            //_area.Rows = 2;
            //_area.Columns = 8;

            var frm = new Form();

            var inputText = new TextInput();
            inputText.Value = "";
            _area.Text = "";
            frm.Submit += (sender, eventArgs) =>
            {
                //_area.Text += string.Format("{0}\r\n", inputText.Value);
                ProcessCommand(machine, io, inputText.Value);
                inputText.Value = "";
                inputText.Name = "";
            };

            inputText.KeyPress += (sender, eventArgs) =>
            {                
            };

            var container = new Paragraph();
            var p1 = new Paragraph();
            var p2 = new Paragraph();
            var p3 = new Paragraph();

            p1.AppendChild(_area);
            frm.AppendChild(inputText);
            p3.AppendChild(frm);

            container.AppendChild(p1);
            container.AppendChild(p3);
            container.AppendChild(p2);
            // Publish a root element to be displayed
            UI.Publish("/", container);

            WaitForOutput(machine, io);
        }

        private static string ProcessCommand(Machine machine, SimpleInputOutput io, string text)
        {
            io.Output = "";
            io.WaitingForInput = false;
            //_area.Text += string.Format(">{0}", text.Replace("\r\n", "<br/>"));
            var p = new Paragraph();
            p.Text = string.Format(">{0}", text.Replace("\r\n", "<br/>"));
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

            string[] stringSeparators = new string[] { Environment.NewLine };
            string[] lines = io.Output.Substring(0, io.Output.Length - 1).Split(stringSeparators, StringSplitOptions.None);
            foreach (var line in lines)
            {
                var p = new Paragraph(line);
                _area.AppendChild(p);
            }
            return io.Output;
        }
    }
}
