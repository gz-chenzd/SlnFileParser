using System;
using System.IO;
using System.Text.RegularExpressions;

namespace SlnFileParser
{
    class Program
    {
        const string EXPR = "^\\s*Project\\(\\s*\\\"(?<sln_id>[^\\\"]+)\\\"\\s*\\)\\s*=\\s*\\\"(?<proj_name>[^\\\"]+)\\\",\\s*\\\"(?<proj_path>[^\\\"]+)\\\",\\s*\\\"(?<proj_id>[^\\\"]+)\\\"\\s*";

        static int Main(string[] args)
        {
            if (args == null || args.Length != 1)
            {
                Console.WriteLine("Usage: slnparser <solution-file>");
                return 1;
            }

            using (StreamReader reader = new StreamReader(args[0]))
            {
                Regex re = new Regex(EXPR, RegexOptions.Compiled);

                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    { break; }

                    var match = re.Match(line);
                    if (match != null && match.Success)
                    {
                        Console.WriteLine(Path.GetDirectoryName(match.Groups["proj_path"].Value));
                    }
                }
            }
            
            return 0;
        }
    }
}
