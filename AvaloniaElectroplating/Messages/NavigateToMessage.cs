using AvaloniaElectroplating.Enums;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AvaloniaElectroplating.Messages;

public class NavigateToMessage(ApplicationPageNames targetPage) : ValueChangedMessage<ApplicationPageNames>(targetPage)
{
}