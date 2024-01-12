﻿using NUnit.Framework;
using CSV_Parser_Demo.Models;
using NUnit.Framework.Legacy;
namespace CSV_Parser_Demo.Tests

{
    [TestFixture]
    public class CSVParserTest
    {
        [SetUp]
        public void SetUp() { }
        
        [Test]
        public void IsValid_SampleInput()
        {
            string sampleInput = File.ReadAllText("SampleInput.txt");
            CSVParser parser = new CSVParser();
            string output = parser.ParseToString(sampleInput);
            string expectedOutput = "[Patient Name][SSN][Age][Phone Number][Status]" +
                "[Prescott, Zeke][542 - 51 - 6641][21][801 - 555 - 2134][Opratory = 2, PCP = 1]" +
                "[Goldstein, Bucky][635 - 45 - 1254][42][435 - 555 - 1541][Opratory = 1, PCP = 1]" +
                "[Vox, Bono][414 - 45 - 1475][51][801 - 555 - 2100][Opratory = 3, PCP = 2]";

            ClassicAssert.AreEqual(output, expectedOutput);
        }


        [TearDown]
        public void TearDown() { }
    }
}
