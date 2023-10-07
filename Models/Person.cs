using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{

    public enum Genter
    {
        other = 0,
        male = 1,
        female = 2
    }
    public class Person
    {
        public Guid ID { get; set; }
        [Display(Name = "Idnetity Card")]
        [Required(ErrorMessage = "TZ Card is missing")]
        [StringLength(20,ErrorMessage ="to long")]
        [TZValidation(ErrorMessage = "TZ Not Valid")]
        public string TZ { get; set; }
        [Required(ErrorMessage = "name is missing")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "full name should be between 5 and 30 characters")]
        [Display(Name = "name")]
        public string Name { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "email missing")]
        [EmailAddress(ErrorMessage ="invalid format for email")]
        [StringLength(100, ErrorMessage = "to long")]
        public string Email { get; set; }
        [Display(Name = "Birthday")]
        [Required(ErrorMessage = "Birthday is invalid")]
        public DateTime Birthday { get; set; }
        [Display(Name = "Gender")]
        public Genter Gender { get; set; }
        [StringLength(30, ErrorMessage = "to long")]
        [Display(Name = "Phone number")]
        [Required(ErrorMessage = "phone is missing")]
        [RegularExpression(@"^[0][5][0|2|3|4|5|9]{1}[]{0,1}[0-9]{7}$", ErrorMessage = "phone number is invalid")]
        public string Phone { get; set; }




    }
}