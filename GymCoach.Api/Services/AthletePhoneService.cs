using GymCoach.Database;
using GymCoach.Database.Entities;
using GymCoach.Shared.Common;
using GymCoach.Shared.Dtos;
using GymCoach.Shared.Enums;
using GymCoach.Shared.Utilities;
using Microsoft.EntityFrameworkCore;

namespace GymCoach.Api.Services;

public interface IAthletePhoneService
{
    Task<Result<CheckPhoneResultDto>> CheckPhoneAsync(Guid coachId, CheckPhoneRequest request, CancellationToken cancellationToken = default);
    Task<Result<AddAthleteByPhoneResultDto>> AddByPhoneAsync(Guid coachId, AddAthleteByPhoneRequest request, CancellationToken cancellationToken = default);
}

public sealed class AthletePhoneService(GymCoachDbContext db) : IAthletePhoneService
{
    public async Task<Result<CheckPhoneResultDto>> CheckPhoneAsync(
        Guid coachId,
        CheckPhoneRequest request,
        CancellationToken cancellationToken = default)
    {
        var phone = PhoneNormalizer.Normalize(request.PhoneNumber);
        if (!PhoneNormalizer.IsValid(phone))
        {
            return Result<CheckPhoneResultDto>.Failure("Invalid phone number.");
        }

        var athlete = await db.Athletes
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.PhoneNumber == phone, cancellationToken);

        if (athlete is null)
        {
            return Result<CheckPhoneResultDto>.Success(new CheckPhoneResultDto
            {
                AccountExists = false
            });
        }

        var accountExists = athlete.UserId != null && athlete.Status != AthleteStatus.Placeholder;
        var alreadyLinked = await db.CoachAthletes
            .AnyAsync(ca => ca.CoachId == coachId && ca.AthleteId == athlete.Id && ca.IsActive, cancellationToken);

        return Result<CheckPhoneResultDto>.Success(new CheckPhoneResultDto
        {
            AccountExists = accountExists,
            ExistingAthleteId = athlete.Id,
            ExistingAthleteNameEn = $"{athlete.FirstName} {athlete.LastName}".Trim(),
            ExistingAthleteNameFa = $"{athlete.FirstName} {athlete.LastName}".Trim(),
            AlreadyLinkedToCoach = alreadyLinked
        });
    }

    public async Task<Result<AddAthleteByPhoneResultDto>> AddByPhoneAsync(
        Guid coachId,
        AddAthleteByPhoneRequest request,
        CancellationToken cancellationToken = default)
    {
        var phone = PhoneNormalizer.Normalize(request.PhoneNumber);
        if (!PhoneNormalizer.IsValid(phone))
        {
            return Result<AddAthleteByPhoneResultDto>.Failure("Invalid phone number.");
        }

        var coachExists = await db.Coaches.AnyAsync(c => c.Id == coachId, cancellationToken);
        if (!coachExists)
        {
            return Result<AddAthleteByPhoneResultDto>.Failure("Coach not found.");
        }

        var existing = await db.Athletes
            .Include(a => a.CoachLinks)
            .FirstOrDefaultAsync(a => a.PhoneNumber == phone, cancellationToken);

        if (existing is not null)
        {
            var linkResult = await EnsureCoachLinkAsync(coachId, existing.Id, cancellationToken);
            if (!linkResult.IsSuccess)
            {
                return Result<AddAthleteByPhoneResultDto>.Failure(linkResult.Error!);
            }

            return Result<AddAthleteByPhoneResultDto>.Success(new AddAthleteByPhoneResultDto
            {
                AthleteId = existing.Id,
                WasAutoLinked = true,
                WasPlaceholderCreated = false,
                Status = existing.Status,
                FullNameEn = $"{existing.FirstName} {existing.LastName}".Trim(),
                FullNameFa = $"{existing.FirstName} {existing.LastName}".Trim()
            });
        }

        var firstName = string.IsNullOrWhiteSpace(request.FirstName) ? "Athlete" : request.FirstName.Trim();
        var lastName = string.IsNullOrWhiteSpace(request.LastName) ? phone[^4..] : request.LastName.Trim();

        var placeholder = new Athlete
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phone,
            Email = string.Empty,
            Status = AthleteStatus.Placeholder,
            Gender = Gender.Other,
            Goal = FitnessGoal.Recomp
        };

        db.Athletes.Add(placeholder);
        db.CoachAthletes.Add(new CoachAthlete
        {
            Id = Guid.NewGuid(),
            CoachId = coachId,
            AthleteId = placeholder.Id,
            Role = CoachAthleteRole.Fitness,
            IsActive = true,
            AssignedAt = DateTime.UtcNow
        });

        await db.SaveChangesAsync(cancellationToken);

        return Result<AddAthleteByPhoneResultDto>.Success(new AddAthleteByPhoneResultDto
        {
            AthleteId = placeholder.Id,
            WasAutoLinked = false,
            WasPlaceholderCreated = true,
            Status = AthleteStatus.Placeholder,
            FullNameEn = $"{firstName} {lastName}".Trim(),
            FullNameFa = $"{firstName} {lastName}".Trim()
        });
    }

    private async Task<Result> EnsureCoachLinkAsync(Guid coachId, Guid athleteId, CancellationToken cancellationToken)
    {
        var hasActiveFitnessCoach = await db.CoachAthletes
            .AnyAsync(ca => ca.AthleteId == athleteId
                && ca.Role == CoachAthleteRole.Fitness
                && ca.IsActive
                && ca.CoachId != coachId, cancellationToken);

        if (hasActiveFitnessCoach)
        {
            return Result.Failure("Athlete already has an active fitness coach.");
        }

        var existingLink = await db.CoachAthletes
            .FirstOrDefaultAsync(ca => ca.CoachId == coachId && ca.AthleteId == athleteId && ca.Role == CoachAthleteRole.Fitness, cancellationToken);

        if (existingLink is null)
        {
            db.CoachAthletes.Add(new CoachAthlete
            {
                Id = Guid.NewGuid(),
                CoachId = coachId,
                AthleteId = athleteId,
                Role = CoachAthleteRole.Fitness,
                IsActive = true,
                AssignedAt = DateTime.UtcNow
            });
        }
        else if (!existingLink.IsActive)
        {
            existingLink.IsActive = true;
            existingLink.AssignedAt = DateTime.UtcNow;
        }

        await db.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
