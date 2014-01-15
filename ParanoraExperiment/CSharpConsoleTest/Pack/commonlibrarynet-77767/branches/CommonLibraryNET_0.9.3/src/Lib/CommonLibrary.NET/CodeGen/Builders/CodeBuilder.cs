using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComLib;
using ComLib.Models;


namespace ComLib.CodeGeneration
{
    public class CodeBuilder
    {
        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static BoolMessageItem<ModelContainer> Process(ModelContext ctx)
        {
            IList<ICodeBuilder> builders = new List<ICodeBuilder>()
            {
                new CodeBuilderDb(ctx.AllModels.Settings.Connection),
                new CodeBuilderORMHibernate(),
                new CodeBuilderDomain()
            };
            return Process(ctx, builders);
        }


        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static BoolMessageItem<ModelContainer> Process(ModelContext ctx, IList<ICodeBuilder> builders)
        {
            foreach (ICodeBuilder builder in builders)
            {
                builder.Process(ctx);
            }
            return new BoolMessageItem<ModelContainer>(null, false, string.Empty);
        }
    }
}
