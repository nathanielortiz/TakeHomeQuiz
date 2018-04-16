using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TakeHomeQuiz.Models
{
    public class GradesModel
    {
        public int GradeID { get; set; }

        [Display(Name = "Student Name")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(51, ErrorMessage = "Invalid Input length.")]
        public string StudentName { get; set; }
    
        [Display(Name = "Courses")]
        [Required(ErrorMessage ="Select Course")]
        public int CourseID { get; set; }
        public List<CourseModel> Courses { get; set; }
        public string Course { get; set; }

        [Display(Name ="Grades")]
        [Required(ErrorMessage = "Required")]
        [Range(0.00, 4.0, ErrorMessage = "Error: Units must be between 1 and 4")]
        public decimal Grade { get; set; }
        public decimal Total { get; set; }


    }
}