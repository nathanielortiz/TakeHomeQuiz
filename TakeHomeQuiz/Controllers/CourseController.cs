using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using TakeHomeQuiz.Models;
using TakeHomeQuiz.App_Code;


namespace TakeHomeQuiz.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult ViewCourses()
        {
            var list = new List<CourseModel>();
            using(SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
            {
                con.Open();
                string query = "SELECT CourseID, Code, Name, Units, DateAdded FROM Courses";
                using(SqlCommand com = new SqlCommand(query, con))
                {
                    using(SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new CourseModel
                            {
                                CourseID = int.Parse(dr["CourseID"].ToString()),
                                Code = dr["Code"].ToString(),
                                Name = dr["Name"].ToString(),
                                Units = decimal.Parse(dr["Units"].ToString()),
                                DateAdded = DateTime.Parse(dr["DateAdded"].ToString())
                            });
                        }
                    }
                }
            }
            return View(list);
        }
        public ActionResult AddCourse()
        {
            CourseModel coursemodel = new CourseModel();
            return View(coursemodel);
        }
        [HttpPost]
        public ActionResult AddCourse(CourseModel rec)
        {
            using (SqlConnection kek = new SqlConnection(Dekomori.GetConnection()))
            {
                kek.Open();
                string lel = @"INSERT INTO Courses VALUES (@Code,@Name,@Units,@DateAdded)";
                using (SqlCommand ehe = new SqlCommand(lel, kek))
                {
                    ehe.Parameters.AddWithValue("@Code", rec.Code);
                    ehe.Parameters.AddWithValue("@Name", rec.Name);
                    ehe.Parameters.AddWithValue("@Units", rec.Units);
                    ehe.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                    ehe.ExecuteNonQuery();
                    return RedirectToAction("ViewCourses");
                }
            }
        }

        public ActionResult DeleteCourse(int? id)
        {
            if (id == null)
                return RedirectToAction("ViewCourses");
            using(SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
            {
                con.Open();
                string del = @"DELETE FROM Courses WHERE CourseID=@CID";
                using(SqlCommand com = new SqlCommand(del, con))
                {
                    com.Parameters.AddWithValue("@CID", id);
                    com.ExecuteNonQuery();
                }
            }
            return RedirectToAction("ViewCourses");
        }
    }
}