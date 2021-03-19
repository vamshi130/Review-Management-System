using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OrganisationEditing.Models
{
    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee
    {
    }
    [MetadataType(typeof(OrganizationMetadata))]
    public partial class Organization
    {
    }
    [MetadataType(typeof(ReviewCreationMetadata))]
    public partial class ReviewCreation
    {

    }
}