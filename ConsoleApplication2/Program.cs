using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {

            var efsw = new Stopwatch();
            var efbatch = new Stopwatch();

            var ct = new Context();

            EfPure(ct, efsw);
            EfBatch(ct, efsw);
            EfOff(ct, efsw);
            EfOffBatch(ct, efsw);
            PureSql(efsw);
            SqlSp(efsw);
            Console.ReadLine();
        }

        private static void SqlSp(Stopwatch efsw)
        {
            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

            using (var con = new SqlConnection(cs))
            using (var cmd = new SqlCommand("sp_Add_Item", con))
            {
                con.Open();
                efsw.Restart();
                var core = new SqlParameter()
                {
                    ParameterName = "@CoreID",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Direction = ParameterDirection.Input,
                    Value = Guid.Parse("{D65FE144-5DAD-446D-A139-890DE41C791B}")
                };
                cmd.Parameters.Add(core);

                var name = new SqlParameter()
                {
                    ParameterName = "@Name",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Value = "test"
                };
                cmd.Parameters.Add(name);
                var city = new SqlParameter()
                {
                    ParameterName = "@City",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Value = 1
                };
                cmd.Parameters.Add(city);
                for (var i = 0; i < 1000; i++)
                {
                    cmd.CommandType = CommandType.StoredProcedure;                                        
                    cmd.ExecuteNonQuery();
                }
                efsw.Stop();
                con.Close();
            }




            Console.WriteLine("Sql SP 1000 record time {0}", efsw.ElapsedMilliseconds);
        }

        private static void PureSql(Stopwatch efsw)
        {

            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            var connection = new SqlConnection(cs);
            var cmd = new SqlCommand();
            const string template = @"INSERT INTO [dbo].[Items]
           ([CoreID]
           ,[Name]
           ,[City])
     VALUES
           (
           'D65FE144-5DAD-446D-A139-890DE41C791B'
           ,'test'
           ,5)";
            cmd.CommandText = template;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connection;

            connection.Open();
            efsw.Restart();
            for (var i = 0; i < 1000; i++)
            {
                var result = cmd.ExecuteNonQuery();
            }

            efsw.Stop();

            connection.Close();
            Console.WriteLine("Pure sql 1000 record time {0}", efsw.ElapsedMilliseconds);
        }

        private static void EfOffBatch(Context ct, Stopwatch efsw)
        {
            var list = new List<Item>();
            for (var i = 0; i < 1000; i++)
            {
                var item = new Item()
                {
                    City = 1,
                    CoreID = Guid.NewGuid(),
                    Name = "more that one time"
                };

                list.Add(item);
            }
            ct.Configuration.AutoDetectChangesEnabled = false;
            efsw.Restart();
            ct.Items.AddRange(list);
            ct.SaveChanges();
            efsw.Stop();
            Console.WriteLine("Off detect batch EF 1000 record time {0}", efsw.ElapsedMilliseconds);
        }
        private static void EfOff(Context ct, Stopwatch efsw)
        {
            var list = new List<Item>();
            for (var i = 0; i < 1000; i++)
            {
                var item = new Item()
                {
                    City = 1,
                    CoreID = Guid.NewGuid(),
                    Name = "more that one time"
                };

                list.Add(item);
            }
            ct.Configuration.AutoDetectChangesEnabled = false;
            efsw.Restart();
            list.ForEach(item => ct.Items.Add(item));
            ct.SaveChanges();
            efsw.Stop();
            Console.WriteLine("Off detect EF 1000 record time {0}", efsw.ElapsedMilliseconds);
        }

        private static void EfPure(Context ct, Stopwatch efsw)
        {
            efsw.Start();
            for (var i = 0; i < 1000; i++)
            {
                var item = new Item()
                    {
                        City = 1,
                        CoreID = Guid.NewGuid(),
                        Name = "more that one time"
                    };
                ct.Items.Add(item);
                ct.SaveChanges();
            }
            efsw.Stop();
            Console.WriteLine("Pure EF 1000 record time {0}", efsw.ElapsedMilliseconds);
        }
        private static void EfBatch(Context ct, Stopwatch efsw)
        {

            var list = new List<Item>();


            for (var i = 0; i < 1000; i++)
            {
                var item = new Item()
                {
                    City = 1,
                    CoreID = Guid.NewGuid(),
                    Name = "more that one time"
                };

                list.Add(item);
            }
            efsw.Restart();
            ct.Items.AddRange(list);
            ct.SaveChanges();
            efsw.Stop();
            Console.WriteLine("Batch EF 1000 record time {0}", efsw.ElapsedMilliseconds);
        }
    }
}
