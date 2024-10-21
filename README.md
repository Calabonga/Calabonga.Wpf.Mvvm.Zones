# Calabonga.Wpf.Mvvm.Zones
Zones is for UserControl binding for WPF application with MVVM. It's like Regions in the Prism framework, but more simple.

# How to use

There are a few simple steps:

1. Install nuget-package [`Calabonga.Wpf.Mvvm.Zones`](https://www.nuget.org/packages/Calabonga.Wpf.Mvvm.Zones/)
2. On the XAML where your zone will be use add namespace:
    ```diff
    <Window x:Class="Calabonga.Wpf.MvvmMdi.Views.MainWindow"
            xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
            xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
            xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
            xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
            xmlns:local="clr-namespace:Calabonga.Wpf.MvvmMdi"
    +       xmlns:zones="clr-namespace:Calabonga.Wpf.Mvvm.Zones;assembly=Calabonga.Wpf.Mvvm.Zones"
            xmlns:core="clr-namespace:Calabonga.Wpf.MvvmMdi.Core"
            mc:Ignorable="d"
            WindowStartupLocation="CenterScreen"
            Title="{Binding Title}" Height="600" Width="800" Icon="/logo.png">
    ```
3. Create a markup in your page. For example, I've created two controls with zone names `MainZone` and `DetailsZone`.
   
   ```diff
   <DockPanel VerticalAlignment="Stretch" HorizontalAlignment="Stretch">
        <StackPanel DockPanel.Dock="Top"  Orientation="Horizontal" HorizontalAlignment="Center">
            <!-- skipped markup for brevity -->
        </StackPanel>

   +    <ContentControl DockPanel.Dock="Left" Width="400" zones:Zones.ZoneName="MainZone"   />
   +    <ContentControl Width="400" zones:Zones.ZoneName="DetailsZone" />

    </DockPanel>
   ```

4. Now we are ready to create some csharp objects. First of all we need views for Zone inherited from `IZoneView`:
    ```csharp
    public interface IMainZoneView : IZoneView { }

    public interface IDetailsZoneView : IZoneView { }
    ```
    We should also create a views using this interfaces. For example for `IMainZoneView`:

    ```csharp
        /// <summary>
        /// Interaction logic for MainZoneView.xaml
        /// </summary>
        public partial class MainZoneView : UserControl, IMainZoneView
        {
                public MainZoneView()
                {
                        InitializeComponent();
                }
        }
    ```

    Then we need to viewModels for Zone inherited from `IZoneViewModel`:

    ```csharp    
    public interface IMainZoneViewModel : IZoneViewModel { }

    public interface IDetailsZoneViewModel : IZoneViewModel { }
    ```

    We should also create a viewModels using interfaces. Example:
    ```csharp
        public partial class MainZoneViewModel : ZoneViewModelBase, IMainZoneViewModel
        {
                //skipped markup for brevity        
        }
    ```
5. Register ZoneManager in your Dependency Injection Container:
    ``` diff
    + services.AddZones();
    ```
6. Register your modules Views ans ViewModels interfaces in Dependency Injection Container:
   
   ```diff
   + services.AddScoped<IMainZoneView, MainZoneView>();
   + services.AddScoped<IMainZoneViewModel, MainZoneViewModel>();
   + services.AddScoped<IDetailsZoneView, DetailZoneView>();
   + services.AddScoped<IDetailsZoneViewModel, DetailsZoneViewModel>();
   ```
7. Inject `ZoneManager` into your main ViewModel:
   ```csharp
    public MainWindowViewModel(IZoneManager zoneManager, IVersionService versionService)
    {
        Title = $"WPF with MVVM (v{versionService.Version})";
        _zoneManager = zoneManager;
    }
   ```
8. Now you can open your components in the zones you want. For example, in the registered zones `MainZone` and `DetailsZone`:
   ```csharp
    [RelayCommand]
    private void OpenMain()
    {
        _zoneManager.ActivateZone<IMainZoneView, IMainZoneViewModel>("MainZone");
    }

    [RelayCommand]
    private void OpenDetails()
    {
        _zoneManager.ActivateZone<IDetailsZoneView, IDetailsZoneViewModel>("DetailsName");
    }
   ```
## Demo

You can find in this repository a `demo` project from where this snippets used.

### CommunityToolkit Messaging

DoubleClick on text in the Detail zone will send a message to the module in Main zone and to the Shell (see in window title).

<img width="640" alt="double-click" src="https://github.com/user-attachments/assets/0d4e68d4-3b0b-42f9-91ea-76406eface6c">

### Remove module

DoubleClick on tne text in the Main zone will remove it self from zone where it opened.
<img width="640" alt="double-click-close" src="https://github.com/user-attachments/assets/d388d98b-1282-406b-8f81-7aeada36f7c8">

## History 

### 1.0.1 2024-10-21

IMvvmObjectFactory added to dependency injection registration fixed.

### 1.0.0 2024-09-26

First release