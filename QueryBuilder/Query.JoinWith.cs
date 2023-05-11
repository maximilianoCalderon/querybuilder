using System;

namespace SqlKata
{
    public partial class Query
    {

        private Query JoinWith(Func<Join, Join> callback)
        {
            var join = callback.Invoke(new Join().AsInner());

            return AddComponent("join", new BaseJoin
            {
                Join = join
            });
        }

        private Query JoinWith(Func<Join, Join> callback, bool nolock, string index)
        {
            var join = callback.Invoke(new Join().AsInner().With(nolock, index));

            return AddComponent("join", new BaseJoin
            {
                Join = join
            });
        }

        public Query JoinWith(
            string table,
            string first,
            string second,
            bool nolock = true,
            string index = "",
            string op = "=",
            string type = "inner join"
        )
        {
            return Join(j => j.JoinWith(table).With(nolock, index).WhereColumns(first, op, second).AsType(type));
        }

        public Query JoinWith(string table, Func<Join, Join> callback, string type = "inner join", bool nolock = true,
            string index = "")
        {
            return Join(j => j.JoinWith(table).With(nolock, index).Where(callback).AsType(type));
        }

        public Query JoinWith(Query query, Func<Join, Join> onCallback, string type = "inner join", bool nolock = true,
            string index = "")
        {
            return Join(j => j.JoinWith(query).With(nolock, index).Where(onCallback).AsType(type));
        }

        public Query LeftJoinWith(string table, string first, string second, string op = "=", bool nolock = true,
            string index = "")
        {
            return JoinWith(table, first, second, nolock, index, op, "left join");
        }

        public Query LeftJoinWith(string table, Func<Join, Join> callback, bool nolock = true,
            string index = "")
        {
            return JoinWith(table, callback, "left join", nolock, index);
        }

        public Query LeftJoinWith(Query query, Func<Join, Join> onCallback, bool nolock = true,
            string index = "")
        {
            return JoinWith(query, onCallback, "left join", nolock, index);
        }

        public Query RightJoinWith(string table, string first, string second, string op = "=", bool nolock = true,
            string index = "")
        {
            return JoinWith(table, first, second, nolock, index, op, "right join");
        }

        public Query RightJoinWith(string table, Func<Join, Join> callback, bool nolock = true, string index = "")
        {
            return JoinWith(table, callback, "right join", nolock, index);
        }

        public Query RightJoinWith(Query query, Func<Join, Join> onCallback, bool nolock = true, string index = "")
        {
            return JoinWith(query, onCallback, "right join", nolock, index);
        }

        public Query CrossJoinWith(string table, bool nolock = true,
            string index = "")
        {
            return JoinWith(j => j.JoinWith(table).AsCross(), nolock, index);
        }

    }
}
