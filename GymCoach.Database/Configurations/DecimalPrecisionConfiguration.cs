using GymCoach.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymCoach.Database.Configurations;

public static class DecimalPrecisionConfiguration
{
    public static void ApplyDecimalPrecision(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Athlete>(e =>
        {
            e.Property(x => x.HeightCm).HasPrecision(6, 2);
            e.Property(x => x.WeightKg).HasPrecision(6, 2);
            e.Property(x => x.BodyFatPercentage).HasPrecision(5, 2);
        });

        modelBuilder.Entity<Measurement>(e =>
        {
            e.Property(x => x.Weight).HasPrecision(6, 2);
            e.Property(x => x.Chest).HasPrecision(6, 2);
            e.Property(x => x.Waist).HasPrecision(6, 2);
            e.Property(x => x.Arms).HasPrecision(6, 2);
            e.Property(x => x.Forearms).HasPrecision(6, 2);
            e.Property(x => x.Thighs).HasPrecision(6, 2);
            e.Property(x => x.Calves).HasPrecision(6, 2);
            e.Property(x => x.Neck).HasPrecision(6, 2);
            e.Property(x => x.Shoulders).HasPrecision(6, 2);
            e.Property(x => x.BodyFatPercentage).HasPrecision(5, 2);
        });

        modelBuilder.Entity<Assessment>(e =>
        {
            e.Property(x => x.HeightCm).HasPrecision(6, 2);
            e.Property(x => x.WeightKg).HasPrecision(6, 2);
        });

        modelBuilder.Entity<AthleteCoachRequest>(e =>
        {
            e.Property(x => x.HeightCm).HasPrecision(6, 2);
            e.Property(x => x.WeightKg).HasPrecision(6, 2);
        });

        modelBuilder.Entity<ProgressPhoto>(e =>
        {
            e.Property(x => x.Weight).HasPrecision(6, 2);
        });

        modelBuilder.Entity<MealItem>(e =>
        {
            e.Property(x => x.Quantity).HasPrecision(8, 2);
        });

        modelBuilder.Entity<PaymentOrder>(e =>
        {
            e.Property(x => x.Amount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<PaymentTransaction>(e =>
        {
            e.Property(x => x.Amount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<CoachEarning>(e =>
        {
            e.Property(x => x.Amount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<GymCommission>(e =>
        {
            e.Property(x => x.Amount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<Gym>(e =>
        {
            e.Property(x => x.CommissionRate).HasPrecision(5, 4);
        });

        modelBuilder.Entity<Coach>(e =>
        {
            e.Property(x => x.MaxPlanPrice).HasPrecision(18, 2);
        });

        modelBuilder.Entity<WorkoutPlan>(e =>
        {
            e.Property(x => x.Price).HasPrecision(18, 2);
        });

        modelBuilder.Entity<PersonalRecord>(e =>
        {
            e.Property(x => x.MaxWeight).HasPrecision(7, 2);
            e.Property(x => x.Estimated1Rm).HasPrecision(7, 2);
        });

        modelBuilder.Entity<WorkoutSessionItem>(e =>
        {
            e.Property(x => x.Weight).HasPrecision(7, 2);
        });
    }
}
