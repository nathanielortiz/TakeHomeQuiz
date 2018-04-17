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
        //existing subject or not
       bool IsExisting(int CourseID)
        {
            using(SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
            {
                con.Open();
                //string query = "SELECT c.Code, g.CourseID FROM Grades as g INNER JOIN Courses c on c.CourseID =g.CourseID WHERE g.CourseID = @CID AND c.Code =@CD";
                string query = "SELECT CourseID FROM Grades WHERE CourseID=@CID";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@CID", CourseID);
                    //com.Parameters.AddWithValue("@CD", Code);
                    return com.ExecuteScalar() == null ? false : true;
                }

            }
        }
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
            //if (IsExisting(rec.CourseID))
            //{
            //    ViewBag.Error = "<div class='alert alert-danger'> Subject Already added </div>";
            //    return View();
            //}
            //else
            //{
                using (SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
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
            //}
        }


    
        double GetTotalUnits()
        {
            using(SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
            {
                con.Open();
                string query = @"Select SUM(c.Units) FROM courses c Inner JOIN grades g ON c.CourseID = g.CourseID";
                using(SqlCommand com = new SqlCommand(query, con))
                {
                   
                    return Convert.ToDouble((decimal)com.ExecuteScalar());
                }
            }
        }
        double GetTotalScore()
        {
            using (SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
            {
                con.Open();
                string query = @"SELECT SUM(g.Grade * c.Units) From Grades g Inner Join Courses c on c.CourseID = g.CourseID";
                using (SqlCommand com = new SqlCommand(query, con))
                {


                    return Convert.ToDouble((decimal)com.ExecuteScalar());
                }
            }
        }
        // GET: Grade
        public ActionResult ViewGrades()
        {
 
            var list = new List<GradesModel>();
            
            using(SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
            {
                con.Open();
                string query = @"SELECT g.CourseID, g.GradeID,c.Code,c.Name,g.Grade,c.Units FROM Grades AS g INNER JOIN Courses c ON g.CourseID = c.CourseID";
                
                using(SqlCommand com = new SqlCommand(query, con))
                {
                    using(SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new GradesModel
                            {
                                CourseID = int.Parse(dr["CourseID"].ToString()),
                                GradeID = int.Parse(dr["GradeID"].ToString()),
                                Code = dr["Code"].ToString(),
                                Name = dr["Name"].ToString(),
                                Grade = decimal.Parse(dr["Grade"].ToString()),
                                Units = dr["Units"].ToString()
                                

                            });
                        }
                        //ViewBag.TotalGrades = GetTotalGrades().ToString();
                        ViewBag.TotalUnits = GetTotalUnits().ToString("#.0");
                        ViewBag.TotalGPA =  GetTotalScore() /GetTotalUnits();


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
                    com.Parameters.AddWithValue("@GD", id);
                    com.ExecuteNonQuery();
                }
                return RedirectToAction("ViewGrades");
            }
        }
    }
}