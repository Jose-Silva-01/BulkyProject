using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Identity package in .Net already comes with a standard user table. However, we can implement it and generate new properties that we'd like
        [Required]
        public string Name { get; set; }    

        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

        [ForeignKey("CompanyId")]
        [ValidateNever] // we don't want to validate it because it will be assigned to a user after it's creation
        public int? CompanyId { get; set; }

    }
}
