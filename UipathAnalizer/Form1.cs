using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIPathValidator.Validation;
using UIPathValidator.UIPath;
using System.IO;

namespace UipathAnalizer
{
    public partial class Form1 : Form
    {
        private int raiseStep = 5;
        private string projectPath = "";
        Form3 form3;
        Form2 form2;

        public Form1()
        {
            InitializeComponent();

        }
        private void timer1_Tick(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillData("button");
        }

        public void FillData(string flag)
        {

            folderBrowserDialog.ShowNewFolderButton = true;
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {

                projectPath = folderBrowserDialog.SelectedPath;
                label2.Text = projectPath;
            }

            else
            {
                timer1.Enabled = false;
                timer1.Stop();
            }
        }

        public void FillGridView(ProjectValidator pValidator)
        {
            DataTable warning = new DataTable();
            warning.Columns.Add("Message");
            DataTable info = new DataTable();
            info.Columns.Add("Message");
            DataTable error = new DataTable();
            error.Columns.Add("Message");

            foreach (var item in pValidator.GetResults())
            {
                if (item.ToString().Contains("WARNİNG"))
                {
                    DataRow drWarning = warning.NewRow();
                    drWarning["Message"] = item;
                    warning.Rows.Add(drWarning);
                }
                else if (item.ToString().Contains("ERROR"))
                {
                    DataRow drError = error.NewRow();
                    drError["Message"] = item;
                    error.Rows.Add(drError);
                }
                else
                {
                    DataRow drInfo = info.NewRow();
                    drInfo["Message"] = item;
                    info.Rows.Add(drInfo);
                }

            }

            dataGridView1.DataSource = warning;
            dataGridView2.DataSource = info;
            dataGridView3.DataSource = error;

            // for Pie Chart

            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
            chart1.Titles.Clear();
            var totalReportCount = dataGridView1.RowCount + dataGridView2.RowCount + dataGridView3.RowCount;
            var errorValue = (dataGridView3.RowCount * 100 / totalReportCount).ToString();
            var infoValue = (dataGridView2.RowCount * 100 / totalReportCount).ToString();
            var warningValue = (dataGridView1.RowCount * 100 / totalReportCount).ToString();
            chart1.Titles.Add("Results Rates");
            chart1.Series["Series1"].Points.AddXY(string.Concat("Error  %", errorValue), errorValue);
            chart1.Series["Series1"].Points.AddXY(string.Concat("Info  %", infoValue), infoValue);
            chart1.Series["Series1"].Points.AddXY(string.Concat("Warning  %", warningValue), warningValue);


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label10.Visible = true;
                label9.Visible = true;
            }
            else
            {
                label10.Visible = false;
                label9.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form3 = new Form3();
            form2 = new Form2();
            if (!String.IsNullOrEmpty(projectPath))
            {

                
                    ProjectValidator pValidator = null;
                    ReadFile readFile = new ReadFile();
                    Project project = null;

                    if (checkBox1.Checked && File.Exists(String.Concat(projectPath, @"\Data\", "Config.xlsx")))
                    {
                        // form2.Show();
                        form3.Show();
                        timer1.Enabled = true;
                        timer1.Start();
                        this.Enabled = false;
                        label10.Text = readFile.ReadConfigExcel(@projectPath);
                        label10.Visible = true;
                        label9.Visible = true;
                    }
                    else
                    {
                        label10.Visible = false;
                        label9.Visible = false;
                    }

                    project = readFile.ProjectLoad(@projectPath);

                    label5.Text = project.Name;
                    label6.Text = project.Version;
                    label8.Text = project.StudioVersion;
                    pValidator = readFile.ControlTotalDelayTime(@projectPath);

                    if (pValidator != null && project != null)
                    {
                        FillGridView(pValidator);
                    }
                    if (checkBox1.Checked)
                    {
                        form3.Close();
                        this.Enabled = true;
                    }

                if (checkBox1.Checked)
                {

                    if (!File.Exists(String.Concat(projectPath, @"\Data\", "Config.xlsx")))
                    {
                        MessageBox.Show(String.Concat("Didn't Find Config File In That Path ", @projectPath));
                    }
                }

            }
            else
            {
                MessageBox.Show("Didn't Choose Project File");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var executePath = "";
            if (dataGridView1.SelectedRows.Count > 0)
            {
                executePath = String.Concat(projectPath, @"\", ((System.Data.DataRowView)dataGridView1.SelectedRows[0].DataBoundItem).Row.ItemArray[0].ToString().Split('-')[0].Split(':')[1].Trim());
            }
            else if (dataGridView2.SelectedRows.Count > 0)
            {
                executePath = String.Concat(projectPath, @"\", ((System.Data.DataRowView)dataGridView1.SelectedRows[0].DataBoundItem).Row.ItemArray[0].ToString().Split('-')[0].Split(':')[1].Trim());
            }
            else if (dataGridView3.SelectedRows.Count > 0)
            {
                executePath = String.Concat(projectPath, @"\", ((System.Data.DataRowView)dataGridView1.SelectedRows[0].DataBoundItem).Row.ItemArray[0].ToString().Split('-')[0].Split(':')[1].Trim());
            }

            if (dataGridView1.SelectedRows.Count == 0 && dataGridView2.SelectedRows.Count == 0 && dataGridView3.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please Select a Row!");
            }
            else
            {
                System.Diagnostics.Process.Start(executePath);
            }
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            dataGridView3.ClearSelection();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("SchedualCalender.exe");
        }
    }
}
