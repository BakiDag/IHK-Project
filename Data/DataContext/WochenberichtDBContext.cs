using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessEfCore.DataContext
{
    public partial class WochenberichtDBContext : DbContext
    {
        public WochenberichtDBContext()
        {
        }

        public WochenberichtDBContext(DbContextOptions<WochenberichtDBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Instructor> Instructors { get; set; } = null!;
        public virtual DbSet<Apprentice> Apprentices { get; set; } = null!;
        public virtual DbSet<Note> Notes { get; set; } = null!;
        public virtual DbSet<WeeklyReport> WeeklyReports { get; set; } = null!;
        public virtual DbSet<WeeklyReportPosition> WeeklyReportPositions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Admins
            modelBuilder.Entity<Admin>(entity =>
                {
                    entity.ToTable("Admins");
                    entity.HasKey(e => e.ID);                    
                    entity.Property(e => e.Email).HasColumnName("Email").HasMaxLength(50).IsRequired();
                    entity.HasIndex(e => e.Email).IsUnique();
                    entity.Property(e => e.Token).HasColumnName("Token").HasMaxLength(500).IsRequired();
                    entity.Property(e => e.Password).HasColumnName("Password").IsRequired();
                    entity.Property(e => e.UserName).HasColumnName("UserName").HasMaxLength(50);
                    entity.Property(e => e.FirstName).HasColumnName("FirstName").HasMaxLength(50);
                    entity.Property(e => e.LastName).HasColumnName("LastName").HasMaxLength(50);
                    entity.Property(e => e.DateEntry).HasColumnName("DateEntry").HasColumnType("datetime2");
                    entity.Property(e => e.Role).HasColumnName("Role").IsRequired();
                    entity.HasIndex(e => e.Email).IsUnique();
                });
            #endregion

            #region Instructor
            modelBuilder.Entity<Instructor>(entity =>
                {
                    entity.ToTable("Instructors");
                    entity.HasKey(e => e.ID);
                    entity.Property(e => e.Email).HasColumnName("Email").HasMaxLength(50).IsRequired();
                    entity.HasIndex(e => e.Email).IsUnique();
                    entity.Property(e => e.Password).HasColumnName("Password").IsRequired();
                    entity.Property(e => e.UserName).HasColumnName("UserName").HasMaxLength(50);
                    entity.Property(e => e.FirstName).HasColumnName("FirstName").HasMaxLength(50);
                    entity.Property(e => e.LastName).HasColumnName("LastName").HasMaxLength(50);
                    entity.Property(e => e.DateEntry).HasColumnName("DateEntry").HasColumnType("datetime2");
                    entity.Property(e => e.Role).HasColumnName("Role").IsRequired();
                    entity.Property(e => e.Token).HasColumnName("Token").HasMaxLength(500).IsRequired();
                    entity.HasMany(e => e.Notes)
                          .WithOne(e => e.Instructor)
                          .HasForeignKey(e => e.InstructorID)
                          .OnDelete(DeleteBehavior.Cascade);
                    entity.HasMany(e => e.WeeklyReports)
                          .WithOne(e => e.Instructor)
                          .HasForeignKey(e => e.InstructorID)
                          .OnDelete(DeleteBehavior.NoAction);
                    entity.HasMany(e => e.Apprentices)
                          .WithOne(e => e.Instructor)
                          .HasForeignKey(e => e.InstructorID)
                          .OnDelete(DeleteBehavior.Restrict);
                });
            #endregion

            #region Apprentice
            modelBuilder.Entity<Apprentice>(entity =>
                {
                    entity.ToTable("Apprentices");
                    entity.HasKey(e => e.ID);
                    entity.Property(e => e.Email).HasColumnName("Email").HasMaxLength(50).IsRequired();
                    entity.HasIndex(e => e.Email).IsUnique();
                    entity.Property(e => e.Password).HasColumnName("Password").IsRequired();
                    entity.Property(e => e.UserName).HasColumnName("UserName").HasMaxLength(50);
                    entity.Property(e => e.FirstName).HasColumnName("FirstName").HasMaxLength(50);
                    entity.Property(e => e.LastName).HasColumnName("LastName").HasMaxLength(50);
                    entity.Property(e => e.DateEntry).HasColumnName("DateEntry").HasColumnType("datetime2");
                    entity.Property(e => e.Role).HasColumnName("Role").IsRequired();
                    entity.Property(e => e.Token).HasColumnName("Token").HasMaxLength(500).IsRequired();
                    entity.Property(e => e.InstructorID).HasColumnName("InstructorID").IsRequired();
                    
                    //entity.HasOne(e => e.Instructor)
                    //      .WithMany(e => e.Apprentices)
                    //      .HasForeignKey(e => e.InstructorID)
                    //      .OnDelete(DeleteBehavior.Cascade);

                    //entity.HasMany(e => e.WeeklyReports)
                    //      .WithOne(e => e.Apprentice)
                    //      .HasForeignKey(e => e.ApprenticeID)
                    //      .OnDelete(DeleteBehavior.Cascade);
                    //entity.HasMany(e => e.WeeklyReportPositions)
                    //      .WithOne(e => e.apprentice)
                    //      .HasForeignKey(e => e.ApprenticeID)
                    //      .OnDelete(DeleteBehavior.Cascade);
                });
            #endregion

            #region Note
            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("Notes");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Comment).HasColumnName("Comment").HasMaxLength(350);
                entity.Property(e => e.InstructorID).HasColumnName("InstructorID").IsRequired();
                
                entity.Property(e => e.WeeklyReportPositionsID).HasColumnName("WeeklyReportPositionsID").IsRequired();
                //entity.HasOne(d => d.Instructor)
                //      .WithMany(p => p.Notes)
                //      .HasForeignKey(d => d.InstructorID)
                //      .OnDelete(DeleteBehavior.NoAction);
                //entity.HasOne(d => d.WeeklyReportPosition)
                //      .WithOne(p => p.Note)
                //      .HasForeignKey<Note>(d => d.WeeklyReportPositionsID)
                //      .OnDelete(DeleteBehavior.Restrict);
            });
            #endregion

            #region WeeklyReport
            modelBuilder.Entity<WeeklyReport>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.ToTable("WeeklyReport");

                entity.Property(e => e.CalenderWeek)
                .HasColumnName("CalenderWeek");

                entity.Property(e => e.DateFrom)
                      .HasColumnName("DateFrom")
                      .HasColumnType("date")
                      .IsRequired();
                entity.HasIndex(e => e.DateFrom).IsUnique();

                entity.Property(e => e.DateTo)
                      .HasColumnName("DateTo")
                      .HasColumnType("date")
                      .IsRequired();
                entity.HasIndex(e => e.DateTo).IsUnique();

                entity.Property(e => e.Page)
                      .HasColumnName("Page");

                entity.Property(e => e.StatusApprentice)
                      .HasColumnName("StatusApprentice").IsRequired();

                entity.Property(e => e.StatusInstructor)
                      .HasColumnName("StatusInstructor");
                

        entity.Property(e => e.SigningInstructor)
                      .HasColumnName("SigningInstructor")
                      .HasMaxLength(byte.MaxValue)
                      .HasColumnType("nvarchar(50)");

                entity.Property(e => e.SigningApprentice)
                      .HasColumnName("SigningApprentice")
                      .HasMaxLength(byte.MaxValue)
                      .HasColumnType("nvarchar(50)");

                entity.Property(e => e.SigningDateApprentice)
                      .HasColumnName("SigningDateApprentice")
                      .HasColumnType("datetime2");


                entity.Property(e => e.SigningDateInstructor)
                      .HasColumnName("SigningDateInstructor")
                      .HasColumnType("datetime2");
                

                //entity.HasOne(d => d.Apprentice)
                //      .WithMany(p => p.WeeklyReports)
                //      .HasForeignKey(d => d.ApprenticeID)
                //      .OnDelete(DeleteBehavior.Cascade);

                //entity.HasOne(d => d.Instructor)
                //      .WithMany(p => p.WeeklyReports)
                //      .HasForeignKey(d => d.InstructorID)
                //      .OnDelete(DeleteBehavior.NoAction);
            });
            #endregion

            #region WeeklyReportPosition
            modelBuilder.Entity<WeeklyReportPosition>(entity =>
            {
                entity.ToTable("WeeklyReportPositions");
                entity.HasKey(e => e.ID);

                entity.Property(e => e.DailyReport)
                      .HasColumnName("DailyReport")
                      .HasMaxLength(350)
                      .IsRequired();

                entity.Property(e => e.DailyHours)
                      .HasColumnName("DailyHours")
                      .IsRequired();

                entity.Property(e => e.Date)
                      .HasColumnName("Date");

                entity.Property(e => e.WeeklyReportID)
                        .HasColumnName("WeeklyReportID")
                        .IsRequired();
                        

                //entity.HasOne(d => d.WeeklyReport)
                //      .WithMany(p => p.WeeklyReportPositions)
                //      .HasForeignKey(d => d.WeeklyReportID)
                //      .IsRequired()                      
                //      .OnDelete(DeleteBehavior.Restrict); 
                    
                //entity.HasOne(d => d.apprentice)
                //      .WithMany(p => p.WeeklyReportPositions)
                //      .HasForeignKey(d => d.ApprenticeID)
                //      .IsRequired()
                //      .OnDelete(DeleteBehavior.Restrict);

            });
            #endregion

            #region Seeding
            modelBuilder.Entity<Admin>()
                .HasData(new Admin
                {
                    ID = 1,
                    FirstName = "Max",
                    LastName = "Mustermann",
                    Email = "admin@gmail.com",
                    Token = "",
                    UserName = "admin@gmail.com",
                    Role = Role.Admin,
                    DateEntry = new DateTime(2010, 8, 10),
                    Password = "12345678910!aA!"
                });

            modelBuilder.Entity<Instructor>()
                .HasData(new Instructor
                {
                    ID = 1,
                    FirstName = "Uwe",
                    LastName = "Meier",
                    Email = "ausbilder@gmail.com",
                    Token = "",
                    UserName = "ausbilder@gmail.com",
                    Role = Role.Ausbilder,
                    DateEntry = new DateTime(2011, 5, 10),
                    Password = "12345678910!aA!"
                });
            modelBuilder.Entity<Instructor>()
                .HasData(new Instructor
                {
                    ID = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "ausbilder2@gmail.com",
                    Token = "",
                    UserName = "ausbilder2@gmail.com",
                    Role = Role.Ausbilder,
                    DateEntry = new DateTime(2010, 2, 8),
                    Password = "12345678910!aA!"
                });

            modelBuilder.Entity<Apprentice>()
                .HasData(new Apprentice
                {
                    ID = 1,
                    FirstName = "Ruwen",
                    LastName = "Müller",
                    Email = "auszubildender@gmail.com",
                    Token = "",
                    UserName = "auszubildender@gmail.com",
                    Role = Role.Auszubildender,
                    DateEntry = new DateTime(20, 8, 1),
                    Password = "12345678910!aA!",
                    InstructorID = 1
                });

            modelBuilder.Entity<Apprentice>()
                .HasData(new Apprentice
                {
                    ID = 2,
                    FirstName = "Kevin",
                    LastName = "McCallister",
                    Email = "auszubildender2@gmail.com",
                    Token = "",
                    UserName = "auszubildender2@gmail.com",
                    Role = Role.Auszubildender,
                    DateEntry = new DateTime(2020, 8, 1),
                    Password = "12345678910!aA!",
                    InstructorID = 2
                });
            #endregion
        }
    }
}

