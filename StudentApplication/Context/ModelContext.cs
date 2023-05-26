using Microsoft.EntityFrameworkCore;
using StudentApplication.Model;
using StudentApplication.Service;

namespace StudentApplication.Context
{
    public class ModelContext:DbContext
    {
        public ModelContext(DbContextOptions<ModelContext> options):base(options)
        {
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<LogInSevice>(eb =>
        //    {
        //        eb.HasNoKey();
        //        eb.ToView("ViewLoginUser");
        //        eb.Property(v=>v.UserName).HasColumnName("UserName");
        //    });
        //}
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<UserModel> LoginUser { get; set; }
    }
}
