using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using StudentManagenmentSystem.Resources;
using System.Data;
using System.Windows.Forms;

namespace StudentManagenmentSystem
{
     class studentClass
    {
        DBconnect connect = new DBconnect();
        //create a function to add a new student to the database
        public bool insertStudent(string fname, string lname, DateTime bdate, string gender, string phone, string address, byte[] img)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `student`(`stdFirstName`, `stdLastName`, `BirthDate`, `Gender`, `Phone`, `Address`, `Photo`) " +
                "VALUES (@fn, @ln, @bd, @gd, @ph, @adr, @img)", 
                connect.Getconnection);
            //@fn, @ln, @bd, @gd, @ph, @adr, @img
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bd", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adr", MySqlDbType.VarChar).Value = address;
            command.Parameters.Add("@img", MySqlDbType.Blob).Value = img;

            connect.OpenConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.CloseConnect();
                return true;
            }
            else
            {
                connect.CloseConnect();
                return false;
            }
        }
        // to get student table
        public DataTable GetstudentList(MySqlCommand command)
        {
            command.Connection = connect.Getconnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        // create a function to execute the count query(total, male, female)
        public string exeCount(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connect.Getconnection);
            connect.OpenConnect();
            string count = command.ExecuteScalar().ToString();
            connect.CloseConnect();
            return count;

        }
        // To get the total student
        public string totalStudent()
        {
            return exeCount("SELECT COUNT(*) FROM student");
        }
        // To get the male student count
        public string maleStudent()
        {
            return exeCount("SELECT COUNT(*) FROM student WHERE `Gender`='Male'");
        }
        // To get the female student count
        public string femaleStudent()
        {
            return exeCount("SELECT COUNT(*) FROM student WHERE `Gender`='Female'");
        }

        // Create a function search for students (first name, last name, address)
        public DataTable searchStudent(string searchdata)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `student` WHERE CONCAT(`stdFirstName`,`stdLastName`,`Address`) LIKE '%"+searchdata+"%'", connect.Getconnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        // Create a function edit for student
        public bool updateStudent(int id, string fname, string lname, DateTime bdate, string gender, string phone, string address, byte[] img)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `student` SET `stdFirstName`=@fn, `stdLastName`=@ln, `BirthDate`=@bd, `Gender`=@gd, `Phone`=@ph, `Address`=@adr, `Photo`=@img WHERE `stdId`=@id", 
                connect.Getconnection);
            //@fn, @ln, @bd, @gd, @ph, @adr, @img
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bd", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adr", MySqlDbType.VarChar).Value = address;
            command.Parameters.Add("@img", MySqlDbType.Blob).Value = img;
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id; // Add the ID parameter

            connect.OpenConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.CloseConnect();
                return true;
            }
            else
            {
                connect.CloseConnect();
                return false;
            }
        }
        // Create a function to delete student data 
        public bool deleteStudent(int id) {

            MySqlCommand command = new MySqlCommand("DELETE FROM `student` WHERE `stdId`=@id", connect.Getconnection);
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            try
            {
                connect.OpenConnect(); // Ensure the connection is open
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected == 1; // Return true if one row was deleted
            }
            catch (MySqlException ex)
            {
                // Log or display the error message
                MessageBox.Show("Error deleting student: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Indicate failure
            }
            finally
            {
                connect.CloseConnect(); // Ensure the connection is closed
            }
        }

        //Create a function for any command in studentDB
        public DataTable getList(MySqlCommand command)
        {
            command.Connection = connect.Getconnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
  }
