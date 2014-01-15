using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using ComLib;
using ComLib.Types;
using ComLib.Collections;


namespace CommonLibrary.Tests
{
    [TestFixture]
    public class StringDelimitedTests
    {
        [Test]
        public void CanParseKeyValuePairs()
        {
            string test = "city=Queens, state=Ny, zipcode=12345, Country=usa";
            IDictionary<string, string> pairs = StringHelpers.ToMap(test, ',', '=', false, true);

            Assert.AreEqual(pairs["city"], "Queens");
            Assert.AreEqual(pairs["state"], "Ny");
            Assert.AreEqual(pairs["zipcode"], "12345");
            Assert.AreEqual(pairs["Country"], "usa");
        }
    }



    [TestFixture]
    public class StringHelperTests
    {
        [Test]
        public void CanParseDelimitedData()
        {
            string[] pageNums = StringHelpers.GetDelimitedChars("search-classes-workshops-4-1-2-6",
                "search-classes-workshops-", '-');
            Assert.AreEqual(4, pageNums.Length);
            Assert.AreEqual("4", pageNums[0]);
            Assert.AreEqual("1", pageNums[1]);
            Assert.AreEqual("2", pageNums[2]);
            Assert.AreEqual("6", pageNums[3]);
        }


        [Test]
        public void CanConvertSingleWordToSentenceCase()
        {
            string lowerCase = "newyork";
            string sentenceCase = StringHelpers.ConvertToSentanceCase(lowerCase, ' ');

            Assert.AreEqual("Newyork", sentenceCase);
        }


        [Test]
        public void CanConvertMultipleWordsToSentenceCase()
        {
            string lowerCase = "AMERICAN SAMOA";
            string sentenceCase = StringHelpers.ConvertToSentanceCase(lowerCase, ' ');

            Assert.AreEqual("American Samoa", sentenceCase);
        }


        [Test]
        public void CanTrucateNullOrEmpty()
        {
            string txt = null;
            string t = StringHelpers.Truncate(txt, 10);
            Assert.AreEqual(null, t);

            txt = string.Empty;
            string t2 = StringHelpers.Truncate(txt, 10);
            Assert.AreEqual(string.Empty, t2);
        }


        [Test]
        public void CanTrucate()
        {
            string txt = "1234567890";
            string t = StringHelpers.Truncate(txt, 5);
            Assert.AreEqual("12345", t);

            txt = "1234567890";
            string t2 = StringHelpers.Truncate(txt, 15);
            Assert.AreEqual("1234567890", t2);
        }
    }


    [TestFixture]
    public class ParsableCollectionTests
    {
        [Test]
        public void CanParse()
        {
            ParsableCollection coll = new ParsableCollection(';');
            coll.DelimitedValues = "kishore;reddy;sush;boadi";

            Assert.IsTrue(coll.Contains("kishore"));
            Assert.IsTrue(coll.Contains("reddy"));
            Assert.IsTrue(coll.Contains("sush"));
            Assert.IsTrue(coll.Contains("boadi"));

            Assert.IsFalse(coll.Contains("starbucks"));
        }
    }



    [TestFixture]
    public class TextSplitterTests
    {
        [Test]
        public void CanSplitSingleLongWord()
        {
            string longWord = "abcdefghijklmnopqrstuvwxyz";
            string splitWord = TextSplitter.SplitWord(longWord, 5, " ");

            Assert.AreEqual("abcde fghij klmno pqrst uvwxy z", splitWord);
        }


        [Test]
        public void DoesNotSplitSingleShortWord()
        {
            string longWord = "abcdefghijklmn";
            string splitWord = TextSplitter.SplitWord(longWord, 15, " ");
            Assert.AreEqual("abcdefghijklmn", splitWord);

            longWord = null;
            splitWord = TextSplitter.SplitWord(longWord, 15, " ");
            Assert.IsNull(splitWord);

            longWord = string.Empty;
            splitWord = TextSplitter.SplitWord(longWord, 15, " ");
            Assert.AreEqual(string.Empty, splitWord);

        }


        [Test]
        public void CanCheckSingleLinWithSplitting()
        {
            string text = "kishore 12345678901234567890 reddy";
            string splitText = TextSplitter.CheckAndSplitText(text, 15);

            Assert.AreEqual("kishore 123456789012345 67890 reddy", splitText);
        }


        [Test]
        public void CanCheckSingleLineWithoutSplitting()
        {
            string text = "kishore 1234567890 1234567890 reddy";
            string splitText = TextSplitter.CheckAndSplitText(text, 15);

            Assert.AreEqual("kishore 1234567890 1234567890 reddy", splitText);
        }


        [Test]
        public void CanCheckMultiLineWithOutSplitting()
        {
            string text = "Famed archaeologist/adventurer Dr. Henry \"Indiana\" Jones" + Environment.NewLine
                        + "is called back into action when he becomes entangled in a " + Environment.NewLine
                        + "Soviet plot to uncover the secret behind mysterious artifacts" + Environment.NewLine
                        + "known as the Crystal Skulls";


            string splitText = TextSplitter.CheckAndSplitText(text, 25);

            Assert.AreEqual(text, splitText);
        }


        [Test]
        public void CanCheckMultipleWithSplitting()
        {
            string text = "Famed archaeologist/adventurer123 Dr. Henry \"Indiana\" Jones" + Environment.NewLine
                        + "is called back into action when he becomes entangled in a " + Environment.NewLine
                        + "Soviet plot to uncoverTheSecretBehindMysteriousArtifacts" + Environment.NewLine
                        + "known as the Crystal Skulls";

            string splitText = TextSplitter.CheckAndSplitText(text, 25);
            string expected = "Famed archaeologist/adventurer1 23 Dr. Henry \"Indiana\" Jones" + Environment.NewLine
                        + "is called back into action when he becomes entangled in a " + Environment.NewLine
                        + "Soviet plot to uncoverTheSecretBehindMys teriousArtifacts" + Environment.NewLine
                        + "known as the Crystal Skulls";
            Assert.AreEqual(expected, splitText);
        }
    }    
}
