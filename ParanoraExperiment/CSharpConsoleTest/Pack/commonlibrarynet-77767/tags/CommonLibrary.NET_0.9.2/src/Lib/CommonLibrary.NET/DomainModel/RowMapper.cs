using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CommonLibrary;


namespace CommonLibrary.DomainModel
{
    public abstract class EntityRowMapper<T> : RowMapperReaderBased<T>, IEntityRowMapper<T>
    {   
    }
}
