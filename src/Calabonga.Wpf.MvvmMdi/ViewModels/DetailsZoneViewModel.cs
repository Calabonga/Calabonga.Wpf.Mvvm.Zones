using Calabonga.Wpf.Mvvm.Zones;
using Calabonga.Wpf.MvvmMdi.Messaging;
using Calabonga.Wpf.MvvmMdi.Screens;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Calabonga.Wpf.MvvmMdi.ViewModels;

public partial class DetailsZoneViewModel : ZoneViewModelBase, IDetailsZoneViewModel
{
    [RelayCommand]
    private void SendMessage()
    {
        WeakReferenceMessenger.Default.Send(new MessageFromDetails(DateTime.Now.ToString("G")));
    }
}