﻿using System;
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
        /// <summary>
        /// Save file event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //configuring the dialog
            saveFileDialog1.FileName = "Student.txt";
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog1.Filter = "Text Files (*.txt)|*.txt| All Files (*.*)|*.*";

            //open file dialog - Modal Form
            var result = saveFileDialog1.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                //open file 

                using (StreamWriter outputStream = new StreamWriter(
                    File.Open(saveFileDialog1.FileName, FileMode.Create)))
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

        private void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            //configure open file dialog
            StudentOpenFileDialog.FileName = "Student.txt";
            StudentOpenFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            StudentOpenFileDialog.Filter = "Text Files (*.txt)|*.txt| All Files (*.*)|*.*";

            //open the file dialog
            var result = StudentOpenFileDialog.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                using (StreamReader inputStream = new StreamReader(
                File.Open("Student.txt", FileMode.Open)))
                { 
                    try
                    {
                        //read file
                        Program.student.id = int.Parse(inputStream.ReadLine());
                        Program.student.StudentID = inputStream.ReadLine();
                        Program.student.FirstName = inputStream.ReadLine();
                        Program.student.LastName = inputStream.ReadLine();

                        inputStream.Close();
                        inputStream.Dispose();

                        GetDataButton_Click(sender, e);
                    }
                    catch (IOException exception)
                    {
                        MessageBox.Show("Error: " + exception.Message, "File I/O Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void SaveBinaryFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //configuring the dialog
            saveFileDialog1.FileName = "Student.dat";
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog1.Filter = "Binary Files (*.dat)|*.dat| All Files (*.*)|*.*";

            //open file dialog - Modal Form
            var result = saveFileDialog1.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                //open file 

                using (BinaryWriter outputStream = new BinaryWriter(
                    File.Open(saveFileDialog1.FileName, FileMode.Create)))
                {
                    //write file
                    outputStream.Write(Program.student.id);
                    outputStream.Write(Program.student.StudentID);
                    outputStream.Write(Program.student.FirstName);
                    outputStream.Write(Program.student.LastName);

                    //cleanup section 

                    outputStream.Flush();

                    //close file
                    outputStream.Close();

                    //dispose of the memory
                    outputStream.Dispose();
                }
            }

            MessageBox.Show("Binary File Saved", "Saving Binary File...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OpenBinaryFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //configure open file dialog
            StudentOpenFileDialog.FileName = "Student.dat";
            StudentOpenFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            StudentOpenFileDialog.Filter = "Binary Files (*.dat)|*.dat| All Files (*.*)|*.*";

            //open the file dialog
            var result = StudentOpenFileDialog.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                using (BinaryReader inputStream = new BinaryReader(
                File.Open(StudentOpenFileDialog.FileName, FileMode.Open)))
                {
                    try
                    {
                        //read file
                        Program.student.id = int.Parse(inputStream.ReadString());
                        Program.student.StudentID = inputStream.ReadString();
                        Program.student.FirstName = inputStream.ReadString();
                        Program.student.LastName = inputStream.ReadString();

                        inputStream.Close();
                        inputStream.Dispose();

                        GetDataButton_Click(sender, e);
                    }
                    catch (IOException exception)
                    {
                        MessageBox.Show("Error: " + exception.Message, "File I/O Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
