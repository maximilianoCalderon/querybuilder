using System;
using System.Collections.Generic;

namespace SqlKata
{
    public abstract class AbstractWith : AbstractClause
    {
    }

    /// <summary>
    /// Represents a "WITH" clause.
    /// </summary>
    public class WithClause : AbstractWith
    {
        //public string Table { get; set; }
        public bool NoLock { get; set; }
        public string Index { get; set; }

        /// <inheritdoc />
        public override AbstractClause Clone()
        {
            return new WithClause
            {
                Engine = Engine,
                //Alias = Alias,
                //Table = Table,
                Component = Component,
                NoLock = NoLock,
                Index = Index
            };
        }
    }

    /// <summary>
    /// Represents a "from subquery" clause.
    /// </summary>
    public class QueryWithClause : AbstractWith
    {
        public Query Query { get; set; }

        //public override string Alias
        //{
        //    get
        //    {
        //        return string.IsNullOrEmpty(_alias) ? Query.QueryAlias : _alias;
        //    }
        //}

        /// <inheritdoc />
        public override AbstractClause Clone()
        {
            return new QueryWithClause
            {
                Engine = Engine,
                //Alias = Alias,
                Query = Query.Clone(),
                Component = Component,
            };
        }
    }

    public class RawWithClause : AbstractWith
    {
        public string Expression { get; set; }
        public object[] Bindings { set; get; }

        /// <inheritdoc />
        public override AbstractClause Clone()
        {
            return new RawWithClause
            {
                Engine = Engine,
                //Alias = Alias,
                Expression = Expression,
                Bindings = Bindings,
                Component = Component,
            };
        }
    }

}
