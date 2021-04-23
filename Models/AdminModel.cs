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
    public class AdminModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }


        /// <summary>
        /// insert new admin and return its id
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static int InsertAdmin(string Name,string Password)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"INSERT INTO Admins (Name,Password) VALUES('{0}','{1}')
                                              SELECT SCOPE_IDENTITY() AS ID", Name, Password);
                using (SqlCommand cmd = new SqlCommand(form))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        object ob = cmd.ExecuteScalar();
                        return int.Parse(string.Format("{0}", ob));
                    }
                }
            }
        }

        public static AdminModel GetAdmin(int ID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format("SELECT * FROM Admins WHERE Id={0};", ID);
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

                var myEnumerable = dt.AsEnumerable();
                List<AdminModel> AdminsList =
                    (from item in myEnumerable
                     select new AdminModel
                     {
                         Id = item.Field<int>("Id"),
                         Name = item.Field<string>("Name"),
                         Password = item.Field<string>("Password")
                     }).ToList();
                return AdminsList[0];
            }
        }

    }
}
