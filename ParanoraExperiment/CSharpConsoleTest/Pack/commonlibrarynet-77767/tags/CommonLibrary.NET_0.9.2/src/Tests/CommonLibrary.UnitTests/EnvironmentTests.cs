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
    public class EnvrionmentTests
    {
        private EnvironmentContext GetContext()
        {
            List<EnvItem> env = new List<EnvItem>()
            {
                new EnvItem(){ Name = "Dev", Source = "File", Path = "${rootfolder}/dev.config", DeepInherit = true, EnvType = EnvironmentType.Dev, InheritPath = "" },
                new EnvItem(){ Name = "Dev2", Source = "File", Path = "${rootfolder}/dev2.config", DeepInherit = true, EnvType = EnvironmentType.Dev, InheritPath = "" },
                new EnvItem(){ Name = "Qa", Source = "File", Path = "${rootfolder}/qa.config", DeepInherit = true, EnvType = EnvironmentType.Qa, InheritPath = "Dev" },
                new EnvItem(){ Name = "Prod", Source = "File", Path = "${rootfolder}/prod.config", DeepInherit = true, EnvType = EnvironmentType.Prod, InheritPath = "Qa" },
                new EnvItem(){ Name = "Custom", Source = "File", Path = "${rootfolder}/custom.config", DeepInherit = true, EnvType = EnvironmentType.MixedProd, InheritPath = "Prod,Dev2" }
            };
            Dictionary<string, EnvItem> envMap = new Dictionary<string, EnvItem>();
            env.ForEach<EnvItem>(e => envMap[e.Name] = e);

            EnvironmentContext ctx = new EnvironmentContext();
            ctx.Envs = env;
            ctx.Substitutions = new Dictionary<string, string>();
            ctx.Substitutions["rootfolder"] = "c:/apps/myapp/config";
            return ctx;
        }


        [Test]
        public void CanConfigureEnvironment()
        {
            EnvironmentContext ctx = GetContext();
            EnvironmentCurrent.Init(new EnvironmentDefinition(ctx, "Prod"));

            Assert.IsTrue(EnvironmentCurrent.IsProd);
            Assert.IsTrue(EnvironmentCurrent.Selected == ctx.EnvsByName["Prod"]);
            Assert.IsTrue(EnvironmentCurrent.Inheritance[0] == ctx.EnvsByName["Prod"]);
            Assert.IsTrue(EnvironmentCurrent.Inheritance[1] == ctx.EnvsByName["Qa"]);
            Assert.IsTrue(EnvironmentCurrent.Inheritance[2] == ctx.EnvsByName["Dev"]);
        }
    }
}
