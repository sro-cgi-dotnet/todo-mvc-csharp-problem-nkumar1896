using Microsoft.EntityFrameworkCore;
namespace EFCoreTutorials
{
    public class KeepContext : DbContext
    {
        public DbSet<Student> Stu { get; set; }
         public DbSet<Label> Lab { get; set; }
        public DbSet<CheckList> Check { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=KEEPDB13;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Student>().HasMany(n => n.labels).WithOne().HasForeignKey(c => c.StudentId);
            modelBuilder.Entity<Student>().HasMany(n => n.checkLists).WithOne().HasForeignKey(c => c.StudentId);
        }  
    }
}