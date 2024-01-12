using NUnit.Framework;
using CSV_Parser_Demo.Models;
using NUnit.Framework.Legacy;
namespace NUnit.Tests

{
    [TestFixture]
    public class CSVParserTest
    {
        [SetUp]
        public void SetUp() { }
        
        [Test]
        public void IsValid_SampleInput()
        {
            string sampleInput = File.ReadAllText("Tests/TextFiles/SampleInput.txt");
            CSVParser parser = new CSVParser();
            string output = parser.ParseToString(sampleInput);
            string expectedOutput = File.ReadAllText("Tests/TextFiles/SampleOutput.txt");

            ClassicAssert.AreEqual(expectedOutput, output);
        }

        [Test]
        public void IsValid_CommasInQuotes()
        {
            string input = File.ReadAllText("Tests/TextFiles/CommaSpaceInput.txt");
            CSVParser parser = new CSVParser();
            string output = parser.ParseToString(input);
            string expectedOutput = File.ReadAllText("Tests/TextFiles/CommaSpaceOutput.txt");

            ClassicAssert.AreEqual(expectedOutput, output);
        }


        [TearDown]
        public void TearDown() { }
    }
}
