using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OrganisationEditing.Models
{
    public class EmployeeMetadata
    {
        public int EmployeeId { get; set; }
        
        [Display(Name ="First Name")]
        [Required(ErrorMessage = "Field is required")]
        public string FirstName { get; set; }
        [Display(Name ="Last Name")]
        [Required(ErrorMessage = "Field is required")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Field is required")]
        public System.DateTime DOB { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string Designation { get; set; }
        [DataType(DataType.Date)]
        [Display(Name ="Join Date")]
        [Required(ErrorMessage = "Field is required")]
        public System.DateTime Date_of_joining { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string Department { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [Display(Name ="Employee Type")]
        public string Employee_Type { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [Display(Name ="Phone Numeber")]
        public long PhoneNumber { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string Password { get; set; }

        public int Id { get; set; }
        public System.DateTime Created_On { get; set; }
        public string Role { get; set; }

    }
    public class OrganizationMetadata
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string Name { get; set; }
        [Display(Name="Phone Number")]
       
        public long Phone_Number { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Field is required")]
        public string Email { get; set; }
        [Display(Name="User Name")]
        [Required(ErrorMessage = "Field is required")]
        [Remote("IsUserIdExist","Employee",ErrorMessage ="User name already exists")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string Password { get; set; }
        [Display(Name="Country")]
        [Required(ErrorMessage = "Field is required")]
        public int CountryId { get; set; }
        [Display(Name = "State")]
        [Required(ErrorMessage = "Field is required")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [Display(Name = "City")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public bool Status { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string Description { get; set; }

        public System.DateTime Created_On { get; set; }
        public string Role { get; set; }

        public virtual City City { get; set; }
        public virtual Country Country { get; set; }

    }
    public class ReviewCreationMetadata
    {
        public int ReviewId { get; set; }
        [Required(ErrorMessage ="Field is required")]
        public string Agenda { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Field is required")]
        public System.DateTime Review_Cycle_Start_Date { get; set; }
        [Display(Name ="End Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Field is required")]
        public System.DateTime Review_Cycle_End_Date { get; set; }
        [Display(Name ="Min Rating")]
        [Range(0,10)]
        [Required(ErrorMessage = "rating from 0 to 10")]
        public string MinimunRating { get; set; }
        [Display(Name ="Max Rating")]
        [Range(0,10)]
        [Required(ErrorMessage = "rating from 0 to 10")]
        public string MaximumRating { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }

    }
    public class SubmissionScreen
    {
        public int ReviewId { get; set; }
        public int EmployeeId { get; set; }
        [Range(0,10)]
        public string Rating { get; set; }
        [Required(ErrorMessage ="field is Required")]
        public string Feedback { get; set; }
        public int id { get; set; }
        public int ReviewerId { get; set; }
    }
}