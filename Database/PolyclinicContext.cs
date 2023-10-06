using Database.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;
using Models.IDs;

namespace Database;

public abstract class PolyclinicContext : DbContext
{
    public DbSet<Cabinet> Cabinets { get; set; }

    public DbSet<Client> Clients { get; set; }

    public DbSet<Doctor> Doctors { get; set; }

    public DbSet<Schedule> Schedules { get; set; }

    public DbSet<ScheduleStatus> ScheduleStatuses { get; set; }

    public DbSet<Speciality> Specialities { get; set; }

    protected override void OnModelCreating (ModelBuilder modelBuilder)
    {
        #region Creating converters for IDs
        var cabNumberConverter = new ValueConverter<CabinetNumber, int>(id => id.Value, value => new(value));
        var doctodIDConverter = new ValueConverter<DoctorID, Guid>(id => id.Value, value => new(value));
        var clientIDConverter = new ValueConverter<ClientID, Guid>(id => id.Value, value => new(value));
        var specialityIDConverter = new ValueConverter<SpecialityID, Guid>(id => id.Value, value => new(value));
        var scheduleIDConverter = new ValueConverter<ScheduleID, Guid>(id => id.Value, value => new(value));
        var scheduleStatusIDConverter = new ValueConverter<ScheduleStatusID, int>(id => id.Value, value => new(value));
        #endregion

        modelBuilder.Entity<Cabinet>(entity => {
            entity.HasKey(e => e.Number);

            entity.ToTable("Cabinet");

            entity.Property(e => e.Number)
                  .HasConversion(cabNumberConverter)
                  .ValueGeneratedNever();
        });

        modelBuilder.Entity<Client>(entity => {
            entity.ToTable("Client");

            entity.Property(e => e.ID)
                  .HasConversion(clientIDConverter)
                  .ValueGeneratedNever()
                  .HasColumnName("ID");

            entity.Property(e => e.Login)       .HasMaxLength(25);
            entity.Property(e => e.Email)       .HasMaxLength(50);
            entity.Property(e => e.LastName)    .HasMaxLength(50);
            entity.Property(e => e.Name)        .HasMaxLength(50);
            entity.Property(e => e.Password)    .HasMaxLength(50);
            entity.Property(e => e.Patronymic)  .HasMaxLength(50);
        });

        modelBuilder.Entity<Doctor>(entity => {
            entity.ToTable("Doctor");

            entity.HasIndex(e => e.CabinetNumber, "IX_Doctor_CabinetNumber");

            entity.HasIndex(e => e.SpecialityId, "IX_Doctor_SpecialityID");

            entity.Property(e => e.ID)
                  .HasConversion(doctodIDConverter)
                  .ValueGeneratedNever()
                  .HasColumnName("ID");

            entity.Property(e => e.LastName)    .HasMaxLength(50);
            entity.Property(e => e.Name)        .HasMaxLength(50);
            entity.Property(e => e.Patronymic)  .HasMaxLength(50);

            entity.Property(e => e.SpecialityId)
                  .HasConversion(specialityIDConverter)
                  .HasColumnName("SpecialityID");

            #region Navigation properties
            entity.HasOne(d => d.Cabinet)
                  .WithMany()
                  .HasForeignKey(d => d.CabinetNumber)
                  .HasConstraintName("FK_Doctor_Cabinet");

            entity.HasOne(d => d.Speciality)
                  .WithMany(p => p.Doctors)
                  .HasForeignKey(d => d.SpecialityId)
                  .HasConstraintName("FK_Doctor_Speciality");
            #endregion
        });

        modelBuilder.Entity<Schedule>(entity => {
            entity.ToTable("Schedule");

            entity.HasIndex(e => e.ClientId, "IX_Schedule_ClientID");

            entity.HasIndex(e => e.DoctorId, "IX_Schedule_DoctorID");

            entity.HasIndex(e => e.StatusId, "IX_Schedule_StatusID");

            entity.Property(e => e.ID)
                  .HasConversion(scheduleIDConverter)
                  .ValueGeneratedNever()
                  .HasColumnName("ID");

            entity.Property(e => e.Date)
                  .HasColumnType("datetime");

            entity.Property(e => e.ClientId)
                  .HasConversion(clientIDConverter)
                  .HasColumnName("ClientID");

            entity.Property(e => e.DoctorId)
                  .HasConversion(doctodIDConverter)
                  .HasColumnName("DoctorID");

            entity.Property(e => e.StatusId)
                  .HasConversion(scheduleStatusIDConverter)
                  .HasColumnName("StatusID");

            #region Navigation properties
            entity.HasOne(d => d.Client)
                  .WithMany(p => p.Schedules)
                  .HasForeignKey(d => (Guid)d.ClientId!)
                  .HasConstraintName("FK_Schedule_Client");

            entity.HasOne(d => d.Doctor)
                  .WithMany(p => p.Schedules)
                  .HasForeignKey(d => (Guid)d.DoctorId!)
                  .HasConstraintName("FK_Schedule_Doctor");

            entity.HasOne(d => d.Status)
                  .WithMany()
                  .HasForeignKey(d => (int)d.StatusId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_Schedule_ScheduleStatus");
            #endregion
        });

        modelBuilder.Entity<ScheduleStatus>(entity => {
            entity.ToTable("ScheduleStatus");

            entity.Property(e => e.ID)
                  .HasConversion(scheduleStatusIDConverter)
                  .HasColumnName("ID");

            entity.Property(e => e.Status)
                  .HasMaxLength(50);
        });

        modelBuilder.Entity<Speciality>(entity => {
            entity.ToTable("Speciality");

            entity.Property(e => e.ID!)
                  .HasConversion(specialityIDConverter)
                  .ValueGeneratedNever()
                  .HasColumnName("ID");

            entity.Property(e => e.Title)
                  .HasMaxLength(50);
        });

        DBSeeder.SeedData(modelBuilder);
    }
}