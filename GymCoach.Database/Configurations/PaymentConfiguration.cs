using GymCoach.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymCoach.Database.Configurations;

public sealed class PaymentConfiguration : IEntityTypeConfiguration<PaymentOrder>,
    IEntityTypeConfiguration<PaymentTransaction>,
    IEntityTypeConfiguration<CoachEarning>,
    IEntityTypeConfiguration<GymCommission>,
    IEntityTypeConfiguration<SettlementBatch>
{
    public void Configure(EntityTypeBuilder<PaymentOrder> builder)
    {
        builder.HasOne(p => p.Athlete)
            .WithMany()
            .HasForeignKey(p => p.AthleteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.WorkoutPlan)
            .WithMany()
            .HasForeignKey(p => p.WorkoutPlanId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
    {
        builder.HasOne(t => t.PaymentOrder)
            .WithMany(o => o.Transactions)
            .HasForeignKey(t => t.PaymentOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public void Configure(EntityTypeBuilder<CoachEarning> builder)
    {
        builder.HasOne(e => e.Coach)
            .WithMany()
            .HasForeignKey(e => e.CoachId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.PaymentOrder)
            .WithMany()
            .HasForeignKey(e => e.PaymentOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.SettlementBatch)
            .WithMany(b => b.CoachEarnings)
            .HasForeignKey(e => e.SettlementBatchId)
            .OnDelete(DeleteBehavior.SetNull);
    }

    public void Configure(EntityTypeBuilder<GymCommission> builder)
    {
        builder.HasOne(c => c.Gym)
            .WithMany()
            .HasForeignKey(c => c.GymId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.PaymentOrder)
            .WithMany()
            .HasForeignKey(c => c.PaymentOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.SettlementBatch)
            .WithMany(b => b.GymCommissions)
            .HasForeignKey(c => c.SettlementBatchId)
            .OnDelete(DeleteBehavior.SetNull);
    }

    public void Configure(EntityTypeBuilder<SettlementBatch> builder)
    {
        builder.HasIndex(b => b.Status);
    }
}
