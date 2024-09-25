﻿using System.Collections.ObjectModel;
using Calabonga.Wpf.Mvvm.Zones;
using Calabonga.Wpf.MvvmMdi.Core;
using Calabonga.Wpf.MvvmMdi.Messaging;
using Calabonga.Wpf.MvvmMdi.Screens;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Calabonga.Wpf.MvvmMdi.ViewModels;

/// <summary>
/// ViewModel for MainWindow
/// </summary>
public partial class MainWindowViewModel : ViewModelBase, IRecipient<MessageFromDetails>
{
    private readonly IMvvmObjectFactory _mvvmFactory;
    private readonly IZoneManager _zoneManager;

    public MainWindowViewModel(
        IMvvmObjectFactory mvvmFactory,
        IZoneManager zoneManager,
        IVersionService versionService)
    {
        Title = $"WPF with MVVM (v{versionService.Version})";
        _mvvmFactory = mvvmFactory;
        _zoneManager = zoneManager;

        _zoneManager.Activated += (_, view) =>
        {
            if (view.Type == typeof(IMainZoneView))
            {
                WeakReferenceMessenger.Default.RegisterAll(this);
            }
        };
        _zoneManager.Deactivated += (_, view) =>
        {
            if (view.Type == typeof(IMainZoneView))
            {
                WeakReferenceMessenger.Default.UnregisterAll(this);
            }
        };
    }

    [ObservableProperty]
    private ObservableCollection<IZoneView> _tabs = new();

    [ObservableProperty]
    private string _zoneName = "Main";

    #region Commands

    [RelayCommand]
    private void Add()
    {
        var view = _mvvmFactory.Create(typeof(IMainZoneView), typeof(IMainZoneViewModel));

        Tabs.Add(view);
    }

    [RelayCommand]
    private void OpenMain()
    {
        _zoneManager.ActivateZone<IMainZoneView, IMainZoneViewModel>(ZoneName);
    }

    [RelayCommand]
    private void OpenDetails()
    {
        _zoneManager.ActivateZone<IDetailsZoneView, IDetailsZoneViewModel>(ZoneName);
    }

    #endregion

    public void Receive(MessageFromDetails message)
    {
        Title = $"WPF with MVVM -> {message.Text}";
    }
}