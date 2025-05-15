using MySql.Data.MySqlClient;
using StudentManagenmentSystem.Resources;
using System.Data;

namespace StudentManagenmentSystem
{
    internal class RankingClass
    {
        DBconnect connect = new DBconnect();
        public DataTable GetStudentRankings()
        {
            string query = @"
                SELECT 
                    s.stdId, 
                    s.stdFirstName, 
                    s.stdLastName, 
                    sc.CourseName, 
                    sc.Score,
                    CASE
                        WHEN sc.Score >= 90 THEN 'A'
                        WHEN sc.Score >= 80 THEN 'B'
                        WHEN sc.Score >= 70 THEN 'C'
                        WHEN sc.Score >= 60 THEN 'D'
                        ELSE 'F'
                    END AS Grading
                FROM student s
                INNER JOIN score sc ON s.stdId = sc.StudentId
                ORDER BY sc.Score DESC";
            MySqlCommand command = new MySqlCommand(query, connect.Getconnection);
            return GetList(command);
        }
        private DataTable GetList(MySqlCommand command)
        {
            command.Connection = connect.Getconnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
