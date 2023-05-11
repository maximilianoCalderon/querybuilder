using System;
using System.Collections.Generic;

namespace SqlKata
{
    public class CaseOption
    {
        public object When { get; set; }
        public object Then { get; set; }

        public CaseOption(string when, string then)
        {
            this.When = when;
            this.Then = then;
        }
    }

    public abstract class AbstractCase : AbstractClause
    {
    }

    /// <summary>
    /// Represents a "WITH" clause.
    /// </summary>
    public class CaseClause : AbstractWith
    {
        //public string Table { get; set; }
        public string Parameter { get; set; }
        public IEnumerable<CaseOption> Options { get; set; }

        /// <inheritdoc />
        public override AbstractClause Clone()
        {
            return new CaseClause
            {
                Engine = Engine,
                //Alias = Alias,
                //Table = Table,
                Component = Component,
                Parameter = Parameter,
                Options = Options
            };
        }
    }

    /// <summary>
    /// Represents a "from subquery" clause.
    /// </summary>
    public class QueryCaseClause : AbstractCase
    {
        public Query Query { get; set; }

        /// <inheritdoc />
        public override AbstractClause Clone()
        {
            return new QueryCaseClause
            {
                Engine = Engine,
                Query = Query.Clone(),
                Component = Component,
            };
        }
    }

    public class RawCaseClause : AbstractCase
    {
        public string Expression { get; set; }
        public object[] Bindings { set; get; }

        /// <inheritdoc />
        public override AbstractClause Clone()
        {
            return new RawWithClause
            {
                Engine = Engine,
                Expression = Expression,
                Bindings = Bindings,
                Component = Component,
            };
        }
    }

}
