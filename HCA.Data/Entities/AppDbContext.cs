
using HCA.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace WebAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) 
        {
            
        }
       
        public DbSet<HCA_EmployeeDetails> HCA_EmployeeDetails { get; set; }
        public DbSet<HCA_Status> HCA_Status { get; set; }
       
        public DbSet<TaskActivityMapping> TaskActivityMapping { get; set; }
        public DbSet<TaskDetails> TaskDetails { get; set; }

        public DbSet<TaskTagMapping> TaskTagMapping { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder );
            modelBuilder.Entity<HCA_Status>().HasData(new HCA_Status
            {
                Id = 1,
                StatusDescription = "Pending",
                IsActive = true,
                CreateDate = DateTime.Now,
                Createdby = "System",
                
               
            }
            );
            modelBuilder.Entity<HCA_Status>().HasData(new HCA_Status
            {
                Id = 2,
                StatusDescription = "Completed",
                IsActive = true,
                CreateDate = DateTime.Now,
                Createdby = "System",
               
            }
           );


            modelBuilder.Entity<HCA_EmployeeDetails>().HasData(new HCA_EmployeeDetails
            {
                Id = 1,
                Name="Altrocks1",
                Email="Ponnusamy.r@altrockstech.com"
            }

           );

            modelBuilder.Entity<HCA_EmployeeDetails>().HasData(new HCA_EmployeeDetails
            {
                Id = 2,
                Name = "Altrocks2",
                Email = "bharath.p@altrockstech.com"
            }

          );
           
        }
       }
}
