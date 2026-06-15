using GymCoach.Client.Client.Models.Athlete;
using Microsoft.AspNetCore.Components;

namespace GymCoach.Client.Client.Components.Athlete.PendingRequestsCard;

public partial class PendingRequestsCard
{
    [Parameter, EditorRequired] public PendingRequestsModel Requests { get; set; } = new();
    [Parameter] public string Title { get; set; } = "Pending Requests";
    [Parameter] public string PhotoRequestsLabel { get; set; } = "Photo Requests";
    [Parameter] public string MeasurementRequestsLabel { get; set; } = "Measurement Requests";
    [Parameter] public string PendingLabel { get; set; } = "pending";
    [Parameter] public EventCallback OnPhotoRequestsClick { get; set; }
}
