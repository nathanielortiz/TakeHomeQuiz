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
    public class TotalCountController : Controller
    {
        public List<GradesModel> GetTotalSubject()
        {
            var list = new List<GradesModel>();
            using (SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
            {
                con.Open();
                string query = "SELECT COUNT(GradeID) FROM Grades";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new GradesModel
                            {
                                CourseID = int.Parse(dr["CourseID"].ToString())
                            });
                        }
                    }

                }
            }
            return list;
        }
        //public List<CourseModel> GetTotalCourses()
        //{
        //    var list = new List<CourseModel>();
        //    using (SqlConnection con = new SqlConnection(Dekomori.GetConnection()))
        //    {
        //        con.Open();
        //        string query = "SELECT COUNT(CourseID) FROM Courses";
        //        using(SqlCommand com = new SqlCommand(query, con))
        //        {
        //            using(SqlDataReader dr = com.ExecuteReader())
        //            {
        //                while (dr.Read())
        //                {
        //                    list.Add(new CourseModel
        //                    {
        //                        CourseID = int.Parse(dr["CourseID"].ToString())
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    return list;
        //}
        // GET: TotalCount
        public ActionResult Index()
        {
            //var list = new CourseAndGradeViewModel();
            ////list.CoursesM = GetTotalCourses();
            //list.GradesM = GetTotalSubject();
            return View();
        }
    }
}