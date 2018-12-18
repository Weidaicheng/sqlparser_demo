using System;
using System.IO;
using System.Linq;
using Microsoft.SqlServer.Management.SqlParser.Parser;

namespace sqlparser_demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var sqlFiles = Directory.GetFiles("")
                .Where(f => f.EndsWith(".sql"));
            foreach (var file in sqlFiles)
            {
                using (var fs = File.Open(file, FileMode.Open))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        var sql = sr.ReadToEnd();
                        var result = Parser.Parse(sql);
                        if (result.Errors.Count() > 0)
                            Console.WriteLine($"Errors in file '{file}':");
                        else
                            Console.WriteLine($"No error in file: '{file}'.");
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"\t{error.Message}");
                        }

                        sr.Close();
                    }

                    fs.Close();
                }
            }
        }
    }
}
