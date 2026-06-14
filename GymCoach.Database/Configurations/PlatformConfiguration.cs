using GymCoach.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymCoach.Database.Configurations;

public sealed class PlatformConfiguration : IEntityTypeConfiguration<Gym>,
    IEntityTypeConfiguration<GymCoachLink>,
    IEntityTypeConfiguration<GymAthlete>,
    IEntityTypeConfiguration<PaymentOrder>,
    IEntityTypeConfiguration<AthleteCoachRequest>,
    IEntityTypeConfiguration<Coach>,
    IEntityTypeConfiguration<WorkoutPlan>,
    IEntityTypeConfiguration<Notification>,
    IEntityTypeConfiguration<PlatformSetting>
{
    public void Configure(EntityTypeBuilder<Gym> builder)
    {
        builder.Property(g => g.Name).HasMaxLength(200);
        builder.Property(g => g.CommissionRate).HasPrecision(5, 4);
        builder.HasIndex(g => g.Name);
    }

    public void Configure(EntityTypeBuilder<GymCoachLink> builder)
    {
        builder.HasIndex(x => new { x.GymId, x.CoachId }).IsUnique();
        builder.HasIndex(x => x.CoachId);
    }

    public void Configure(EntityTypeBuilder<GymAthlete> builder)
    {
        builder.HasIndex(x => new { x.GymId, x.AthleteId }).IsUnique();
        builder.HasIndex(x => x.AthleteId);
    }

    public void Configure(EntityTypeBuilder<PaymentOrder> builder)
    {
        builder.Property(p => p.Amount).HasPrecision(18, 2);
        builder.HasIndex(p => p.AthleteId);
        builder.HasIndex(p => p.WorkoutPlanId);
        builder.HasIndex(p => p.Status);
    }

    public void Configure(EntityTypeBuilder<AthleteCoachRequest> builder)
    {
        builder.HasIndex(r => r.CoachId);
        builder.HasIndex(r => r.AthleteId);
        builder.HasIndex(r => r.Status);
        builder.Property(r => r.RejectionReason).HasMaxLength(1000);
    }

    public void Configure(EntityTypeBuilder<Coach> builder)
    {
        builder.HasIndex(c => c.UserId);
        builder.HasIndex(c => c.PhoneNumber);
        builder.HasIndex(c => c.ApprovalStatus);
        builder.Property(c => c.MaxPlanPrice).HasPrecision(18, 2);
    }

    public void Configure(EntityTypeBuilder<WorkoutPlan> builder)
    {
        builder.Property(p => p.Price).HasPrecision(18, 2);
        builder.HasIndex(p => p.CoachId);
        builder.HasIndex(p => p.AthleteId);
        builder.HasIndex(p => p.Status);
    }

    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasIndex(n => n.UserId);
        builder.HasIndex(n => new { n.UserId, n.IsRead });
    }

    public void Configure(EntityTypeBuilder<PlatformSetting> builder)
    {
        builder.HasIndex(s => s.Key).IsUnique();
    }
}
