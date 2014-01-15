using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using ComLib;
using ComLib.Models;


namespace ComLib.CodeGeneration
{
    public class CodeBuilderSerialization : CodeBuilderBase, ICodeBuilder
    {
        #region ICodeBuilder Members
        /// <summary>
        /// Create the ORM mappings in xml file.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public BoolMessageItem<ModelContainer> Process(ModelContext ctx)
        {
            return null;
        }

        #endregion
    }
}
