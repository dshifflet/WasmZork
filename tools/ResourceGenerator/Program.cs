using System;
using System.IO;

namespace ResourceGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("<input file> <output file>");
                return;
            }
            var input = new FileInfo(args[0]);
            if (!input.Exists)
            {
                Console.WriteLine("Input file does not exist");
                return;
            }
            var output = new FileInfo(args[1]);
            if (output.Exists)
            {
                Console.WriteLine("Output file already exists");
                return;
            }
            Generate(input, output);
        }

        static void Generate(FileInfo input, FileInfo output)
        {
            var bytes = File.ReadAllBytes(input.FullName);

            using (var stream = output.Create())
            using (var sw = new StreamWriter(stream))
            {
                sw.WriteLine("");
                sw.WriteLine("namespace TodoNamespace");
                sw.WriteLine("{");
                sw.WriteLine("\tpublic static class {0}", output.Name.Replace(".cs", ""));
                sw.WriteLine("\t{");
                sw.WriteLine("\t\tpublic static byte[] GetData()");
                sw.WriteLine("\t\t{");
                sw.Write("\t\t\treturn new byte[] {");
                var cnt = 0;
                foreach (var item in bytes)
                {
                    if (cnt == 16)
                    {
                        cnt = 0;
                        sw.Write(Environment.NewLine);
                        sw.Write("\t\t\t\t");
                    }
                    sw.Write("{0},", (int) item);
                    cnt++;
                }
                sw.Write("};" + Environment.NewLine);
                sw.WriteLine("\t\t}");
                sw.WriteLine("\t}");
                sw.WriteLine("}");
            }
        }
    }
}
