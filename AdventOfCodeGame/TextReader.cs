using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeGame
{
    public class TextReader
    {
        public TextReader()
        {
        }

        public List<string> ReadText(string inputPath)
        {
            string path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName, @inputPath);

            string[] Input = File.ReadAllLines(path);

            List<string> allData = new List<string>();

            foreach (string s in Input)
            {
                allData.Add(s);
            }

            return allData;
        }
    }
}
