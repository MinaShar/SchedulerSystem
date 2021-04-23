using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using System.Web.UI.HtmlControls;

namespace SSP.Instructor
{
    public partial class TimeTable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int InstructorID = int.Parse(Session["instructorID"].ToString());
            Dictionary<string, HtmlTableCell> AllTableCells = new Dictionary<string, HtmlTableCell>();
            AllTableCells = this.CreateTableCells(AllTableCells);
            List<LecturesOfGroups> TimeTable = LecturesOfGroups.GetDoctorTimeTable(InstructorID);
            foreach(LecturesOfGroups x in TimeTable)
            {
                int day = x.PeriodDay;
                int period = x.PeriodNumber;
                string q = string.Format("{0}_{1}", day, period);
                HtmlTableCell Cell = AllTableCells[q];
                if (x.PeriodType == 1)
                {
                    Cell.BgColor = "antiquewhite";
                    Cell.InnerHtml = "Lecture<br />";
                }
                else if (x.PeriodType == 2)
                {
                    Cell.BgColor = "aquamarine";
                    Cell.InnerHtml = "Section<br />";
                }
                else if (x.PeriodType == 3)
                {
                    Cell.BgColor = "darkgoldenrod";
                    Cell.InnerHtml = "Lab<br />";
                }
                Cell.InnerHtml += x.CourseName + "<br />";
                Cell.InnerHtml += x.Place;

            }
        }

        public Dictionary<string, HtmlTableCell> CreateTableCells(Dictionary<string, HtmlTableCell> TableCells)
        {
            TableCells.Add("1_1",P1_1);
            TableCells.Add("2_1", P2_1);
            TableCells.Add("3_1", P3_1);
            TableCells.Add("4_1", P4_1);
            TableCells.Add("5_1", P5_1);
            TableCells.Add("6_1", P6_1);

            TableCells.Add("1_2", P1_2);
            TableCells.Add("2_2", P2_2);
            TableCells.Add("3_2", P3_2);
            TableCells.Add("4_2", P4_2);
            TableCells.Add("5_2", P5_2);
            TableCells.Add("6_2", P6_2);

            TableCells.Add("1_3", P1_3);
            TableCells.Add("2_3", P2_3);
            TableCells.Add("3_3", P3_3);
            TableCells.Add("4_3", P4_3);
            TableCells.Add("5_3", P5_3);
            TableCells.Add("6_3", P6_3);

            TableCells.Add("1_4", P1_4);
            TableCells.Add("2_4", P2_4);
            TableCells.Add("3_4", P3_4);
            TableCells.Add("4_4", P4_4);
            TableCells.Add("5_4", P5_4);
            TableCells.Add("6_4", P6_4);

            TableCells.Add("1_5", P1_5);
            TableCells.Add("2_5", P2_5);
            TableCells.Add("3_5", P3_5);
            TableCells.Add("4_5", P4_5);
            TableCells.Add("5_5", P5_5);
            TableCells.Add("6_5", P6_5);

            return TableCells;
        }
    }
}