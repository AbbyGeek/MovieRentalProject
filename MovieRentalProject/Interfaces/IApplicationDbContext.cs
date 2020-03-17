using Microsoft.AspNet.Identity.EntityFramework;
using MovieRentalProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MovieRentalProject.Interfaces
{
    public interface IApplicationDbContext : IDisposable
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Movie> Movies { get; set; }
        DbSet<MembershipType> MembershipTypes { get; set; }
        DbSet<Genre> Genres { get; set; }

        int SaveChanges();
    }
}
