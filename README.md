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

4. Now we are ready to create some C# objects. First of all we need views for Zone inherited from `IZoneView`:
    ```c#
    public interface IMainZoneView : IZoneView { }

    public interface IDetailsZoneView : IZoneView { }
    ```
    We should also create a views using this interfaces. For example for `IMainZoneView`:

    ```c#
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

    ```c#    
    public interface IMainZoneViewModel : IZoneViewModel { }

    public interface IDetailsZoneViewModel : IZoneViewModel { }
    ```

    We should also create a viewModels using interfaces. Example:
    ```c#
        public partial class MainZoneViewModel : ZoneViewModelBase, IMainZoneViewModel
        {
                //skipped markup for brevity        
        }
    ```

5. Register your items in Dependency Injection Container:
   ```c#
        services.AddScoped<IMainZoneView, MainZoneView>();
        services.AddScoped<IMainZoneViewModel, MainZoneViewModel>();

        services.AddScoped<IDetailsZoneView, DetailZoneView>();
        services.AddScoped<IDetailsZoneViewModel, DetailsZoneViewModel>();
   ```
6. Inject `ZoneManager` into your main ViewModel:
   ```c#
        
    public MainWindowViewModel(
        IZoneManager zoneManager,
        IVersionService versionService)
    {
        Title = $"WPF with MVVM (v{versionService.Version})";
        _zoneManager = zoneManager;
    }
   ```
7. Switch your components to 
   ```c#
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

You can find in repository a demo project.