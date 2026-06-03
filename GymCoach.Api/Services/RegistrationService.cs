using GymCoach.Database;
using GymCoach.Database.Entities;
using GymCoach.Shared.Common;
using GymCoach.Shared.Dtos;
using GymCoach.Shared.Enums;
using GymCoach.Shared.Utilities;
using Microsoft.EntityFrameworkCore;

namespace GymCoach.Api.Services;

public interface IRegistrationService
{
    Task<Result<RegisterUserResultDto>> RegisterAndLinkAsync(RegisterUserRequest request, CancellationToken cancellationToken = default);
}

public sealed class RegistrationService(GymCoachDbContext db) : IRegistrationService
{
    public async Task<Result<RegisterUserResultDto>> RegisterAndLinkAsync(
        RegisterUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var phone = PhoneNormalizer.Normalize(request.PhoneNumber);
        if (!PhoneNormalizer.IsValid(phone))
        {
            return Result<RegisterUserResultDto>.Failure("Invalid phone number.");
        }

        if (string.IsNullOrWhiteSpace(request.UserId))
        {
            return Result<RegisterUserResultDto>.Failure("UserId is required.");
        }

        var placeholder = await db.Athletes
            .FirstOrDefaultAsync(a => a.PhoneNumber == phone && a.Status == AthleteStatus.Placeholder, cancellationToken);

        if (placeholder is null)
        {
            return Result<RegisterUserResultDto>.Success(new RegisterUserResultDto
            {
                LinkedToPlaceholder = false
            });
        }

        placeholder.UserId = request.UserId;
        placeholder.Status = AthleteStatus.Active;

        if (!string.IsNullOrWhiteSpace(request.FirstName))
        {
            placeholder.FirstName = request.FirstName.Trim();
        }

        if (!string.IsNullOrWhiteSpace(request.LastName))
        {
            placeholder.LastName = request.LastName.Trim();
        }

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            placeholder.Email = request.Email.Trim();
        }

        await db.SaveChangesAsync(cancellationToken);

        return Result<RegisterUserResultDto>.Success(new RegisterUserResultDto
        {
            LinkedToPlaceholder = true,
            AthleteId = placeholder.Id
        });
    }
}
