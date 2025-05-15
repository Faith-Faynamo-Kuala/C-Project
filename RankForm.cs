using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DGVPrinterHelper;

namespace StudentManagenmentSystem
{
    public partial class RankForm : Form
    {
        RankingClass ranking = new RankingClass();
        DGVPrinter printer = new DGVPrinter();
       
        public RankForm()
        {
            InitializeComponent();
        }

        private void DataGridView_ranking_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
        }
       
        private void RankForm_Load(object sender, EventArgs e)
        {
            ShowRankings();
        }
        private void ShowRankings()
        {
            DataTable rankings = ranking.GetStudentRankings();
            DataGridView_ranking.DataSource = rankings;
            // Optional: Customize column headers
            DataGridView_ranking.Columns["stdId"].HeaderText = "Student ID";
            DataGridView_ranking.Columns["stdFirstName"].HeaderText = "First Name";
            DataGridView_ranking.Columns["stdLastName"].HeaderText = "Last Name";
            DataGridView_ranking.Columns["CourseName"].HeaderText = "Course";
            DataGridView_ranking.Columns["Score"].HeaderText = "Score";
            DataGridView_ranking.Columns["Grading"].HeaderText = "Grading";
        }

        private void button_print_Click(object sender, EventArgs e)
        {
            // we need DGV printer Helper for print PDF file
            printer.Title = "Ialibu Secondary Student Score List";
            printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = false; // Important for fitting nicely
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = ""; // Remove footer to save space
            printer.FooterSpacing = 5;             // Smaller footer spacing
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.printDocument.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(10, 10, 10, 10);
            // Optional: Adjust DataGridView column widths just before printing
            DataGridView_ranking.Columns["stdId"].Width = 60;
            DataGridView_ranking.Columns["stdFirstName"].Width = 120;
            DataGridView_ranking.Columns["stdLastName"].Width = 120;
            DataGridView_ranking.Columns["CourseName"].Width = 100;
            DataGridView_ranking.Columns["Score"].Width = 60;
            DataGridView_ranking.Columns["Grading"].Width = 50;
            printer.PrintDataGridView(DataGridView_ranking);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

