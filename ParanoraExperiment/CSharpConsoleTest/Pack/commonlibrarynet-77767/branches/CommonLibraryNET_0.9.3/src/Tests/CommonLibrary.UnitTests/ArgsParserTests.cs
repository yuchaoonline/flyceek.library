using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Resources;
using NUnit.Framework;

using ComLib;
using ComLib.Arguments;


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
        public void IsHelp()
        {
            Args args = new Args(new string[]{ "-help"});
            Assert.IsTrue(args.IsHelp);
        }


        [Test]
        public void IsVersion()
        {
            Args args = new Args(new string[] { "-version" });
            Assert.IsTrue(args.IsVersion);
        }


        [Test]
        public void IsPause()
        {
            Args args = new Args(new string[] { "-pause" });
            Assert.IsTrue(args.IsPause);
        }


        [Test]
        public void IsInfo()
        {
            Args args = new Args(new string[] { "-about" });
            Assert.IsTrue(args.IsInfo);
        }

        [Test]
        public void CanParseText()
        {            
            Args args = Args.Parse("-config:prod.xml -businessDate:T-1 myApplicationId").Item;
            //Args.Parse(
            Assert.AreEqual(args.Named["config"], "prod.xml");
            Assert.AreEqual(args.Named["businessDate"], "T-1");
            Assert.AreEqual(args.Positional[0], "myApplicationId");
        }


        [Test]
        public void CanParseArgListWithNonDefaultNameValueIdentifiers()
        {
            string[] argList = new string[] { "@config=prod.xml", "@businessDate=T-1", "myApplicationId" };

            Args args = Args.Parse(argList, "@", "=", null).Item;

            Assert.AreEqual(args.Prefix, "@");
            Assert.AreEqual(args.Separator, "=");
            Assert.AreEqual(args.Named["config"], "prod.xml");
            Assert.AreEqual(args.Named["businessDate"], "T-1");
            Assert.AreEqual(args.Positional[0], "myApplicationId");
        }


        [Test]
        public void CanParseArgListWithSpaces()
        {
            string[] argList = new string[] { "@config=my prod.xml", "@businessDate=T-1", "myApplicationId" };

            Args args = Args.Parse(argList, "@", "=", null).Item;

            Assert.AreEqual(args.Prefix, "@");
            Assert.AreEqual(args.Separator, "=");
            Assert.AreEqual(args.Named["config"], "my prod.xml");
            Assert.AreEqual(args.Named["businessDate"], "T-1");
            Assert.AreEqual(args.Positional[0], "myApplicationId");
        }


        [Test]
        public void CanParseArgListWithOnlyKeysForBoolFlags()
        {
            string[] argList = new string[] { "-email", "-recurse", "-config:my prod.xml", "100" };

            Args args = Args.Parse(argList, "-", ":").Item;

            Assert.AreEqual(args.Prefix, "-");
            Assert.AreEqual(args.Separator, ":");
            Assert.AreEqual(args.Named["config"], "my prod.xml");
            Assert.AreEqual(args.Named["email"], "true");
            Assert.AreEqual(args.Named["recurse"], "true");
            Assert.AreEqual(args.Positional[0], "100");
        }


        [Test]
        public void CanParseTextWithNonDefaultNameValueIdentifiers()
        {
            Args args = Args.Parse("@config=prod.xml @businessDate=T-1 myApplicationId", "@", "=").Item;

            Assert.AreEqual(args.Prefix, "@");
            Assert.AreEqual(args.Separator, "=");
            Assert.AreEqual(args.Named["config"], "prod.xml");
            Assert.AreEqual(args.Named["businessDate"], "T-1");
            Assert.AreEqual(args.Positional[0], "myApplicationId");
        }


        [Test]
        public void CanParseTextWithQuotesWithNonDefaultNameValueIdentifiers()
        {
            Args args = Args.Parse("@config='prod.xml' @businessDate=T-1 'c:/program files/ccnet'", "@", "=").Item;

            Assert.AreEqual(args.Prefix, "@");
            Assert.AreEqual(args.Separator, "=");
            Assert.AreEqual(args.Named["config"], "'prod.xml'");
            Assert.AreEqual(args.Named["businessDate"], "T-1");
            Assert.AreEqual(args.Positional[0], "c:/program files/ccnet");
        }


        [Test]
        public void CanParseTextWithAttributes()
        {
            StartupArgs startupArgs = new StartupArgs();

            Args args = Args.Parse("-config:prod.xml -dateoffset:2 -busdate:${today} -dryrun:true", "-", ":", startupArgs).Item;

            Assert.AreEqual(args.Prefix, "-");
            Assert.AreEqual(args.Separator, ":");
            Assert.AreEqual(args.Named["config"], "prod.xml");
            Assert.AreEqual(args.Named["dateoffset"], "2");
            Assert.AreEqual(args.Named["dryrun"], "true");
            Assert.AreEqual(startupArgs.Config, "prod.xml");
            Assert.AreEqual(startupArgs.BusinessDateOffset, 2);
            Assert.AreEqual(startupArgs.BusinessDate, DateTime.Today);
            Assert.AreEqual(startupArgs.DryRun, true);
        }


        [Test]
        public void CanCatchErrorsViaAttributes()
        {
            StartupArgs startupArgs = new StartupArgs();

            BoolMessageItem<Args> result = Args.Parse("-busdate:abc -dryrun:test", "-", ":", startupArgs);
            Args args = result.Item;

            Assert.IsFalse(result.Success);
            Assert.IsNotEmpty(result.Message);           
        }


        [Test]
        public void CanParseUnNamedArgs()
        {
            StartupArgs startupArgs = new StartupArgs();

            BoolMessageItem<Args> result = Args.Parse("-config:Prod -dryrun:true ShowAll 8", "-", ":", startupArgs);
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
