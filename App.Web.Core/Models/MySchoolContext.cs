using Microsoft.EntityFrameworkCore;

namespace App.Web.Core
{
    public class MySchoolContext : DbContext
    {
        public MySchoolContext(DbContextOptions<MySchoolContext> options) : base(options)
        {
           
        }

		public DbSet<Company> Company { get; set; }

		public DbSet<Constellation> Constellation { get; set; }

		public DbSet<Grade> Grade { get; set; }

		public DbSet<Student> Student { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<Constellation>().ToTable("Constellation");
            modelBuilder.Entity<Grade>().ToTable("Grade");
            modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}
