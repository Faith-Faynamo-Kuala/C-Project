using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;


namespace StudentManagenmentSystem
{
    public partial class mainForm : Form
    {
        studentClass student = new studentClass();
        public mainForm()
        {
            InitializeComponent();
            customizeDesign();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            studentCount();
        }
        // Create a function to display the student count'
        private void studentCount()
        {
            // Dislay the values
            label_totalStd.Text = "Total Students : " + student.totalStudent();
            label_maleStd.Text = "Male : " + student.maleStudent();
            label_femaleStd.Text = "Female : " + student.femaleStudent();
        }
        private void customizeDesign()
        {
            panel_stdsubmenu.Visible = false;
            panel_CourseSubmenu.Visible = false;
            panel_scoreSubmenu.Visible = false;
        }
        private void hideSubmenu()
        {
            if (panel_stdsubmenu.Visible == true)
                panel_stdsubmenu.Visible = false;
            if (panel_CourseSubmenu.Visible == true)
                panel_CourseSubmenu.Visible = false;
            if (panel_scoreSubmenu.Visible == true)
                panel_scoreSubmenu.Visible = false;
        }
        private void showSubmenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hideSubmenu();
                submenu.Visible = true;
            }
            else
               submenu.Visible = false;
            
        }

        private void button_std_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_stdsubmenu);
         }
        #region StdSubmenu

        private void button_registration_Click(object sender, EventArgs e)
        {
            openChildForm(new RegisterForm());
            
            hideSubmenu();
        }

        private void button_managestd_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageStudentForm());

           
            hideSubmenu();
        }

        private void button_status_Click(object sender, EventArgs e)
        {
            openChildForm(new RankForm());
            hideSubmenu();
        }

        private void button_stdPrint_Click(object sender, EventArgs e)
        {
            openChildForm(new printStudent());
            
            hideSubmenu();
        }
        #endregion StdSubmenu

        private void button_course_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_CourseSubmenu);
        }
        #region CourseSubmenu
        private void button_newCourse_Click(object sender, EventArgs e)
        {
            openChildForm(new CourseForm());
           
            hideSubmenu();
        }

        private void button_manageCourse_Click(object sender, EventArgs e)
        {
            openChildForm( new ManageCourseForm());
            
            hideSubmenu();
        }

        private void button_coursePrint_Click(object sender, EventArgs e)
        {
            openChildForm(new PrintCourseForm());
            
            hideSubmenu();
        }
        #endregion CourseSubmenu
        private void button_score_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_scoreSubmenu);
        }
        #region ScoreSubmenu
        private void button_newScore_Click(object sender, EventArgs e)
        {
            openChildForm(new ScoreForm());
            
            hideSubmenu();
        }

        private void button_manageScore_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageScoreForm());
            
            hideSubmenu();
        }

        private void button_scorePrint_Click(object sender, EventArgs e)
        {
            openChildForm(new PrintScoreForm());
            
            hideSubmenu();
        }
        #endregion ScoreSubmenu

        // To show register form in mainform
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_main.Controls.Add(childForm);
            panel_main.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button_dashboard_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            panel_main.Controls.Add(panel_cover);
            studentCount();
        }

       
        private void button_exit_Click(object sender, EventArgs e)
        {
            // Show a confirmation message before exiting
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // If the user clicked Yes, hide the current form and show the LoginForm
                LoginForm login = new LoginForm();
                this.Hide();
                login.Show();
            }
            // If the user clicked No, do nothing and stay on the current form

        }
    }
}
