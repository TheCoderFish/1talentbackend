using Microsoft.EntityFrameworkCore;
using OneTalentResignation.DTO.DomainModel;

namespace OneTalentResignation.DAL.Context
{
    public class ResignationContext : DbContext
    {

        public ResignationContext(DbContextOptions<ResignationContext> options) : base(options)
        {

        }

        public DbSet<ConcernEmployee> ConcernEmployees { get; set; }

        public DbSet<Designation> Designations { get; set; }

        public DbSet<Domain> Domains { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Resignation> Resignations { get; set; }

        public DbSet<Technology> Technologies { get; set; }
    }
}
