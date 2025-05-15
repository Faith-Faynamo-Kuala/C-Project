using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace StudentManagenmentSystem.Resources
{
    /* In this class create the connection between application and mysql database 
     * we need to install Xampp and my sql connector to this project
     * we need to create the student  database 
     */
    public class DBconnect
    {
        // to create connection
       MySqlConnection connect = new MySqlConnection("datasource=localhost;port=3306;username=root;password=root;database=studentdb");
        internal MySqlConnection getconnection;

        // To get Connection
        public MySqlConnection Getconnection
        {
            get
            {
                return connect;
            }
        }

        // Create a function to open connection
        public void OpenConnect()
        {
            if (connect.State == System.Data.ConnectionState.Closed)
                connect.Open();
        }
        // Create a function to close connection
        public void CloseConnect()
        {
            if (connect.State == System.Data.ConnectionState.Open)
                connect.Close();
        }
    }
}
