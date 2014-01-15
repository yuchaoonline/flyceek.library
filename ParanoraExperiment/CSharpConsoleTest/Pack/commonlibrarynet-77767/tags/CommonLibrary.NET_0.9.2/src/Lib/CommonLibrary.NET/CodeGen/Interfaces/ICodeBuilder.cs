using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using CommonLibrary.Models;

namespace CommonLibrary.CodeGeneration
{
    public interface ICodeBuilder
    {
        BoolMessageItem<ModelContainer> Process(ModelContext ctx);
    }
}
