using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.Design.Directives;
using MySql.Data.MySqlClient;
using StudentManagenmentSystem.Resources;

namespace StudentManagenmentSystem
{
    internal class ScoreClass
    {
        DBconnect connect = new DBconnect();
        // Create a function to add scrore
        public bool insertScore(int stdid, string courseName, double scor, string desc)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `score`(`StudentId`, `CourseName`, `Score`, `Description`) VALUES (@stid,@cn,@sco,@desc)", connect.Getconnection);
            //@stid,@cn,@sco,@desc
            command.Parameters.Add("@stid", MySqlDbType.Int32).Value = stdid;
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = courseName;
            command.Parameters.Add("@sco", MySqlDbType.Double).Value = scor;
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
        // Create a function to get List
        public DataTable getList(MySqlCommand command)
        {
            command.Connection = connect.Getconnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        // Create a function to check already course score
        public bool checkScore(int stdId, string cName)
        {
            DataTable table = getList(new MySqlCommand("SELECT * FROM `score` WHERE `StudentId`= '"+ stdId +"' AND `CourseName`='"+ cName +"'"));
            if (table.Rows.Count > 0) 
            { return true; }
            else { return false; }
        }
        // Create a function to edit score data
        public bool updatetScore(int stdid, double scor, string desc, string scn)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `score` SET `Score`=@sco,`Description`=@desc WHERE `StudentId`=@stid AND `CourseName`=@scn", connect.Getconnection);
            //@stid,@sco,@desc,@scn
            command.Parameters.Add("@scn", MySqlDbType.VarChar).Value = scn;
            command.Parameters.Add("@stid", MySqlDbType.Int32).Value = stdid;
            command.Parameters.Add("@sco", MySqlDbType.Double).Value = scor;
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
        // Create a function to delete a score data
        public bool deleteScore(int id)
        {

            MySqlCommand command = new MySqlCommand("DELETE FROM `score` WHERE `StudentId`=@id", connect.Getconnection);
            //@id
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
    }
    }


