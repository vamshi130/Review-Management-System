//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrganisationEditing.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.ReviewAssignments = new HashSet<ReviewAssignment>();
            this.ReviewAssignments1 = new HashSet<ReviewAssignment>();
            this.ReviewSubmissions = new HashSet<ReviewSubmission>();
            this.ReviewSubmissions1 = new HashSet<ReviewSubmission>();
        }
    
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public System.DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public System.DateTime Date_of_joining { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public string Employee_Type { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
        public System.DateTime Created_On { get; set; }
        public string Role { get; set; }
        public string Created_by { get; set; }
    
        public virtual Organization Organization { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReviewAssignment> ReviewAssignments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReviewAssignment> ReviewAssignments1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReviewSubmission> ReviewSubmissions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReviewSubmission> ReviewSubmissions1 { get; set; }
    }
}
