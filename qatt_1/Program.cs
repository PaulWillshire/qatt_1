using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qatt_1
{
    class Program
    {
        static void Main()
        {
            Program spd = new Program();

            // run a simple stored procedure
            spd.RunStoredProc();

            // run a stored procedure that takes a parameter
            //spd.RunStoredProcParams();
        }

        // run a simple stored procedure
        public void RunStoredProc()
        {
            SqlConnection conn = null;
            SqlDataReader rdr = null;

            Console.WriteLine("\nTop 10 Most Expensive Products:\n");

            try
            {
                // create and open a connection object
                
                conn = new
                    SqlConnection("Server=(local);DataBase=test_Transaction;Integrated Security=SSPI");
                conn.Open();

                // 1. create a command object identifying
                // the stored procedure
                SqlCommand cmd = new SqlCommand(
                    "dbo.test_BatchOrderSelect", conn);

                // 2. set the command object so it knows
                // to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // execute the command
                rdr = cmd.ExecuteReader();

                // iterate through results, printing each to console
                while (rdr.Read())
                {

                    SqlParameter parm2 = new SqlParameter("@BatchCode", SqlDbType.VarChar);
                    parm2.Size = 255;
                    parm2.Direction = ParameterDirection.Output;

                    Console.WriteLine(cmd.Parameters["@BatchCode"].Value);

                    /*Console.WriteLine(
                        "Batch ID: ${0} Batch Code: {0, -25}",
                        rdr["BatchOrderId"],
                        rdr["BatchCode"]);
                    Console.ReadLine();*/
                }
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (rdr != null)
                {
                    rdr.Close();
                }
            }
        }
    }
}
