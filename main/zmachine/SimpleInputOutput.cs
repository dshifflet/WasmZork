namespace zmachine
{
    public class SimpleInputOutput : IZmachineInputOutput
    {
        public bool WaitingForInput { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }

        public void Type(string s)
        {
            Input = s;
        }

        public string ReadLine()
        {
            WaitingForInput = true;
            return null;
        }

        public void Write(string str)
        {
            Output += str;
        }

        public void WriteLine(string str)
        {
            Output += str;
        }
    }

}