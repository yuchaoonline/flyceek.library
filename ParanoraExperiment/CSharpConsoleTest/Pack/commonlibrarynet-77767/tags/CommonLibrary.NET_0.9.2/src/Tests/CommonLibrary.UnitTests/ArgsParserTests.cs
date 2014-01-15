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
    public class StartupArgs
    {
        [Arg("config", "config file for environment", typeof(string), true, "", "dev.xml")]
        public string Config { get; set; }


        [Arg("dateoffset", "business date offset from today", typeof(int), true, 0, "0, 1, -1")]
        public int BusinessDateOffset { get; set; }


        [Arg("busdate", "The business date", typeof(int), true, "${today}", "${today}", true, false, false)]
        public DateTime BusinessDate { get; set; }


        [Arg("dryrun", "readonly mode", typeof(bool), false, false, "true|false")]
        public bool DryRun { get; set; }

        
        [Arg(1, "Number of categories to display", typeof(int), true, 1, "1|2|3 etc.")]
        public int CategoriesToDisplay { get; set; }


        [Arg(0, "settings id to load on startup", typeof(string), true, "settings_01", "settings_01")]
        public string DefaultSettingsId { get; set; }
    }




    [TestFixture]
    public class ArgsParserTests
    {

        [Test]
        public void CanParseText()
        {
            Args args = ArgsParser.Parse("-config:prod.xml -businessDate:T-1 myApplicationId").Item;
            
            Assert.AreEqual(args.NamedArgs["config"], "prod.xml");
            Assert.AreEqual(args.NamedArgs["businessDate"], "T-1");
            Assert.AreEqual(args.ArgsList[0], "myApplicationId");
        }


        [Test]
        public void CanParseArgListWithNonDefaultNameValueIdentifiers()
        {
            string[] argList = new string[] { "@config=prod.xml", "@businessDate=T-1", "myApplicationId" };

            Args args = ArgsParser.Parse(argList, "@", "=", null).Item;

            Assert.AreEqual(args.NamedArgs["config"], "prod.xml");
            Assert.AreEqual(args.NamedArgs["businessDate"], "T-1");
            Assert.AreEqual(args.ArgsList[0], "myApplicationId");
        }


        [Test]
        public void CanParseArgListWithSpaces()
        {
            string[] argList = new string[] { "@config=my prod.xml", "@businessDate=T-1", "myApplicationId" };

            Args args = ArgsParser.Parse(argList, "@", "=", null).Item;

            Assert.AreEqual(args.NamedArgs["config"], "my prod.xml");
            Assert.AreEqual(args.NamedArgs["businessDate"], "T-1");
            Assert.AreEqual(args.ArgsList[0], "myApplicationId");
        }


        [Test]
        public void CanParseTextWithNonDefaultNameValueIdentifiers()
        {
            Args args = ArgsParser.Parse("@config=prod.xml @businessDate=T-1 myApplicationId", "@", "=").Item;

            Assert.AreEqual(args.NamedArgs["config"], "prod.xml");
            Assert.AreEqual(args.NamedArgs["businessDate"], "T-1");
            Assert.AreEqual(args.ArgsList[0], "myApplicationId");
        }


        [Test]
        public void CanParseTextWithQuotesWithNonDefaultNameValueIdentifiers()
        {
            Args args = ArgsParser.Parse("@config='prod.xml' @businessDate=T-1 'c:/program files/ccnet'", "@", "=").Item;

            Assert.AreEqual(args.NamedArgs["config"], "'prod.xml'");
            Assert.AreEqual(args.NamedArgs["businessDate"], "T-1");
            Assert.AreEqual(args.ArgsList[0], "c:/program files/ccnet");
        }


        [Test]
        public void CanParseTextWithAttributes()
        {
            StartupArgs startupArgs = new StartupArgs();

            Args args = ArgsParser.Parse("-config:prod.xml -dateoffset:2 -busdate:${today} -dryrun:true", "-", ":", startupArgs).Item;

            Assert.AreEqual(args.NamedArgs["config"], "prod.xml");
            Assert.AreEqual(args.NamedArgs["dateoffset"], "2");
            Assert.AreEqual(args.NamedArgs["dryrun"], "true");
            Assert.AreEqual(startupArgs.Config, "prod.xml");
            Assert.AreEqual(startupArgs.BusinessDateOffset, 2);
            Assert.AreEqual(startupArgs.BusinessDate, DateTime.Today);
            Assert.AreEqual(startupArgs.DryRun, true);
        }


        [Test]
        public void CanCatchErrorsViaAttributes()
        {
            StartupArgs startupArgs = new StartupArgs();

            BoolMessageItem<Args> result = ArgsParser.Parse("-busdate:abc -dryrun:test", "-", ":", startupArgs);
            Args args = result.Item;

            Assert.IsFalse(result.Success);
            Assert.IsNotEmpty(result.Message);           
        }


        [Test]
        public void CanParseUnNamedArgs()
        {
            StartupArgs startupArgs = new StartupArgs();

            BoolMessageItem<Args> result = ArgsParser.Parse("-config:Prod -dryrun:true ShowAll 8", "-", ":", startupArgs);
            Args args = result.Item;

            Assert.IsFalse(result.Success);
            Assert.IsNotEmpty(result.Message);
            Assert.AreEqual(startupArgs.Config, "Prod");
            Assert.AreEqual(startupArgs.DryRun, true);
            Assert.AreEqual(startupArgs.DefaultSettingsId, "ShowAll");
            Assert.AreEqual(startupArgs.CategoriesToDisplay, 8);
        }
    }
}
