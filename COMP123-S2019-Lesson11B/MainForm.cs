using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMP123_S2019_Lesson11B
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This is the event handler for the MainForm's Closing Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// This is the event handler for the Exit Menu Item's Click event       
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.aboutBox.ShowDialog();
        }

        private void HelpToolStripButton_Click(object sender, EventArgs e)
        {
            Program.aboutBox.ShowDialog();
        }

        private void ExittoolStripButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'testDatabaseDataSet.StudentTable' table. You can move, or remove it, as needed.
            this.studentTableTableAdapter.Fill(this.testDatabaseDataSet.StudentTable);

        }

        private void GetDataButton_Click(object sender, EventArgs e)
        {
            Program.studentInfoForm.Show();
            this.Hide();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //open file 

            using (StreamWriter outputStream = new StreamWriter(
                File.Open("Student.txt", FileMode.Create)))
            {
                //write file
                outputStream.WriteLine(Program.student.id);
                outputStream.WriteLine(Program.student.StudentID);
                outputStream.WriteLine(Program.student.FirstName);
                outputStream.WriteLine(Program.student.LastName);

                //close file
                outputStream.Close();

                //dispose of the memory
                outputStream.Dispose();
            }

            MessageBox.Show("File Saved", "Saving...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void StudentTableGridView_SelectionChanged(object sender, EventArgs e)
        {
            //aliases
            var rowIndex = StudentTableGridView.CurrentCell.RowIndex;
            var rows = StudentTableGridView.Rows;
            var cells = rows[rowIndex].Cells;
            var columnCount = StudentTableGridView.ColumnCount;

            StudentTableGridView.Rows[rowIndex].Selected = true;

            string outputString = string.Empty;
            for (int index = 0; index < columnCount; index++)
            {
                outputString += cells[index].Value.ToString() + " ";
            }

            //helps debugging
            //MessageBox.Show(outputString);
            //if (MessageBox.Show(outputString, "Output String", MessageBoxButtons.YesNoCancel) == DialogResult.OK) { //do something};
            //            Debug.WriteLine(outputString);
            SelectionLabel.Text = outputString;

            Program.student.id = int.Parse(cells[(int)StudentField.ID].Value.ToString());
            Program.student.StudentID = cells[(int)StudentField.STUDENT_ID].Value.ToString();
            Program.student.FirstName = cells[(int)StudentField.FIRST_NAME].Value.ToString();
            Program.student.LastName = cells[(int)StudentField.LAST_NAME].Value.ToString();
        }
    }
}
