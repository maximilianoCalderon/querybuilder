using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SqlKata
{
    public class CaseOption
    {
        public object When { get; set; }
        public object Then { get; set; }

        public CaseOption(string when, string then) {
            this.When = when;
            this.Then = then;
        }
    }
    public partial class Query
    {
        /*
                CASE @X 
                    WHEN 3 THEN 'ASDASD' 
                    WHEN 4 THEN 'ASDASD' 
                    WHEN 5 THEN 'ASDASD' 
                    WHEN 2 THEN 'SSS'
                END AS res
         */

        public Query Case(String parameter, params CaseOption[] options)
        {
            Method = "select";
            return this;
        }

    }
}
