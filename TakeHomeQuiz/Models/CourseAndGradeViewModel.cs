using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TakeHomeQuiz.Models
{
    public class CourseAndGradeViewModel
    {
        public List<CourseModel> CoursesM { get; set; }
        public List<GradesModel> GradesM { get; set; }
    }
}