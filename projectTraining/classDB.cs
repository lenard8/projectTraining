using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
namespace projectTraining
{
    class classDB
    {
        public string GetConnection()
        {
            string connection = "server=localhost;user id=root;password=;database=sampleDB";
            return connection;
        }
    }
}
