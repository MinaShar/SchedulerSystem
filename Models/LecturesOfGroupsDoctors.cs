using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace Models
{
    public class LecturesOfGroupsDoctors
    {
        public int Id { get; set; }
        public int LecturesOfGroupsId { get; set; }
        public int DoctorId { get; set; }

        public static void AssignDoctorToLecture(int LectureOfGroupId,int DoctorId)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format("INSERT INTO LecturesOfGroupsDoctors(LecturesOfGroupsId,DoctorId) VALUES({0},{1});", LectureOfGroupId,DoctorId);
                using (SqlCommand cmd = new SqlCommand(form))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static List<Doctor> GetDoctorsOfLecturePeriod(int LectureOfGroupID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format("SELECT * FROM LecturesOfGroupsDoctors WHERE LecturesOfGroupsId={0};",LectureOfGroupID);
                using (SqlCommand cmd = new SqlCommand(form))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        adabter.Fill(dt);
                    }
                }
            }

            List<Doctor> DoctorsList = new List<Doctor>();

            foreach (DataRow item in dt.Rows)
            {

                DoctorsList.Add(
                    Doctor.GetDoctorByID(int.Parse(item["DoctorId"].ToString()))
                    );
            }

            return DoctorsList;

        }

    }
}
