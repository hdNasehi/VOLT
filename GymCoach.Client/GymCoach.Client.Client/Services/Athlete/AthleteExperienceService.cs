using GymCoach.Client.Client.Models.Athlete;
using GymCoach.Shared.Enums;

namespace GymCoach.Client.Client.Services.Athlete;

public interface IAthleteExperienceService
{
    Task<AthleteDashboardModel> GetDashboardAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProgramSummaryModel>> GetProgramsAsync(CancellationToken cancellationToken = default);
    Task<ProgramDetailModel?> GetProgramDetailAsync(Guid programId, CancellationToken cancellationToken = default);
    Task<WorkoutDayDetailModel?> GetWorkoutDayAsync(Guid programId, Guid dayId, CancellationToken cancellationToken = default);
    Task<WorkoutSessionModel> StartWorkoutAsync(Guid programId, Guid dayId, CancellationToken cancellationToken = default);
    Task<WorkoutSessionModel?> GetActiveSessionAsync(CancellationToken cancellationToken = default);
    Task CompleteExerciseAsync(Guid sessionId, Guid exerciseId, CancellationToken cancellationToken = default);
    Task StartRestAsync(Guid sessionId, int seconds, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PhotoRequestModel>> GetPhotoRequestsAsync(CancellationToken cancellationToken = default);
    Task UploadPhotoAsync(Guid requestId, string imageUrl, CancellationToken cancellationToken = default);
}

public sealed class AthleteExperienceService : IAthleteExperienceService
{
    private static readonly Guid ActiveProgramId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
    private static readonly Guid Day1Id = Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc1");
    private static readonly Guid Day2Id = Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc2");
    private static readonly Guid Day3Id = Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc3");

    private WorkoutSessionModel? _activeSession;

    public Task<AthleteDashboardModel> GetDashboardAsync(CancellationToken cancellationToken = default)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var model = new AthleteDashboardModel
        {
            AthleteName = "علی محمودی",
            CoachName = "امیر رضایی",
            GymName = "باشگاه ولت",
            ActiveProgram = new ActiveProgramModel
            {
                ProgramId = ActiveProgramId,
                ProgramName = "بلوک حجم‌گیری A",
                CoachName = "امیر رضایی",
                StartDate = today.AddDays(-21),
                EndDate = today.AddDays(35),
                CurrentDay = 8,
                TotalDays = 56,
                ProgressPercent = 38,
                DaysRemaining = 35,
                CurrentDayId = Day2Id
            },
            BodyProgress = new BodyProgressModel
            {
                WeightKg = 82.4m,
                BodyFatPercent = 14.2m,
                LastMeasured = today
            },
            CoachMessage = new CoachMessagePreviewModel
            {
                LastMessage = "امروز روی فرم حرکت تمرکز کن. استراحت بین ست‌ها را رعایت کن.",
                UnreadCount = 2,
                SentAt = DateTime.UtcNow.AddHours(-3)
            },
            PendingRequests = new PendingRequestsModel
            {
                PhotoRequests = 2,
                MeasurementRequests = 1
            },
            WeightTrend =
            [
                new ChartPointModel { Label = "هفته ۱", Value = 83.2m },
                new ChartPointModel { Label = "هفته ۲", Value = 82.9m },
                new ChartPointModel { Label = "هفته ۳", Value = 82.6m },
                new ChartPointModel { Label = "هفته ۴", Value = 82.4m }
            ],
            BodyFatTrend =
            [
                new ChartPointModel { Label = "هفته ۱", Value = 15.1m },
                new ChartPointModel { Label = "هفته ۲", Value = 14.8m },
                new ChartPointModel { Label = "هفته ۳", Value = 14.5m },
                new ChartPointModel { Label = "هفته ۴", Value = 14.2m }
            ],
            PreviousPrograms =
            [
                new ProgramSummaryModel
                {
                    Id = Guid.NewGuid(),
                    Name = "فاز پایه",
                    CoachName = "امیر رضایی",
                    StartDate = today.AddDays(-120),
                    EndDate = today.AddDays(-92),
                    ProgressPercent = 100,
                    Status = ProgramStatus.Completed
                }
            ]
        };

        return Task.FromResult(model);
    }

    public Task<IReadOnlyList<ProgramSummaryModel>> GetProgramsAsync(CancellationToken cancellationToken = default)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        IReadOnlyList<ProgramSummaryModel> programs =
        [
            new()
            {
                Id = ActiveProgramId,
                Name = "بلوک حجم‌گیری A",
                CoachName = "امیر رضایی",
                StartDate = today.AddDays(-21),
                EndDate = today.AddDays(35),
                ProgressPercent = 38,
                Status = ProgramStatus.Active
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "فاز پایه",
                CoachName = "امیر رضایی",
                StartDate = today.AddDays(-120),
                EndDate = today.AddDays(-92),
                ProgressPercent = 100,
                Status = ProgramStatus.Completed
            }
        ];
        return Task.FromResult(programs);
    }

    public Task<ProgramDetailModel?> GetProgramDetailAsync(Guid programId, CancellationToken cancellationToken = default)
    {
        if (programId != ActiveProgramId)
        {
            return Task.FromResult<ProgramDetailModel?>(null);
        }

        var detail = new ProgramDetailModel
        {
            Id = programId,
            Name = "بلوک حجم‌گیری A",
            CoachName = "امیر رضایی",
            Goal = FitnessGoal.Hypertrophy,
            DurationWeeks = 8,
            Days =
            [
                new WorkoutDaySummaryModel { Id = Day1Id, Name = "روز ۱ — سینه + پشت بازو", Order = 1, State = WorkoutDayState.Completed },
                new WorkoutDaySummaryModel { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc0"), Name = "روز ۰ — استراحت (از دست رفته)", Order = 0, State = WorkoutDayState.Missed },
                new WorkoutDaySummaryModel { Id = Day2Id, Name = "روز ۲ — پشت + جلو بازو", Order = 2, State = WorkoutDayState.Current },
                new WorkoutDaySummaryModel { Id = Day3Id, Name = "روز ۳ — پا", Order = 3, State = WorkoutDayState.Future }
            ]
        };
        return Task.FromResult<ProgramDetailModel?>(detail);
    }

    public Task<WorkoutDayDetailModel?> GetWorkoutDayAsync(Guid programId, Guid dayId, CancellationToken cancellationToken = default)
    {
        if (programId != ActiveProgramId)
        {
            return Task.FromResult<WorkoutDayDetailModel?>(null);
        }

        var dayName = dayId == Day1Id ? "روز ۱ — سینه + پشت بازو"
            : dayId == Day2Id ? "روز ۲ — پشت + جلو بازو"
            : "روز ۳ — پا";

        var exercises = CreateExercisesForDay(dayId);
        return Task.FromResult<WorkoutDayDetailModel?>(new WorkoutDayDetailModel
        {
            Id = dayId,
            ProgramId = programId,
            ProgramName = "بلوک حجم‌گیری A",
            DayName = dayName,
            Exercises = exercises
        });
    }

    public Task<WorkoutSessionModel> StartWorkoutAsync(Guid programId, Guid dayId, CancellationToken cancellationToken = default)
    {
        var exercises = CreateExercisesForDay(dayId);
        for (var i = 0; i < exercises.Count; i++)
        {
            exercises[i].IsLocked = i > 0;
        }

        _activeSession = new WorkoutSessionModel
        {
            SessionId = Guid.NewGuid(),
            DayId = dayId,
            DayName = dayId == Day2Id ? "روز ۲ — پشت + جلو بازو" : "روز تمرین",
            StartedAt = DateTime.UtcNow,
            Exercises = exercises,
            CurrentExerciseIndex = 0
        };
        return Task.FromResult(_activeSession);
    }

    public Task<WorkoutSessionModel?> GetActiveSessionAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult(_activeSession);

    public Task CompleteExerciseAsync(Guid sessionId, Guid exerciseId, CancellationToken cancellationToken = default)
    {
        if (_activeSession is null || _activeSession.SessionId != sessionId)
        {
            return Task.CompletedTask;
        }

        var list = _activeSession.Exercises.ToList();
        var index = list.FindIndex(e => e.Id == exerciseId);
        if (index < 0)
        {
            return Task.CompletedTask;
        }

        list[index].IsCompleted = true;
        list[index].IsLocked = false;
        if (index + 1 < list.Count)
        {
            list[index + 1].IsLocked = false;
        }

        _activeSession.Exercises = list;
        _activeSession.CurrentExerciseIndex = Math.Min(index + 1, list.Count - 1);
        _activeSession.IsResting = true;
        _activeSession.RestSecondsRemaining = list[index].RestSeconds;
        return Task.CompletedTask;
    }

    public Task StartRestAsync(Guid sessionId, int seconds, CancellationToken cancellationToken = default)
    {
        if (_activeSession is not null && _activeSession.SessionId == sessionId)
        {
            _activeSession.IsResting = true;
            _activeSession.RestSecondsRemaining = seconds;
        }
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<PhotoRequestModel>> GetPhotoRequestsAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<PhotoRequestModel> requests =
        [
            new()
            {
                Id = Guid.NewGuid(),
                BodyPart = "نمای جلو",
                Instructions = "در نور طبیعی، دست‌ها کنار بدن.",
                ExampleImageUrl = "https://picsum.photos/seed/front/300/400",
                Status = AssessmentReviewStatus.Pending
            },
            new()
            {
                Id = Guid.NewGuid(),
                BodyPart = "بازو",
                Instructions = "از کنار بدن، عضله را منقبض کن.",
                ExampleImageUrl = "https://picsum.photos/seed/arm/300/400",
                Status = AssessmentReviewStatus.Rejected,
                RejectionReason = "نور کافی نیست. لطفاً دوباره آپلود کنید."
            }
        ];
        return Task.FromResult(requests);
    }

    public Task UploadPhotoAsync(Guid requestId, string imageUrl, CancellationToken cancellationToken = default) =>
        Task.CompletedTask;

    private static List<ExerciseDetailModel> CreateExercisesForDay(Guid dayId)
    {
        if (dayId == Day3Id)
        {
            return
            [
                CreateExercise("اسکوات", "پا", 4, 8, 120, null, "عمق کامل، زانو هم‌راستا با پا"),
                CreateExercise("ددلیفت رومانیایی", "پا", 3, 10, 90, null, "کمر صاف")
            ];
        }

        return
        [
            CreateExercise("پرس سینه هالتر", "سینه", 4, 8, 90, 1, "کتف جمع شده"),
            CreateExercise("زیربغل دمبل", "پشت", 4, 8, 90, 1, "سوپرست با پرس سینه"),
            CreateExercise("جلو بازو دمبل", "بازو", 3, 12, 60, null, "بدون تاب دادن")
        ];
    }

    private static ExerciseDetailModel CreateExercise(
        string name, string muscle, int sets, int reps, int rest, int? superset, string notes) => new()
    {
        Id = Guid.NewGuid(),
        Name = name,
        MuscleGroup = muscle,
        Description = $"تمرکز روی {muscle} با فرم کنترل‌شده.",
        VideoUrl = "https://www.youtube.com/embed/aclHkVaku9U",
        GifUrl = "https://picsum.photos/seed/exercise/400/300",
        Tips = ["گرم‌کردن قبل از ست اول", "دمای بدن را حفظ کن"],
        CommonMistakes = ["استفاده از وزنه سنگین با فرم ضعیف", "نفس حبس کردن"],
        Sets = sets,
        Reps = reps,
        RestSeconds = rest,
        SupersetGroupId = superset,
        SetScheme = superset.HasValue ? "سوپرست" : null,
        CoachNotes = notes
    };
}
