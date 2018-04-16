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
                                CourseID = int.Parse(dr["CourseID"].ToString()),
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
                (@SN,@CourseID,@Grade,@Total)";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@SN", rec.StudentName);
                    com.Parameters.AddWithValue("@CourseID", rec.CourseID);
                    com.Parameters.AddWithValue("@Grade", rec.Grade);
                    com.Parameters.AddWithValue("@Total", DBNull.Value);
                    com.ExecuteNonQuery();
                }
            }
            return RedirectToAction("ViewGrades");
        }

        //double GetTotalGrades()
        //{
        //    using (SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
        //    {
        //        con.Open();
        //        string query = @"SELECT SUM("
        //    }
        //}
        // GET: Grade
        public ActionResult ViewGrades()
        {
            var list = new List<GradesModel>();
            using(SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
            {
                con.Open();
                string query = @"SELECT g.GradeID,c.Code,c.Name,g.Grade,c.Units FROM Grades AS g INNER JOIN Courses c ON g.CourseID = c.CourseID";
                
                using(SqlCommand com = new SqlCommand(query, con))
                {
                    using(SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new GradesModel
                            {
                               
                                GradeID = int.Parse(dr["GradeID"].ToString()),
                                Code = dr["Code"].ToString(),
                                Name = dr["Name"].ToString(),
                                Grade = decimal.Parse(dr["Grade"].ToString()),
                                Units = dr["Units"].ToString()
                                

                            });
                        }
                    }
                }
            }
            return View(list);
        }
        public ActionResult DeleteGrades(int? id)
        {
            if (id == null)
                return RedirectToAction("ViewGrades");
            using(SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
            {
                con.Open();
                string del = @"DELETE FROM Grades WHERE GradeID=@GD";
                using(SqlCommand com = new SqlCommand(del, con))
                {
                    com.Parameters.AddWithValue("@CID", id);
                    com.ExecuteNonQuery();
                }
                return RedirectToAction("ViewGrades");
            }
        }
    }
}