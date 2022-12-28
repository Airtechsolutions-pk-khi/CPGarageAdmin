using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace CPGarage.Helpers
{
    public  class Utility
    {
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    if (rows.Length > 1)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i].Trim();
                        }
                        dt.Rows.Add(dr);
                    }
                }

            }


            return dt;
        }

        public static DataTable ConvertXSLXtoDataTable(string strFilePath, string connString)
        {
            OleDbConnection oledbConn = new OleDbConnection(connString);
            DataTable dt = new DataTable();
            try
            {

                oledbConn.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn))
                {
                    OleDbDataAdapter oleda = new OleDbDataAdapter();
                    oleda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    oleda.Fill(ds);

                    dt = ds.Tables[0];
                }
            }
            catch(Exception Ex)
            {


            }
            finally
            {

                oledbConn.Close();
            }

            return dt;

        }



        //public static string ConvertDataTabletoString(DataTable Set)
        //{
        //    DataTable dt = Set;


        //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //    Dictionary<string, object> row;
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        row = new Dictionary<string, object>();
        //        foreach (DataColumn col in dt.Columns)
        //        {
        //            row.Add(col.ColumnName, dr[col]);
        //        }
        //        rows.Add(row);
        //    }
        //    return serializer.Serialize(rows);


        //}


        public static FileResultCompare ValidateFile(DataTable DataSet,DataTable Sample)
        {
            if (DataSet.Columns.Count == 0) return new FileResultCompare(false, "No Coloum Found");
            else if (DataSet.Columns.Count != Sample.Columns.Count) return new FileResultCompare(false, "Data Must Match With Sample Document");
            else if (HasNull(DataSet)) return new FileResultCompare(true, "Dataset has some null entries");
            else return   new FileResultCompare(true, "Success");
        }


        public static  bool HasNull( DataTable table)
        {
            foreach (DataColumn column in table.Columns)
            {
                if (table.Rows.OfType<DataRow>().Any(r => r.IsNull(column)))
                    return true;
            }

            return false;
        }

    }


    public class FileResultCompare
    {
        public bool Parsed { get; set; }
        public string Cause { get; set; }

        public FileResultCompare() { }

        public FileResultCompare(bool Parsed,string Cause)
        {
            this.Cause = Cause;
            this.Parsed = Parsed;
        }
    }
}


