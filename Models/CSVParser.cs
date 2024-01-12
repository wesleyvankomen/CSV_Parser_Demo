using System.Text;

namespace CSV_Parser_Demo.Models
{
    public class CSVParser
    {

        public CSVParser()
        {
            _options = new CSVParserOptions();
        }

        public CSVParser(CSVParserOptions options)
        {
            _options = options;
        }

        private CSVParserOptions _options;

        public string ParseToString(string inputText)
        {
            string[] lines = inputText.Split(_options.LineTerminators, StringSplitOptions.TrimEntries);
            List<string>[] parsedLines = new List<string>[lines.Length];

            for(int i = 0; i < lines.Length; i++)
            {
                parsedLines[i] = ParseLine(lines[i]);
            }

            return FormatOutput(parsedLines);
        }

        private List<string> ParseLine(string line)
        {
            Stack<char> escapeCharacterStack = new Stack<char>();
            int beginPos = 0;
            int stringLength = 0;
            List<string> result = new List<string>();

            for (int p = 0; p < line.Length; p++)
            {
                if (_options.EscapeCharacters.Contains(line[p]))
                {
                    if(escapeCharacterStack.Count > 0 && escapeCharacterStack.Peek() == line[p])
                    {
                        escapeCharacterStack.Pop();
                    }
                    else
                    {
                        escapeCharacterStack.Push(line[p]);
                    }
                }

                if (line[p].Equals(_options.Delimeter) && escapeCharacterStack.Count == 0)
                {
                    // Remove quotes and other escape characters around field
                    if (_options.EscapeCharacters.Contains(line[beginPos]) && _options.EscapeCharacters.Contains(line[beginPos + stringLength - 1]))
                    {
                        beginPos++;
                        stringLength -= 2;
                    }

                    result.Add(line.Substring(beginPos, stringLength));
                    beginPos = p + 1;
                    stringLength = -1;
                }

                stringLength++;
            }

            // Process final field on line
            if (_options.EscapeCharacters.Contains(line[beginPos]) && _options.EscapeCharacters.Contains(line[beginPos + stringLength - 1]))
            {
                beginPos++;
                stringLength -= 2;
            }
            result.Add(line.Substring(beginPos, stringLength));

            return result;
        }

        public string FormatOutput(IEnumerable<string>[] parsedLines)
        {
            StringBuilder output = new StringBuilder();

            foreach(List<string> line in parsedLines)
            {
                string[] formattedLine = line.Select(x => String.Concat("[", x, "]")).ToArray();
                output.Append(String.Join(" ", formattedLine) + "\n");
            }

            return output.ToString();
        }
    }
}
