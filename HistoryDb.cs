
using Microsoft.EntityFrameworkCore;

public class EnrollmentHistoryContext : DbContext
{
    public DbSet<CourseSection> Snapshots => Set<CourseSection>();

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseMySQL(Environment.GetEnvironmentVariable("RDS_ACCESS_CREDENTIALS")!);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<CourseSection>()
            .HasKey(c => new { c.ClassNumber, c.Time });
        builder.Entity<CourseSection>()
            .Property(c => c.CourseCode)
            .HasColumnType("varchar(20)");
        builder.Entity<CourseSection>()
            .HasIndex(c => c.CourseCode);
    }
}

public class CourseDatabase
{
    public CourseDatabase()
    {
        using (var ctx = new EnrollmentHistoryContext())
        {
            ctx.Database.EnsureCreated();
        }
    }

    public async Task SaveCourses(IEnumerable<CourseSection> sections)
    {
        using (var ctx = new EnrollmentHistoryContext())
        {
            ctx.Snapshots.AddRange(sections);
            await ctx.SaveChangesAsync();
        }
    }
}