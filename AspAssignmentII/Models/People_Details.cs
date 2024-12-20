using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AspAssignmentII.Models
{
    public class People_Details
    {
        [Required(ErrorMessage = "id is required.")]
        public int id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string username { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string name { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string gender { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string address { get; set; }
        
        }
    }