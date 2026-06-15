using GymCoach.Client.Client.Models.Athlete;
using GymCoach.Shared.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace GymCoach.Client.Client.Components.Athlete.PhotoRequestCard;

public partial class PhotoRequestCard
{
    private IBrowserFile? _selectedFile;
    private bool _uploading;

    [Parameter, EditorRequired] public PhotoRequestModel Request { get; set; } = new();
    [Parameter] public string UploadLabel { get; set; } = "Upload photo";
    [Parameter] public string PendingLabel { get; set; } = "Pending review";
    [Parameter] public string ApprovedLabel { get; set; } = "Approved";
    [Parameter] public string RejectedLabel { get; set; } = "Rejected";
    [Parameter] public EventCallback<(Guid RequestId, string ImageUrl)> OnUploaded { get; set; }

    private Color StatusColor => Request.Status switch
    {
        AssessmentReviewStatus.Approved => Color.Success,
        AssessmentReviewStatus.Rejected => Color.Error,
        _ => Color.Warning
    };

    private string StatusText => Request.Status switch
    {
        AssessmentReviewStatus.Approved => ApprovedLabel,
        AssessmentReviewStatus.Rejected => RejectedLabel,
        _ => PendingLabel
    };

    private void HandleFileSelected(InputFileChangeEventArgs e) => _selectedFile = e.File;

    private async Task HandleUpload()
    {
        if (_selectedFile is null)
        {
            return;
        }

        _uploading = true;
        await OnUploaded.InvokeAsync((Request.Id, $"data:image/jpeg;base64,placeholder"));
        _uploading = false;
    }
}
