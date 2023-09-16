using API_Host.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Host.Services;

public class PolyclinicContext : DbContext
{
    private readonly IConfiguration _configuration;

    public PolyclinicContext (IConfiguration configuration)
    {
        _configuration = configuration;

        Database.EnsureCreated();
    }

    public virtual DbSet<Cabinet> Cabinets { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<ScheduleStatus> ScheduleStatuses { get; set; }

    public virtual DbSet<Speciality> Specialities { get; set; }

    protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:Home"]);

    protected override void OnModelCreating (ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cabinet>(entity => {
            entity.HasKey(e => e.Number);

            entity.ToTable("Cabinet");

            entity.Property(e => e.Number).ValueGeneratedNever();
        });

        modelBuilder.Entity<Client>(entity => {
            entity.ToTable("Client");

            entity.Property(e => e.ID)
                  .HasMaxLength(36)
                  .IsUnicode(false)
                  .IsFixedLength()
                  .HasColumnName("ID");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.SecondName).HasMaxLength(50);
        });

        modelBuilder.Entity<Doctor>(entity => {
            entity.ToTable("Doctor");

            entity.Property(e => e.ID)
                  .HasMaxLength(36)
                  .IsUnicode(false)
                  .IsFixedLength()
                  .HasColumnName("ID");

            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.SecondName).HasMaxLength(50);
            entity.Property(e => e.SpecialityId).HasColumnName("SpecialityID");

            entity.HasOne(d => d.Cabinet)
                  .WithMany()
                  .HasForeignKey(d => d.CabinetNumber)
                  .HasConstraintName("FK_Doctor_Cabinet");

            entity.HasOne(d => d.Speciality)
                  .WithMany(p => p.Doctors)
                  .HasForeignKey(d => d.SpecialityId)
                  .HasConstraintName("FK_Doctor_Speciality");
        });

        modelBuilder.Entity<Schedule>(entity => {
            entity.ToTable("Schedule");

            entity.Property(e => e.ID)
                  .HasMaxLength(36)
                  .IsUnicode(false)
                  .IsFixedLength()
                  .HasColumnName("ID");
            entity.Property(e => e.ClientId)
                  .HasMaxLength(36)
                  .IsUnicode(false)
                  .IsFixedLength()
                  .HasColumnName("ClientID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.DoctorId)
                  .HasMaxLength(36)
                  .IsUnicode(false)
                  .IsFixedLength()
                  .HasColumnName("DoctorID");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.Client).WithMany(p => p.Schedules)
                  .HasForeignKey(d => d.ClientId)
                  .HasConstraintName("FK_Schedule_Client");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Schedules)
                  .HasForeignKey(d => d.DoctorId)
                  .HasConstraintName("FK_Schedule_Doctor");

            entity.HasOne(d => d.Status).WithMany()
                  .HasForeignKey(d => d.StatusId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_Schedule_ScheduleStatus");
        });

        modelBuilder.Entity<ScheduleStatus>(entity => {
            entity.ToTable("ScheduleStatus");

            entity.Property(e => e.ID).HasColumnName("ID");
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Speciality>(entity => {
            entity.ToTable("Speciality");

            entity.Property(e => e.ID).HasColumnName("ID");
            entity.Property(e => e.Title)
                  .HasMaxLength(50)
                  .HasColumnName("TItle");
        });

        SeedData(modelBuilder);
    }

    private static void SeedData (ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Speciality>().HasData(new Speciality() {
            ID = 1,
            Title = "Хирург"
        }, new Speciality() {
            ID = 2,
            Title = "Стоматолог"
        }, new Speciality() {
            ID = 3,
            Title = "Терапевт"
        });

        modelBuilder.Entity<Cabinet>().HasData(new Cabinet() {
            Number = 41
        }, new Cabinet() {
            Number = 62
        }, new Cabinet() {
            Number = 12
        }, new Cabinet() {
            Number = 1
        });

        modelBuilder.Entity<ScheduleStatus>().HasData(new ScheduleStatus() {
            ID = 1,
            Status = "Ожидание приёма"
        }, new ScheduleStatus() {
            ID = 2,
            Status = "Приём оказан"
        }, new ScheduleStatus() {
            ID = 3,
            Status = "Приём не оказан"
        });
    }
}