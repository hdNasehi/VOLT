using FluentAssertions;
using GymCoach.Client.Tests.Support;
using MudBlazor;
using Moq;
using Xunit;
using VoltConfirmDialogComponent = GymCoach.Client.Client.Components.Common.VoltConfirmDialog.VoltConfirmDialog;

namespace GymCoach.Client.Tests.Components.Common.VoltConfirmDialogTests;

public class VoltConfirmDialogTests : VoltComponentTestBase
{
    [Fact]
    public void Sets_Title_And_Message_Parameters()
    {
        var dialog = new Mock<IMudDialogInstance>();
        var cut = RenderComponent<VoltConfirmDialogComponent>(p => p
            .AddCascadingValue(dialog.Object)
            .Add(x => x.Title, "Delete?")
            .Add(x => x.Message, "This cannot be undone."));

        cut.Instance.Title.Should().Be("Delete?");
        cut.Instance.Message.Should().Be("This cannot be undone.");
    }
}
