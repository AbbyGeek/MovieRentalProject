using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieRentalProject.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieRentalProject.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool isSubscribedToNewsletter { get; set; }
        public byte MembershipTypeId { get; set; }
        public MembershipTypeDto MembershipType { get; set; }
       // [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }
    }
}