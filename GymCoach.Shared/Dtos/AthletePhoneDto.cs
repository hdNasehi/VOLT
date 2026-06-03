using GymCoach.Shared.Enums;

namespace GymCoach.Shared.Dtos;

public class CheckPhoneRequest
{
    public string PhoneNumber { get; set; } = string.Empty;
}

public class CheckPhoneResultDto
{
    public bool AccountExists { get; set; }
    public Guid? ExistingAthleteId { get; set; }
    public string? ExistingAthleteNameEn { get; set; }
    public string? ExistingAthleteNameFa { get; set; }
    public bool AlreadyLinkedToCoach { get; set; }
}

public class AddAthleteByPhoneRequest
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

public class AddAthleteByPhoneResultDto
{
    public Guid AthleteId { get; set; }
    public bool WasPlaceholderCreated { get; set; }
    public bool WasAutoLinked { get; set; }
    public AthleteStatus Status { get; set; }
    public string FullNameEn { get; set; } = string.Empty;
    public string FullNameFa { get; set; } = string.Empty;
}

public class RegisterUserRequest
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
}

public class RegisterUserResultDto
{
    public bool LinkedToPlaceholder { get; set; }
    public Guid? AthleteId { get; set; }
}
