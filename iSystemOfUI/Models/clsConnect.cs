using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace iSystemOfUI.Models
{
    public class clsConnect
    {
        string connectionstring;
        public SqlConnection cnn;
        SqlCommand cmd;
        SqlDataAdapter adt;
        public clsConnect()
        {
                connectionstring = ConfigurationManager.ConnectionStrings["dbNamHaiContrucsion"].ConnectionString;
                cnn = new SqlConnection(connectionstring);
                cnn.Open();
        }

        public DataTable SelectTop(string table, int top)
        {
            string query = @"SELECT TOP (" + top + ") * FROM " + table.Trim();
            DataTable tbl = new DataTable();
            adt = new SqlDataAdapter(query, cnn);
            adt.Fill(tbl);
            return tbl;
        }
        public DataTable SelectTop(string table, string column, string parameter, int top)
        {
            string query = @"SELECT TOP (" + top + ") * FROM " + table.Trim() + " WHERE " + column.Trim() + " = '" + parameter.Trim() + "';";
            DataTable tbl = new DataTable();
            adt = new SqlDataAdapter(query, cnn);
            adt.Fill(tbl);
            return tbl;
        }
        public DataTable SelectTop(string table, List<string> selectcolumn, string where, int top)
        {
            string query = @"SELECT TOP (" + top + ") ";
            foreach (string a in selectcolumn)
            {
                query += a.Trim();
                if (a != selectcolumn.Last())
                {
                    query += ", ";
                }
            }
            query += (" FROM " + table.Trim() + " WHERE " + where.Trim());
            DataTable tbl = new DataTable();
            adt = new SqlDataAdapter(query, cnn);
            adt.Fill(tbl);
            return tbl;
        }
        public DataTable Select(string table)
        {
            string query = @"SELECT * FROM " + table.Trim();
            DataTable tbl = new DataTable();
            adt = new SqlDataAdapter(query, cnn);
            adt.Fill(tbl);
            return tbl;
        }
        public DataTable Select(string table, string column, string parameter)
        {
            string query = @"SELECT * FROM " + table.Trim() + " WHERE " + column.Trim() + " = N'" + parameter.Trim() + "';";
            DataTable tbl = new DataTable();
            adt = new SqlDataAdapter(query, cnn);
            adt.Fill(tbl);
            return tbl;
        }
        public DataTable Select(string table, string column1, string parameter1, string column2, string parameter2)
        {
            string query = @"SELECT * FROM " + table.Trim() + " WHERE " + column1.Trim() + " = N'" + parameter1.Trim() + "' AND " + column2.Trim() + " = N'" + parameter2.Trim() + "';";
            DataTable tbl = new DataTable();
            adt = new SqlDataAdapter(query, cnn);
            adt.Fill(tbl);
            return tbl;
        }
        public DataTable Select(string table, List<string> selectcolumn, string where)
        {
            string query = @"SELECT ";
            foreach (string a in selectcolumn)
            {
                query += a.Trim();
                if (a != selectcolumn.Last())
                {
                    query += ", ";
                }
            }
            query += (" FROM " + table.Trim() + " WHERE " + where.Trim());
            DataTable tbl = new DataTable();
            adt = new SqlDataAdapter(query, cnn);
            adt.Fill(tbl);
            return tbl;
        }
        public int Update(string Table, List<string> Columns, List<string> Values, string Where)
        {
            string query = @"SET DATEFORMAT dmy; UPDATE " + Table.Trim() + " SET ";
            for (int i = 0; i < Columns.Count; i++)
            {
                string prm = Columns[i].Trim() + " = N'" + Values[i]?.Trim() + "', ";
                query += prm;
            }
            query = query.Trim().Substring(0, query.Length - 2);
            query += " WHERE " + Where;
            cmd = new SqlCommand(query, cnn);
            return cmd.ExecuteNonQuery();
        }

        public int Insert(string Table, List<string> Values)
        {
            string query = @"SET DATEFORMAT dmy; INSERT INTO " + Table.Trim() + " VALUES (N'";
            for (int i = 0; i < Values.Count; i++)
            {
                string prm = Values[i]?.Trim() + "', N'";
                query += prm;
            }
            query = query.Trim().Substring(0, query.Length - 4);
            query += ");";
            cmd = new SqlCommand(query, cnn);
            return cmd.ExecuteNonQuery();
        }

        public int Delete(string Table, string Column, string Value)
        {
            string query = @"DELETE FROM " + Table.Trim() + " WHERE " + Column + " = '" + Value + "';";
            cmd = new SqlCommand(query, cnn);
            return cmd.ExecuteNonQuery();
        }

        public int Delete(string Table, string Column, string Value, string Column1, string Value1)
        {
            string query = @"DELETE FROM " + Table.Trim() + " WHERE " + Column + " = '" + Value + "' AND " + Column1 + " = '" + Value1 + "';";
            cmd = new SqlCommand(query, cnn);
            return cmd.ExecuteNonQuery();
        }

        public string InsertSCOPE(string Table, List<string> Values)
        {
            string query = @"SET DATEFORMAT dmy; INSERT INTO " + Table.Trim() + " VALUES (N'";
            for (int i = 0; i < Values.Count; i++)
            {
                string prm = Values[i]?.Trim() + "', N'";
                query += prm;
            }

            query = query.Trim().Substring(0, query.Length - 4);
            query += "); SELECT SCOPE_IDENTITY()";
            DataTable tbl = new DataTable();
            adt = new SqlDataAdapter(query, cnn);
            adt.Fill(tbl);
            return tbl.Rows[0][0].ToString().Trim();
        }
    }
}