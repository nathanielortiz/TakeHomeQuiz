using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TakeHomeQuiz.Models
{
    public class CourseModel
    {
       
        public int CourseID { get; set; }

        [Display(Name = "Code")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(7, ErrorMessage ="Invalid Input length.")]
        public string Code { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(51, ErrorMessage ="Invalid Input length.")]
        public string Name { get; set; }

        [Display(Name = "Units")]
        [Required(ErrorMessage = "Required")]
        [Range(1.00,6.0,ErrorMessage ="Error: Units must be between 1 and 4")]
        public decimal Units { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }


    }
}