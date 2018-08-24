using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QR_Creator
{
    class Program
    {
        public void Run(string[] args)
        {
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }

            string qrPath = "";
            string csvPath = "";

            foreach (string s in args)
            {
                if (Directory.Exists(s))
                {
                    qrPath = s;
                }
                if (File.Exists(s))
                {
                    csvPath = s;
                }
            }

            if (qrPath.Length == 0 || csvPath.Length == 0)
            {
                ShowHelp();
                return;
            }

            List<string> lines = ReadDocument(csvPath);
            WriteDocument(lines, qrPath);
        }

        private static List<string> ReadDocument (string sourceFile)
        {
            List<string> lines = new List<string>();
            using (StreamReader sr = new StreamReader(sourceFile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines;
        }

        private static void WriteDocument (List<string> lines, string qrPath)
        {
            using (StreamWriter sw = new StreamWriter("QR_Merge.html"))
            {
                string header =
@"<html>
<style>
table, th, td {
    border: 1px solid black;
}
</style>
<body>
<h1 style=""text - align:right; "">Cloudera Galactic HQ - QR Bible - Rooms</h1>
<table style = ""width:100%"">
<tr>
	<th>QR Code</th>
    <th>Room Number</th>
    <th>Room Name</th> 
    <th>Floor</th>
    <th>Area Name</th>
    <th>Building Name</th> 
    <th>QR Code</th>
  </tr>
";
                sw.WriteLine(header);
                for (int i = 1; i < lines.Count; i++)
                {
                    sw.WriteLine("<tr>");
                    string[] parts = lines[i].Split(',');
                    if (i % 2 == 0)
                    {
                        sw.WriteLine("<td></td>");
                    }
                    else
                    {
                        sw.WriteLine($@"<td><img src=""{qrPath}/{parts[0]}.png"" witdh=""100"" height=""100""></td>");
                    }
                    sw.WriteLine($"<td>{parts[1]}</td>");
                    sw.WriteLine($"<td>{parts[2]}</td>");
                    sw.WriteLine($"<td>{parts[3]}</td>");
                    sw.WriteLine($"<td>{parts[4]}</td>");
                    sw.WriteLine($"<td>{parts[5]}</td>");
                    if (i % 2 == 1)
                    {
                        sw.WriteLine("<td></td>");
                    }
                    else
                    {
                        sw.WriteLine($@"<td><img src=""{qrPath}/{parts[0]}.png"" witdh=""100"" height=""100""></td>");
                    }
                    sw.WriteLine("</tr>");
                }
                string footer =
@"</table>
</body>
</html>";
                sw.WriteLine(footer);
            }
        }

        private void ShowHelp()
        {
            System.Console.WriteLine("Input the program name followed by the path to the folder containing images, and the CSV");
        }

        static void Main (string[] args)
        {
            Program prog = new Program();
            prog.Run(args);
        }
    }

    
}
