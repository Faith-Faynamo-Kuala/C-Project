using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentManagenmentSystem
{
    public partial class ManageScoreForm : Form
    {
        CourseClass course = new CourseClass();
        ScoreClass score = new ScoreClass();
        public ManageScoreForm()
        {
            InitializeComponent();
        }

        private void ManageScoreForm_Load(object sender, EventArgs e)
        {
            // Populate the combobox with course name 
            comboBox_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `course` "));
            comboBox_course.DisplayMember = "CourseName";
            comboBox_course.ValueMember = "CourseName";
            // To show data on grid view
            showScore();
        }
        public void showScore()
        {
            DataGridView_score.DataSource = score.getList(new MySqlCommand("SELECT score.StudentId,student.stdFirstName,student.stdLastName,score.CourseName,score.Score,score.Description FROM student INNER JOIN score ON score.StudentId=student.StdId"));
        }

       
        private void button_update_Click(object sender, EventArgs e)
        {
            if (textBox_stdId.Text == "" || textBox_score.Text == "")
            {
                MessageBox.Show("Need Score data", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int stdId = Convert.ToInt32(textBox_stdId.Text);
                string cName = comboBox_course.Text;
                double scor = Convert.ToInt32(textBox_score.Text);
                string desc = textBox_description.Text;
                
                    if (score.updatetScore(stdId, scor, desc, cName))
                    {
                        showScore();
                        button_clear.PerformClick();
                        MessageBox.Show("Score edit complete", "Update score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Score Not added", "Update score", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                
            }
        
        private void button_delete_Click(object sender, EventArgs e)
        {
            
            if (textBox_stdId.Text == "")
            {
                MessageBox.Show("Field Error- We need student Id ","Delete Score",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int id = Convert.ToInt32(textBox_stdId.Text);

                // Show a confirmation message before deleting the student
                if (MessageBox.Show("Are you sure you want to delete this score?", "Delete score", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (score.deleteScore(id))
                    {
                        showScore();
                        MessageBox.Show("Score Removed", "Delete Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button_clear.PerformClick();
                    }
                }
            }

        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_stdId.Clear();
            textBox_score.Clear();
            textBox_description.Clear();
            textBox_search.Clear();
        }

         
        private void DataGridView_course_click(object sender, EventArgs e)
        {
            textBox_stdId.Text = DataGridView_score.CurrentRow.Cells[0].Value.ToString();
            comboBox_course.Text = DataGridView_score.CurrentRow.Cells[3].Value.ToString();
            textBox_score.Text = DataGridView_score.CurrentRow.Cells[4].Value.ToString();
            textBox_description.Text = DataGridView_score.CurrentRow.Cells[5].Value.ToString();
        }

        private void DataGridView_score_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button_search_Click(object sender, EventArgs e)
        {
            DataGridView_score.DataSource = score.getList(new MySqlCommand("SELECT score.StudentId,student.stdFirstName,student.stdLastName,score.CourseName,score.Score,score.Description FROM student INNER JOIN score ON score.StudentId=student.StdId WHERE CONCAT(student.stdFirstName,student.stdLastName,score.CourseName) LIKE '%" + textBox_search.Text +"%'"));
            
        }
    }
}
