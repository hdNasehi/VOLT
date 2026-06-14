namespace GymCoach.Shared.Constants;

public static class Permissions
{
    public const string SuperAdminOnly = "SuperAdminOnly";
    public const string SystemAdminOrAbove = "SystemAdminOrAbove";
    public const string GymManagerOrAbove = "GymManagerOrAbove";
    public const string CoachOnly = "CoachOnly";
    public const string AthleteOnly = "AthleteOnly";
    public const string ManageExercises = "ManageExercises";
    public const string ApproveCoaches = "ApproveCoaches";
    public const string ManageGym = "ManageGym";
    public const string ManageOwnAthletes = "ManageOwnAthletes";
    public const string ManageOwnPlans = "ManageOwnPlans";
    public const string PurchasePlans = "PurchasePlans";
    public const string ViewOwnProgress = "ViewOwnProgress";
    public const string SendMessages = "SendMessages";
    public const string ManageSettlements = "ManageSettlements";
    public const string ManagePlatformSettings = "ManagePlatformSettings";

    public static readonly string[] AllRoles =
    [
        Roles.SuperAdmin,
        Roles.SystemAdmin,
        Roles.GymManager,
        Roles.Coach,
        Roles.Athlete
    ];
}

public static class Roles
{
    public const string SuperAdmin = "SuperAdmin";
    public const string SystemAdmin = "SystemAdmin";
    public const string GymManager = "GymManager";
    public const string Coach = "Coach";
    public const string Athlete = "Athlete";
}
