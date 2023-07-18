using LeaveManagement.Domain;
using LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Persistence.DatabaseContext;

public class LeaveDatabaseContext : DbContext
{
    public LeaveDatabaseContext(DbContextOptions<LeaveDatabaseContext> options) : base(options)
    {

    }

    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LeaveDatabaseContext).Assembly);
        
        #region Comment
        //modelBuilder.ApplyConfiguration(new LeaveTypeConfiguration());

        //Moved to Configuration
        //modelBuilder.Entity<LeaveType>().HasData(
        //{
        //    new LeaveType
        //    {
        //        Id = 1,
        //        Name = "Vacation",
        //        DefaultDays = 10,
        //        DateCreated = DateTime.Now,
        //        DateModified = DateTime.Now

        //    };

        //}; 
        #endregion

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
            .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        {
            entry.Entity.DateModified = DateTime.Now;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.Now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

}



