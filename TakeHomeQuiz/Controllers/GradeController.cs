using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakeHomeQuiz.App_Code;
using TakeHomeQuiz.Models;
namespace TakeHomeQuiz.Controllers
{
    public class GradeController : Controller
    {
        public List<CourseModel> GetCourses()
        {
            var list = new List<CourseModel>();
            using(SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
            {
                con.Open();
                string query = @"SELECT CourseID, Code FROM Courses";
                using (SqlCommand com = new SqlCommand(query,con))
                {
                    using(SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new CourseModel
                            {
                                CourseID = int.Parse(dr["CatID"].ToString()),
                                Code = dr["Code"].ToString()
                            });
                        }
                    }
                }

            }
            return list;
        }
        public ActionResult AddGrades()
        {
            GradesModel rec = new GradesModel();
            rec.Courses = GetCourses();
            return View(rec);
        }
        [HttpPost]
        public ActionResult AddGrades(GradesModel rec)
        {
            using(SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
            {
                con.Open();
                string query = @"INSERT INTO Grades VALUES
                (@SN,@CourseID,@Grade)";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@SN", rec.StudentName);
                    com.Parameters.AddWithValue("@CourseID", rec.CourseID);
                    com.Parameters.AddWithValue("@Grade", rec.Grade);
                    com.ExecuteNonQuery();
                }
            }
            return RedirectToAction("ViewGrades");
        }

        // GET: Grade
        public ActionResult ViewGrades()
        {
            var list = new List<GradesModel>();
            using(SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
            {
                con.Open();
                string query = @"SELECT g.GradeID,c.Code,c.Name,g.Grade FROM Grades AS g INNER JOIN Courses c ON g.CourseID = c.CourseID";
                
                using(SqlCommand com = new SqlCommand(query, con))
                {
                    using(SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new GradesModel
                            {
                                GradeID = int.Parse(dr["GradeID"].ToString()),

                            });
                        }
                    }
                }
            }
            return View();
        }
    }
}