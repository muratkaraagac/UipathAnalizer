using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xaml;
using UIPathValidator.UIPath;
using UIPathValidator.Validation;
using System.Data;
using ExcelApp = Microsoft.Office.Interop.Excel;

namespace UipathAnalizer
{
    public class ReadFile
    {
        public Project project;
        public Project ProjectLoad(string projectPath)
        {
            project = new Project(projectPath);
            project.Load();

            return project;
        }
        public ProjectValidator ControlTotalDelayTime(string projectPath)
        {
            ProjectValidator projectValidator = new ProjectValidator(project);
            projectValidator.Validate();
            return projectValidator;
        }

        public string ReadConfigExcel(string projectPath)
        {
            string queueName = "";
            ExcelApp.Application excelApp = new ExcelApp.Application();
            DataRow myNewRow;
            DataTable myTable;


            if (excelApp == null)
            {
                Console.WriteLine("Excel is not installed!!");
                return "Doesn't Exist QueueName!";
            }

            //
            ExcelApp.Workbook excelBook = excelApp.Workbooks.Open(String.Concat(projectPath, @"\Data\", "Config.xlsx"));
            ExcelApp._Worksheet excelSheet = excelBook.Sheets[1];
            ExcelApp.Range excelRange = excelSheet.UsedRange;

            int rows = excelRange.Rows.Count;
            int cols = excelRange.Columns.Count;

            //Set DataTable Name and Columns Name
            myTable = new DataTable("MyDataTable");
            myTable.Columns.Add("Name", typeof(string));
            myTable.Columns.Add("Value", typeof(string));



            //first row using for heading, start second row for data
            for (int i = 2; i <= 6; i++)
            {
                myNewRow = myTable.NewRow();
                myNewRow["Name"] = excelRange.Cells[i, 1].Value2 == null ? "" : excelRange.Cells[i, 1].Value2.ToString(); //string
                myNewRow["Value"] = excelRange.Cells[i, 2].Value2 == null ? "" : excelRange.Cells[i, 2].Value2.ToString(); //string           

                myTable.Rows.Add(myNewRow);
            }

            excelApp.Workbooks.Close();
            foreach( DataRow item in myTable.Rows)
            {
                if (item.ItemArray[0].ToString().Equals("OrchestratorQueueName"))
                {
                    queueName = item.ItemArray[1].ToString();
                    break;
                }

            }
            return queueName;
        }
    }
}
