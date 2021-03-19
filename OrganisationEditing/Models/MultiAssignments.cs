using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganisationEditing.Models
{
    public class MultiAssignments
    {
        public int ReviewId { get; set; }
        public int [] EmployeeId { get; set; }
        public int Reviewer { get; set; }
        public int id { get; set; }
        public int OrganisationId { get; set; }
        public bool Status { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Employee Employee1 { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ReviewCreation ReviewCreation { get; set; }
    }
}