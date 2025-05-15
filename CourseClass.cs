using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using StudentManagenmentSystem.Resources;

namespace StudentManagenmentSystem
{
    internal class CourseClass
    {
        DBconnect connect = new DBconnect();
        // Create a function to insert course
        public bool insetCourse(string cName, int hr, string desc)
        {
            MySqlCommand command  = new MySqlCommand("INSERT INTO `course`(`CourseName`, `CourseHour`, `Description`) VALUES(@cn, @ch, @desc)", connect.Getconnection);
            //@cn, @ch, @desc
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = cName;
            command.Parameters.Add("@ch", MySqlDbType.Int32).Value = hr;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = desc;
            connect.OpenConnect();
            if (command.ExecuteNonQuery() ==1)
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
        // Create a function to get course list
        public DataTable getCourse(MySqlCommand command)
        {
            command.Connection = connect.Getconnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        // create a update function for course edit
        public bool updateCourse(int id, string cName, int hr, string desc)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `course` SET `CourseName`=@cn,`CourseHour`=@ch,`Description`=@desc WHERE `CourseId`=@id", connect.Getconnection);
            // @id, @cn, @ch, @desc
            command.Parameters.Add("@id", MySqlDbType.Int32).Value =id;
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = cName;
            command.Parameters.Add("@ch", MySqlDbType.Int32).Value = hr;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = desc;
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
        // Create a function to delete a class
        // We only need course id
        public bool deleteCourse(int id) 
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `course` WHERE `CourseId`=@id", connect.Getconnection);
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
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
        // Create a function search for course (course name)
        public DataTable searchcourse(string searchdata)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `course` WHERE CONCAT(`CourseName`) LIKE @search", connect.Getconnection);
            command.Parameters.Add("@search", MySqlDbType.VarChar).Value = "%" + searchdata + "%"; // Use parameterized query to prevent SQL injection
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
