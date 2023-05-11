using System;
using System.Collections.Generic;
using System.Text;

namespace SqlKata.Clauses
{
    public enum TableClause
    {
        FROM = 1,
        WITH = 2,
        WHERE = 3,
        GROUP_BY = 4,
        ORDER_BY = 5,
        INDEX = 6
    }
}
