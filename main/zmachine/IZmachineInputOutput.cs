using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zmachine
{
    public interface IZmachineInputOutput
    {
        string ReadLine();
        void Write(String str);
        void WriteLine(String str);
    }
}
