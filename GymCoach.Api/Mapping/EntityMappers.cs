using GymCoach.Database.Entities;
using GymCoach.Shared.Dtos;

namespace GymCoach.Api.Mapping;

public static class EntityMappers
{
    public static AthleteDto ToDto(Athlete athlete) => new()
    {
        Id = athlete.Id,
        FullName = $"{athlete.FirstName} {athlete.LastName}".Trim(),
        FullNameFa = $"{athlete.FirstName} {athlete.LastName}".Trim(),
        Email = athlete.Email,
        PhoneNumber = athlete.PhoneNumber,
        AvatarUrl = athlete.ProfilePhotoUrl,
        DateOfBirth = athlete.BirthDate,
        Status = athlete.Status,
        Goal = athlete.Goal,
        WeightKg = athlete.WeightKg
    };

    public static CoachDetailDto ToDto(Coach coach) => new()
    {
        Id = coach.Id,
        FirstName = coach.FirstName,
        LastName = coach.LastName,
        Email = coach.Email,
        PhoneNumber = coach.PhoneNumber,
        ApprovalStatus = coach.ApprovalStatus,
        Bio = coach.Bio
    };

    public static WorkoutPlanDetailDto ToDetailDto(WorkoutPlan plan) => new()
    {
        Id = plan.Id,
        NameEn = plan.NameEn,
        NameFa = plan.NameFa,
        DescriptionEn = plan.DescriptionEn,
        DescriptionFa = plan.DescriptionFa,
        Goal = plan.Goal,
        DurationWeeks = plan.DurationWeeks,
        DaysPerWeek = plan.DaysPerWeek,
        Price = plan.Price,
        Status = plan.Status,
        AthleteId = plan.AthleteId,
        CoachId = plan.CoachId,
        StartDate = plan.StartDate,
        EndDate = plan.EndDate,
        Days = plan.Days.OrderBy(d => d.Order).Select(d => new ProgramDayDto
        {
            Id = d.Id,
            NameEn = d.NameEn,
            NameFa = d.NameFa,
            Order = d.Order,
            Exercises = d.Exercises.OrderBy(e => e.Order).Select(e => new ProgramDayExerciseDto
            {
                Id = e.Id,
                ExerciseId = e.ExerciseId,
                ExerciseNameEn = e.Exercise?.NameEn ?? string.Empty,
                Sets = e.Sets,
                Reps = e.Reps,
                RestSeconds = e.RestSeconds,
                SupersetGroupId = e.SupersetGroupId,
                Order = e.Order
            }).ToList()
        }).ToList()
    };

    public static MeasurementRecordDto ToDto(Measurement m) => new()
    {
        Id = m.Id,
        AthleteId = m.AthleteId,
        Weight = m.Weight,
        Chest = m.Chest,
        Waist = m.Waist,
        Arms = m.Arms,
        Thighs = m.Thighs,
        Calves = m.Calves,
        Neck = m.Neck,
        BodyFatPercentage = m.BodyFatPercentage,
        MeasurementDate = m.MeasurementDate
    };

    public static ProgressPhotoDto ToDto(ProgressPhoto p) => new()
    {
        Id = p.Id,
        AthleteId = p.AthleteId,
        PhotoUrl = p.PhotoUrl,
        DateTaken = p.Date,
        Weight = p.Weight,
        Notes = p.Notes,
        Category = p.Category
    };

    public static PersonalRecordDto ToDto(PersonalRecord pr) => new()
    {
        Id = pr.Id,
        AthleteId = pr.AthleteId,
        ExerciseId = pr.ExerciseId,
        ExerciseNameEn = pr.Exercise?.NameEn ?? string.Empty,
        MaxWeight = pr.MaxWeight,
        MaxReps = pr.MaxReps,
        Estimated1Rm = pr.Estimated1Rm,
        AchievedDate = pr.AchievedDate
    };

    public static AthleteCoachRequestDto ToDto(AthleteCoachRequest r) => new()
    {
        Id = r.Id,
        AthleteId = r.AthleteId,
        CoachId = r.CoachId,
        CoachName = $"{r.Coach.FirstName} {r.Coach.LastName}".Trim(),
        AthleteName = $"{r.Athlete.FirstName} {r.Athlete.LastName}".Trim(),
        Goal = r.Goal,
        Experience = r.Experience,
        HeightCm = r.HeightCm,
        WeightKg = r.WeightKg,
        Measurements = r.Measurements,
        Diseases = r.Diseases,
        Medications = r.Medications,
        Supplements = r.Supplements,
        Notes = r.Notes,
        Status = r.Status,
        RejectionReason = r.RejectionReason,
        CreatedAt = r.CreatedAt
    };

    public static decimal CalculateEpley1Rm(decimal weight, int reps) =>
        reps <= 0 ? weight : weight * (1 + reps / 30m);
}
