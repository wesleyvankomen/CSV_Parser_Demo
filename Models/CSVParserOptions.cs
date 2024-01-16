namespace CSV_Parser_Demo.Models
{
    public class CSVParserOptions
    {
        public CSVParserOptions() 
        {
            Delimeter = ',';
            LineTerminators = new string[] { "\r\n", "\n", "\r" };
            EscapeCharacters = new char[] { '\"' };
        }

        public char Delimeter {get; set;}
        public string[] LineTerminators { get; set; }
        public char[] EscapeCharacters { get; set; }
    }
}
