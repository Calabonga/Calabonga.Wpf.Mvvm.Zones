using Calabonga.Wpf.Mvvm.Zones;
using Calabonga.Wpf.MvvmMdi.Messaging;
using Calabonga.Wpf.MvvmMdi.Screens;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Calabonga.Wpf.MvvmMdi.ViewModels;

public partial class MainZoneViewModel : ZoneViewModelBase, IMainZoneViewModel, IRecipient<MessageFromDetails>
{

    [ObservableProperty] private string? _message;

    [RelayCommand]
    private void Close()
    {
        Message = "Closing";
        ZoneManager.Remove(this);
    }

    public override void OnDeactivate()
    {
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }

    public override void OnActivate()
    {
        WeakReferenceMessenger.Default.RegisterAll(this);
    }

    public void Receive(MessageFromDetails message)
    {
        Message = message.Text;
    }
}