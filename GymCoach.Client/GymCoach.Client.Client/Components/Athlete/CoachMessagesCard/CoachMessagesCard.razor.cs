using GymCoach.Client.Client.Models.Athlete;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GymCoach.Client.Client.Components.Athlete.CoachMessagesCard;

public partial class CoachMessagesCard
{
    [Parameter, EditorRequired] public CoachMessagePreviewModel Message { get; set; } = new();
    [Parameter] public string Title { get; set; } = "Coach Messages";
    [Parameter] public string UnreadLabel { get; set; } = "unread";
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

    private Task HandleClick(MouseEventArgs args) => OnClick.InvokeAsync(args);
}
