using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using ComLib.Database;


namespace ComLib.Entities
{
    public abstract class EntityRowMapper<T> : RowMapperReaderBased<T>, IEntityRowMapper<T>
    {   
    }
}
