using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyT4Templating
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionstring = "";
            var strcommandText = "select name, object_id from sys.tables where type = 'U' and name not like 'sys%'";
            using(SqlConnection scon = new SqlConnection(connectionstring))
            {
                scon.Open();

                using(SqlCommand scmd = new SqlCommand(strcommandText, scon))
                {
                    using(var reader = scmd.ExecuteReader())
                    {
                        string name = reader.GetString(0);
                        string objectid = reader.GetInt32(1);
                        
                        WriteProperties(objectid, scon);
                    }
                }
            }
        }

        static void WriteProperties(string objectid, SqlConnection scon)
        {
            string strcommandText = "select * from sys.columns where object_id = " + objectid;

            using (SqlCommand scmd = new SqlCommand(strcommandText, scon))
            {
                using (var reader = scmd.ExecuteReader())
                {

                }
            }
        }
    }
}
