using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AGOT
{
    public static class RuleTableParser
    {
        public static IEnumerable<RulesTable> ParseFile(string file)
        {
            string[] lines = file.Split('\n');// File.ReadAllLines(file);
            int c = 0;
            string line;

            while (c < lines.Count())
            {                
                line = lines[c].Trim();
                // "!---" marks start of dataset
                if (line == "!---")
                {                    
                    RulesTable rt = new RulesTable();
                    rt.Rows = new List<string>();
                    // next line is the name of table.
                    c += 1;
                    rt.Name = lines[c].Trim();
                    //next line contains the headers separated by '|'.
                    c += 1;
                    rt.Headers = lines[c].Trim().Split('|');
                    c += 1;
                    // "EndTable" marks end of actual table
                    while (lines[c].Trim() != "EndTable")
                    {
                        // get the rows of the table.
                        rt.Rows.Add(lines[c].Trim());
                        c += 1;
                    }
                    c += 1;
                    
                    // "---!" marks end of dataset
                    // between "Endtable" and "---!" may be additional text.
                    while (lines[c].Trim() != "---!")
                    {
                        rt.AdditionalText.Add(lines[c].Trim());
                        c += 1;
                    }
                        
                    yield return rt;
                }
                c += 1;
            }             
        }
    }
}
