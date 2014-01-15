using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Resources;
using NUnit.Framework;

using CommonLibrary;


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

            CsvDocument doc = new CsvDocument(text, false);
            doc.ParseDict();

            Assert.IsTrue(doc.Columns.Contains("Id"));
            Assert.IsTrue(doc.Columns.Contains("Name"));
            Assert.IsTrue(doc.Columns.Contains("Desc"));
            Assert.IsTrue(doc.RecordsMap[0]["Id"] == "0");
            Assert.IsTrue(doc.RecordsMap[0]["Name"] == "Art");
            Assert.IsTrue(doc.RecordsMap[0]["Desc"] == "Art class");
            Assert.IsTrue(doc.RecordsMap[1]["Id"] == "1");
            Assert.IsTrue(doc.RecordsMap[1]["Name"] == "Sports");
            Assert.IsTrue(doc.RecordsMap[1]["Desc"] == "all sports");
        }
    }
}
