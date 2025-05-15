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
using static System.ComponentModel.Design.ObjectSelectorEditor;
using DGVPrinterHelper;

namespace StudentManagenmentSystem
{
    public partial class printStudent : Form
    {
        studentClass student = new studentClass();
        DGVPrinter printer = new DGVPrinter();
        public printStudent()
        {
            InitializeComponent();
        }
                
        private void printStudent_Load(object sender, EventArgs e)
        {
            showData(new MySqlCommand("SELECT * FROM `student`"));
        }
        // Create a function to show the student list in datagridview
        public void showData(MySqlCommand command)
        {
            DataGridView_student.ReadOnly = true;
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            DataGridView_student.DataSource = student.getList(command);
            // Column 7 is the image Column index
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[7];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }
        private void radioButton_female_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button_search_Click(object sender, EventArgs e)
        {
            // Check the radio button
            string selectQuery;
            if (radioButton_all.Checked)
            {
                selectQuery = "SELECT * FROM `student`";
            }
            else if (radioButton_male.Checked) 
            {
                selectQuery = "SELECT * FROM `student` WHERE `Gender`='Male'";
            }
            else
            {
                selectQuery = "SELECT * FROM `student` WHERE `Gender`= 'Female'";
            }
            showData(new MySqlCommand(selectQuery));
        }

        private void button_print_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter();  // create new printer instance each print

            printer.Title = "Ialibu Secondary Student List";
            printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = false;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "Ialibu Secondary";
            printer.FooterSpacing = 15;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            try
            {
                printer.PrintDataGridView(DataGridView_student);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while printing: " + ex.Message);
            }
        }
    }
}
