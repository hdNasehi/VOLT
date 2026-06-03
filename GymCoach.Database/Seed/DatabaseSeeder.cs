using GymCoach.Database.Entities;
using GymCoach.Shared.Enums;
using GymCoach.Shared.Utilities;
using Microsoft.EntityFrameworkCore;

namespace GymCoach.Database.Seed;

public static class DatabaseSeeder
{
    public static readonly Guid DefaultCoachId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

    public static async Task SeedAsync(GymCoachDbContext db, CancellationToken cancellationToken = default)
    {
        if (await db.CoachAthletes.AnyAsync(cancellationToken))
        {
            return;
        }

        var coach = new Coach
        {
            Id = DefaultCoachId,
            FirstName = "Jordan",
            LastName = "Lee",
            Email = "coach@volt.app"
        };

        var exercises = CreateSampleExercises();
        db.Coaches.Add(coach);
        db.Exercises.AddRange(exercises);
        await db.SaveChangesAsync(cancellationToken);

        var athletes = CreateAthletes();
        db.Athletes.AddRange(athletes);
        await db.SaveChangesAsync(cancellationToken);

        foreach (var athlete in athletes)
        {
            db.CoachAthletes.Add(new CoachAthlete
            {
                Id = Guid.NewGuid(),
                CoachId = coach.Id,
                AthleteId = athlete.Id,
                Role = CoachAthleteRole.Fitness,
                IsActive = true,
                AssignedAt = DateTime.UtcNow.AddDays(-Random.Shared.Next(30, 120))
            });
        }

        SeedAssessments(db, athletes);
        SeedMeasurements(db, athletes);
        SeedProgressPhotos(db, athletes);
        SeedPrograms(db, athletes, coach.Id, exercises);
        SeedCoachNotes(db, athletes, coach.Id);
        SeedWorkoutSessions(db, athletes);

        await db.SaveChangesAsync(cancellationToken);
    }

    private static List<Athlete> CreateAthletes()
    {
        return
        [
            CreateAthlete("11111111-1111-1111-1111-111111111111", "Alex", "Rivera", "09121234567", AthleteStatus.Active,
                Gender.Male, FitnessGoal.Hypertrophy, 82.4m, 14.2m, "user-alex", 12),
            CreateAthlete("22222222-2222-2222-2222-222222222222", "Sam", "Chen", "09121234568", AthleteStatus.Active,
                Gender.Female, FitnessGoal.Strength, 68m, 18.5m, "user-sam", 5),
            CreateAthlete("33333333-3333-3333-3333-333333333333", "Riley", "Patel", "09121234569", AthleteStatus.OnHold,
                Gender.Other, FitnessGoal.FatLoss, 75m, 22m, "user-riley", 0),
            CreateAthlete("44444444-4444-4444-4444-444444444444", "Morgan", "Kim", "09121234570", AthleteStatus.Inactive,
                Gender.Female, FitnessGoal.FatLoss, 62m, 20m, "user-morgan", 0),
            CreateAthlete("55555555-5555-5555-5555-555555555555", "Jamie", "Okafor", "09121234571", AthleteStatus.Active,
                Gender.Male, FitnessGoal.Strength, 90m, 16m, "user-jamie", 8),
            CreateAthlete("66666666-6666-6666-6666-666666666666", "Taylor", "Nguyen", "09121234572", AthleteStatus.Active,
                Gender.Female, FitnessGoal.Hypertrophy, 58m, 19m, "user-taylor", 3),
            CreateAthlete("77777777-7777-7777-7777-777777777777", "Casey", "Brooks", "09121234573", AthleteStatus.Active,
                Gender.Male, FitnessGoal.Recomp, 78m, 17m, "user-casey", 1),
            CreateAthlete("88888888-8888-8888-8888-888888888888", "Pending", "Athlete", "09129998888", AthleteStatus.Placeholder,
                Gender.Other, FitnessGoal.Recomp, null, null, null, 0)
        ];
    }

    private static Athlete CreateAthlete(
        string id,
        string first,
        string last,
        string phone,
        AthleteStatus status,
        Gender gender,
        FitnessGoal goal,
        decimal? weight,
        decimal? bodyFat,
        string? userId,
        int streak)
    {
        return new Athlete
        {
            Id = Guid.Parse(id),
            FirstName = first,
            LastName = last,
            PhoneNumber = PhoneNormalizer.Normalize(phone),
            Email = status == AthleteStatus.Placeholder ? string.Empty : $"{first.ToLower()}@volt.app",
            UserId = userId,
            Status = status,
            Gender = gender,
            Goal = goal,
            WeightKg = weight,
            BodyFatPercentage = bodyFat,
            StreakDays = streak,
            BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-25)),
            HeightCm = gender == Gender.Female ? 165m : 178m
        };
    }

    private static void SeedAssessments(GymCoachDbContext db, List<Athlete> athletes)
    {
        foreach (var athlete in athletes.Where(a => a.Status != AthleteStatus.Placeholder))
        {
            db.Assessments.Add(new Assessment
            {
                Id = Guid.NewGuid(),
                AthleteId = athlete.Id,
                Age = 28,
                HeightCm = athlete.HeightCm ?? 175m,
                WeightKg = athlete.WeightKg ?? 75m,
                Goal = athlete.Goal,
                TrainingExperience = TrainingExperience.Intermediate,
                Injuries = "None reported",
                MedicalConditions = "None",
                AvailableEquipment = "Full gym access",
                TrainingDaysPerWeek = 4,
                CreatedAt = DateTime.UtcNow.AddDays(-14)
            });
        }
    }

    private static void SeedMeasurements(GymCoachDbContext db, List<Athlete> athletes)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        foreach (var athlete in athletes.Where(a => a.Status != AthleteStatus.Placeholder))
        {
            var baseWeight = athlete.WeightKg ?? 75m;
            for (var i = 7; i >= 0; i--)
            {
                var delta = (7 - i) * 0.2m;
                db.Measurements.Add(new Measurement
                {
                    Id = Guid.NewGuid(),
                    AthleteId = athlete.Id,
                    Weight = baseWeight - delta,
                    Chest = 100m - delta,
                    Waist = 82m - delta * 0.5m,
                    Arms = 36m + delta * 0.1m,
                    Forearms = 28m + delta * 0.05m,
                    Thighs = 58m + delta * 0.1m,
                    Calves = 38m + delta * 0.05m,
                    Shoulders = 118m + delta * 0.1m,
                    BodyFatPercentage = (athlete.BodyFatPercentage ?? 18m) - delta * 0.1m,
                    MeasurementDate = today.AddDays(-i * 7)
                });
            }
        }
    }

    private static void SeedProgressPhotos(GymCoachDbContext db, List<Athlete> athletes)
    {
        var categories = new[] { ProgressPhotoCategory.Front, ProgressPhotoCategory.Back, ProgressPhotoCategory.LeftSide };
        var activeAthletes = athletes.Where(a => a.Status == AthleteStatus.Active).Take(3).ToList();
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        foreach (var athlete in activeAthletes)
        {
            for (var i = 0; i < 2; i++)
            {
                db.ProgressPhotos.Add(new ProgressPhoto
                {
                    Id = Guid.NewGuid(),
                    AthleteId = athlete.Id,
                    Category = categories[i % categories.Length],
                    PhotoUrl = $"https://picsum.photos/seed/{athlete.Id:N}{i}/400/600",
                    Date = today.AddDays(-i * 14),
                    Weight = athlete.WeightKg,
                    Notes = i == 0 ? "Baseline" : "2-week check-in"
                });
            }
        }
    }

    private static void SeedPrograms(
        GymCoachDbContext db,
        List<Athlete> athletes,
        Guid coachId,
        List<Exercise> exercises)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var alex = athletes[0];
        var sam = athletes[1];
        var jamie = athletes[4];

        var activePlan = new WorkoutPlan
        {
            Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            NameEn = "Hypertrophy Block A",
            NameFa = "بلوک هیpertrophy A",
            DescriptionEn = "Push / Pull / Legs split",
            DescriptionFa = "اسplit فشار / کشش / پا",
            Goal = FitnessGoal.Hypertrophy,
            DurationWeeks = 8,
            Status = ProgramStatus.Active,
            AthleteId = alex.Id,
            CoachId = coachId,
            StartDate = today.AddDays(-21),
            EndDate = today.AddDays(35),
            Days =
            [
                CreateProgramDay("Push", "فشار", 1, exercises, [0, 3]),
                CreateProgramDay("Pull", "کشش", 2, exercises, [4, 6]),
                CreateProgramDay("Legs", "پا", 3, exercises, [1, 5])
            ]
        };

        var expiringPlan = new WorkoutPlan
        {
            Id = Guid.NewGuid(),
            NameEn = "Strength Cycle",
            NameFa = "چرخه قدرت",
            DescriptionEn = "Heavy compound focus",
            DescriptionFa = "تمرکز روی حرکات پایه سنگین",
            Goal = FitnessGoal.Strength,
            DurationWeeks = 6,
            Status = ProgramStatus.Active,
            AthleteId = sam.Id,
            CoachId = coachId,
            StartDate = today.AddDays(-40),
            EndDate = today.AddDays(3),
            Days =
            [
                CreateProgramDay("Upper", "بالاتنه", 1, exercises, [0, 4, 3])
            ]
        };

        var completedPlan = new WorkoutPlan
        {
            Id = Guid.NewGuid(),
            NameEn = "Foundation Phase",
            NameFa = "فاز پایه",
            DescriptionEn = "Intro program",
            DescriptionFa = "برنامه مقدماتی",
            Goal = FitnessGoal.Recomp,
            DurationWeeks = 4,
            Status = ProgramStatus.Completed,
            AthleteId = jamie.Id,
            CoachId = coachId,
            StartDate = today.AddDays(-90),
            EndDate = today.AddDays(-62),
            Days =
            [
                CreateProgramDay("Full Body", "تمام بدن", 1, exercises, [0, 1, 7])
            ]
        };

        db.WorkoutPlans.AddRange(activePlan, expiringPlan, completedPlan);
    }

    private static ProgramDay CreateProgramDay(
        string nameEn,
        string nameFa,
        int order,
        List<Exercise> exercises,
        int[] exerciseIndexes)
    {
        var day = new ProgramDay
        {
            Id = Guid.NewGuid(),
            NameEn = nameEn,
            NameFa = nameFa,
            Order = order
        };

        var exerciseOrder = 1;
        foreach (var idx in exerciseIndexes)
        {
            day.Exercises.Add(new ProgramDayExercise
            {
                Id = Guid.NewGuid(),
                ExerciseId = exercises[idx].Id,
                Sets = 4,
                Reps = 8,
                RestSeconds = 90,
                RirRpe = "RIR 2",
                CoachNotes = "Controlled tempo",
                Order = exerciseOrder++
            });
        }

        return day;
    }

    private static void SeedCoachNotes(GymCoachDbContext db, List<Athlete> athletes, Guid coachId)
    {
        foreach (var athlete in athletes.Where(a => a.Status == AthleteStatus.Active).Take(4))
        {
            db.CoachNotes.Add(new CoachNote
            {
                Id = Guid.NewGuid(),
                AthleteId = athlete.Id,
                CoachId = coachId,
                Text = $"Focus on progressive overload for {athlete.FirstName}. Monitor recovery.",
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            });
        }
    }

    private static void SeedWorkoutSessions(GymCoachDbContext db, List<Athlete> athletes)
    {
        var today = DateTime.UtcNow;
        var alex = athletes[0];
        var sam = athletes[1];
        var jamie = athletes[4];

        db.WorkoutSessions.AddRange(
            new WorkoutSession
            {
                Id = Guid.NewGuid(),
                AthleteId = alex.Id,
                WorkoutPlanId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                SessionDate = today.AddDays(-1),
                IsCompleted = true
            },
            new WorkoutSession
            {
                Id = Guid.NewGuid(),
                AthleteId = sam.Id,
                SessionDate = today.AddDays(-2),
                IsCompleted = true
            },
            new WorkoutSession
            {
                Id = Guid.NewGuid(),
                AthleteId = jamie.Id,
                SessionDate = today.AddDays(-10),
                IsCompleted = true
            });
    }

    private static List<Exercise> CreateSampleExercises()
    {
        var exercises = new List<Exercise>();
        var defs = new[]
        {
            ("Barbell Bench Press", "پرس سینه هالتر", MuscleGroup.Chest, DifficultyLevel.Intermediate),
            ("Back Squat", "اسکوات", MuscleGroup.Legs, DifficultyLevel.Intermediate),
            ("Conventional Deadlift", "ددلیفت", MuscleGroup.Back, DifficultyLevel.Advanced),
            ("Overhead Press", "پرس سرشانه", MuscleGroup.Shoulders, DifficultyLevel.Intermediate),
            ("Pull-Up", "بارفیکس", MuscleGroup.Back, DifficultyLevel.Intermediate),
            ("Romanian Deadlift", "ددلیفت رومانیایی", MuscleGroup.Legs, DifficultyLevel.Intermediate),
            ("Dumbbell Row", "زیربغل دمبل", MuscleGroup.Back, DifficultyLevel.Beginner),
            ("Plank", "پلانک", MuscleGroup.Core, DifficultyLevel.Beginner)
        };

        foreach (var (nameEn, nameFa, muscle, diff) in defs)
        {
            var ex = new Exercise
            {
                Id = Guid.NewGuid(),
                NameEn = nameEn,
                NameFa = nameFa,
                DescriptionEn = $"Perform {nameEn} with controlled tempo.",
                DescriptionFa = $"اجرای {nameFa} با تمپوی کنترل‌شده.",
                MuscleGroup = muscle,
                DifficultyLevel = diff,
                Equipment = "Barbell",
                MuscleActivations =
                [
                    new ExerciseMuscleActivation
                    {
                        Id = Guid.NewGuid(), MuscleNameEn = muscle.ToString(),
                        MuscleNameFa = nameFa, ActivationPercent = 85
                    }
                ],
                SupportingMuscles =
                [
                    new ExerciseSupportingMuscle
                    {
                        Id = Guid.NewGuid(),
                        NameEn = "Triceps",
                        NameFa = "سه‌سر بازو"
                    }
                ],
                Media =
                [
                    new ExerciseMedia
                    {
                        Id = Guid.NewGuid(),
                        MediaType = ExerciseMediaType.Image,
                        Url = $"https://picsum.photos/seed/{nameEn.GetHashCode()}/400/300",
                        Order = 1
                    }
                ],
                Steps =
                [
                    new ExerciseStep
                    {
                        Id = Guid.NewGuid(), Order = 1,
                        InstructionEn = "Set up with stable base.",
                        InstructionFa = "ستاپ پایدار بگیرید."
                    },
                    new ExerciseStep
                    {
                        Id = Guid.NewGuid(), Order = 2,
                        InstructionEn = "Execute the rep with full range.",
                        InstructionFa = "حرکت را با دامنه کامل انجام دهید."
                    }
                ]
            };
            exercises.Add(ex);
        }

        exercises[0].AlternativesFrom.Add(new ExerciseAlternative
        {
            ExerciseId = exercises[0].Id,
            AlternativeExerciseId = exercises[6].Id
        });

        return exercises;
    }
}
