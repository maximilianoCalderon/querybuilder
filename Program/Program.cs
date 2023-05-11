using System;
using System.Collections.Generic;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using Npgsql;
using System.Data;
using Dapper;
using System.Data.SQLite;
using static SqlKata.Expressions;
using System.IO;

namespace Program
{
    class Program
    {
        private class Loan
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public List<Installment> Installments { get; set; } = new List<Installment>();
        }

        private class Installment
        {
            public string Id { get; set; }
            public string LoanId { get; set; }
            public int DaysCount { get; set; }
        }

        static List<string> ShowMethods(Type type)
        {
            List<string> result = new List<string>();
            foreach (var method in type.GetMethods())
            {
                if (!result.Exists(x => x == method.Name))
                {
                    result.Add(method.Name);
                }
            }
            return result;
        }

        static void Main(string[] args)
        {
            var connection = new SqlConnection("Data Source=MyDb;User Id=User;Password=TopSecret");
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(connection, compiler);

            //var query = db.Query("tbFacturaEnc").With("tbFacturaEnc", true, "IX_tbFacturaEnc");
            //Console.WriteLine(db.Compiler.Compile(query).Sql);
            //query = db.Query("tbFacturaEnc").With("tbFacturaEnc", true);
            //Console.WriteLine(db.Compiler.Compile(query).Sql);
            var query1 = db.Query("tbFacturaEnc")
                .With(true)
                .LeftJoinWith("tbFacturaDet", "tbFacturaDet.intFactura", "tbFacturaEnc.intFactura");

            var query = db.Query("tbFacturaEnc")
                .With(true)
                .Where(p => p.Where("asd", ""))
                .Select("",)
                .Join("tbFacturaDet", "tbFacturaDet.intFactura", "tbFacturaEnc.intFactura");

            //var methods = ShowMethods(typeof(Query));



            Console.WriteLine(db.Compiler.Compile(query1).Sql);
            Console.WriteLine("-------------------------------------------------------------");

            Console.WriteLine(db.Compiler.Compile(query).Sql);

            //using (var db = SqlLiteQueryFactory())
            //{
            //    var query = db.Query("accounts")
            //        .With("NO LOCK")
            //    .Limit(10);

            //    Console.WriteLine(db.Compiler.Compile(query).Sql);
            //    //var accounts = query.Clone().Get();
            //    //Console.WriteLine(JsonConvert.SerializeObject(accounts, Formatting.Indented));

            //    //var exists = query.Clone().Exists();
            //    //Console.WriteLine(exists);
            //}
        }

        private static void log(Compiler compiler, Query query)
        {
            var compiled = compiler.Compile(query);
            Console.WriteLine(compiled.ToString());
            Console.WriteLine(JsonConvert.SerializeObject(compiled.Bindings));
        }

        private static QueryFactory SqlLiteQueryFactory()
        {
            var compiler = new SqliteCompiler();

            var connection = new SQLiteConnection("Data Source=Demo.db");

            var db = new QueryFactory(connection, compiler);

            db.Logger = result =>
            {
                Console.WriteLine(result.ToString());
            };

            if (!File.Exists("Demo.db"))
            {
                Console.WriteLine("db not exists creating db");

                SQLiteConnection.CreateFile("Demo.db");

                db.Statement("create table accounts(id integer primary key autoincrement, name varchar, currency_id varchar, balance decimal, created_at datetime);");
                for (var i = 0; i < 10; i++)
                {
                    db.Statement("insert into accounts(name, currency_id, balance, created_at) values(@name, @currency, @balance, @date)", new
                    {
                        name = $"Account {i}",
                        currency = "USD",
                        balance = 100 * i * 1.1,
                        date = DateTime.UtcNow,
                    });
                }

            }

            return db;

        }

        private static QueryFactory SqlServerQueryFactory()
        {
            var compiler = new PostgresCompiler();
            var connection = new SqlConnection(
               "Server=tcp:localhost,1433;Initial Catalog=Lite;User ID=sa;Password=P@ssw0rd"
           );

            var db = new QueryFactory(connection, compiler);

            db.Logger = result =>
            {
                Console.WriteLine(result.ToString());
            };

            return db;
        }

    }
}
