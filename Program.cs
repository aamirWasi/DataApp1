using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Server=HP-9470M;Database=LearnDB;User Id=aamirrohan;Password = 123456; ";
            const string insertSql = "insert into employee values('jemy','female',5467,3)";
            const string deleteSql = "delete from employee where id=3;";
            const string updateSql = "update employee set departmentId =1 where id=7;";
            var query = "select * from employee";
            ExecuteSql(connectionString, updateSql);
            var data = GetData(connectionString, query);
            var data2 = GetDataUsingAdapter(connectionString, query);

        }
        static void ExecuteSql(string connectionString,string sql)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection?.Close();
                }
            }
        }


        static DataSet GetDataUsingAdapter(string connectionString, string sql)
        {
            var dataset = new DataSet("LearnDB");
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        //var dataTable = new DataTable("Employee");
                        //dataTable.Columns.Add(new DataColumn("Id"));
                        //dataTable.Columns.Add(new DataColumn("Name"));
                        //dataTable.Columns.Add(new DataColumn("Gender"));
                        //dataTable.Columns.Add(new DataColumn("Salary"));
                        //dataTable.Columns.Add(new DataColumn("departmentId"));

                        var adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataset, "Person");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection?.Close();
                }
                return dataset;
            }
        }

        static List<Dictionary<string, object>> GetData(string connectionString,string sql)
        {
            var allRows = new List<Dictionary<string, object>>();
            using(var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using(var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var row = new Dictionary<string, object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var fieldName = reader.GetName(i);
                                    var fieldValue = reader.GetValue(i);

                                    row[fieldName] = fieldValue;
                                }
                                allRows.Add(row);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection?.Close();
                }
                return allRows;
            }
        }
        //static List<Dictionary<string, object>> GetData(string connectionString, string sql)
        //{
        //    var allRows = new List<Dictionary<string, object>>();
        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            connection.Open();
        //            using (var command = new SqlCommand(sql, connection))
        //            {
        //                using(var reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        var row = new Dictionary<string, object>();
        //                        for (int i = 0; i < reader.FieldCount; i++)
        //                        {
        //                            var fieldName = reader.GetName(i);
        //                            var fieldValue = reader.GetValue(i);

        //                            row[fieldName] = fieldValue;
        //                        }
        //                        allRows.Add(row);
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }
        //        finally
        //        {
        //            connection?.Close();
        //        }
        //        return allRows;
        //    }
        //}
    }
}
