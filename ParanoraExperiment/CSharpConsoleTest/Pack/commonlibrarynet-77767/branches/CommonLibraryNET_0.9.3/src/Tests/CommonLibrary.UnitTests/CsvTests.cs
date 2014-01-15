using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Resources;
using NUnit.Framework;

using ComLib;
using ComLib.CsvParse;

namespace CommonLibrary.Tests
{

    [TestFixture]
    public class CsvTests
    {

        [Test]
        public void CanParseCsv()
        {
            string text = "'Id', 'Name',   'Desc'" + Environment.NewLine
                        + "'0',  'Art',    'Art class'" + Environment.NewLine
                        + "'1',  'Sports', 'all sports'";

            CsvDoc doc = Csv.LoadText(text, true);

            Assert.IsTrue(doc.Columns.Contains("Id"));
            Assert.IsTrue(doc.Columns.Contains("Name"));
            Assert.IsTrue(doc.Columns.Contains("Desc"));            
            Assert.IsTrue(string.Compare((string)doc.Data[0]["Id"], "0") == 0);
            Assert.IsTrue(string.Compare((string)doc.Data[0]["Name"], "Art") == 0);
            Assert.IsTrue(string.Compare((string)doc.Data[0]["Desc"], "Art class") == 0);
            Assert.IsTrue(string.Compare((string)doc.Data[1]["Id"], "1") == 0);
            Assert.IsTrue(string.Compare((string)doc.Data[1]["Name"], "Sports") == 0);
            Assert.IsTrue(string.Compare((string)doc.Data[1]["Desc"], "all sports") == 0);
        }


        [Test]
        public void CanParseCsvAndGetTypedVal()
        {
            string text = "'Id', 'Name',   'Desc'" + Environment.NewLine
                        + "'0',  'Art',    'Art class'" + Environment.NewLine
                        + "'1',  'Sports', 'all sports'";

            CsvDoc doc = Csv.LoadText(text, true);

            Assert.IsTrue(doc.Columns.Contains("Id"));
            Assert.IsTrue(doc.Columns.Contains("Name"));
            Assert.IsTrue(doc.Columns.Contains("Desc"));
            Assert.AreEqual(doc.Get<string>(0, "Id"), "0");
            Assert.AreEqual(doc.Get<string>(0, "Name"), "Art");
            Assert.AreEqual(doc.Get<string>(0, "Desc"), "Art class");
            Assert.AreEqual(doc.Get<string>(1, "Id"), "1");
            Assert.AreEqual(doc.Get<string>(1, "Name"), "Sports");
            Assert.AreEqual(doc.Get<string>(1, "Desc"), "all sports");
        }


        [Test]
        public void CanWriteCsv()
        {
            string text = "'Id', 'Name',   'Desc'" + Environment.NewLine
                        + "'0',  'Art',    'Art class'" + Environment.NewLine
                        + "'1',  'Sports', 'all sports'";
            CsvDoc doc = Csv.LoadText(text, true);
            doc.Write(@"F:\temp\test.csv", ",");
        }
    }
}
