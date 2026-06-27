using AssignementProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AssignementProject.Data;

/// <summary>
/// Represents the Entity Framework Core database context for the application.
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">The database context options.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the students table.
    /// </summary>
    public DbSet<Student> Students { get; set; }

    /// <summary>
    /// Gets or sets the qualifications table.
    /// </summary>
    public DbSet<Qualification> Qualifications { get; set; }

    /// <summary>
    /// Configures entity relationships and database constraints.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to configure entity mappings.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(student => student.StudentId);

            entity.HasIndex(student => student.StudentCode)
                .IsUnique();

            entity.Property(student => student.StudentCode)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(student => student.FirstName)
                .HasMaxLength(50);

            entity.Property(student => student.LastName)
                .HasMaxLength(50);

            entity.Property(student => student.Gender)
                .HasMaxLength(20);

            entity.Property(student => student.Email)
                .HasMaxLength(100);

            entity.Property(student => student.Phone)
                .HasMaxLength(15);

            entity.Property(student => student.Address)
                .HasMaxLength(250);

            entity.Property(student => student.Username)
                .HasMaxLength(50);

            entity.Property(student => student.Password)
                .HasMaxLength(100);

            entity.Property(student => student.ProfileImagePath)
                .HasMaxLength(250);

            entity.HasMany(student => student.Qualifications)
                .WithOne(qualification => qualification.Student)
                .HasForeignKey(qualification => qualification.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Qualification>(entity =>
        {
            entity.HasKey(qualification => qualification.QualificationId);

            entity.Property(qualification => qualification.CourseName)
                .HasMaxLength(100);

            entity.Property(qualification => qualification.University)
                .HasMaxLength(100);

            entity.Property(qualification => qualification.Percentage)
                .HasPrecision(5, 2);
        });
    }
}
