# GRC.Shared

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![Avalonia](https://img.shields.io/badge/Avalonia-11.3.11-8B44AC)
![License](https://img.shields.io/badge/license-MIT-green)

A comprehensive .NET 8 library providing shared components, UI controls, and domain models for GRC (Governance, Risk, and Compliance) applications. Built with [Avalonia UI](https://avaloniaui.net/) for cross-platform desktop applications following MVVM architecture patterns.

## ?? Features

### ?? Rich UI Components
- **Custom Controls**: TrafficLight indicators, SearchBox, RadialGauge, LoadingIndicator, StatusBar, and more
- **Modal Dialog System**: Acrylic-themed dialogs with flexible layouts and animations
- **Toast Notifications**: Non-intrusive success/warning/error notifications with auto-dismiss
- **Data Converters**: 11+ value converters for currency, dates, booleans, and custom transformations

### ??? MVVM Infrastructure
- **Base ViewModels**: Ready-to-use base classes with messenger support and validation
- **Messaging System**: Cross-component communication using CommunityToolkit.Mvvm
- **Command Helpers**: Thread-safe command execution and state management
- **Validation Support**: Built-in validation with data annotations

### ?? Domain Models
- **Engagement Management**: Complete engagement lifecycle tracking with financial data
- **Resource Allocation**: Hour allocation tracking and budgeting
- **Financial Models**: Revenue tracking, fiscal years, closing periods, and actuals
- **Assignment Models**: Manager assignments, rank budgets, and PAPD hierarchies

### ?? Localization
- Multi-language support (English, Portuguese-Brazil)
- Extensible resource-based localization system

## ?? Projects

### GRC.Shared.Core
Core business logic layer containing:
- Domain models and entities
- Business enumerations
- Service interfaces
- Data transfer objects

### GRC.Shared.UI
Presentation layer providing:
- Reusable Avalonia UI controls
- ViewModel base classes
- Value converters and behaviors
- Dialog and toast services
- File picker utilities

### GRC.Shared.Resources
Resource layer including:
- Localization resources (en-US, pt-BR)
- Theme definitions
- Application assets

## ?? Getting Started

### Prerequisites
- .NET 8 SDK or later
- Visual Studio 2022 or JetBrains Rider (recommended for Avalonia development)

### Installation

Add the project references to your Avalonia application:

```xml
<ItemGroup>
  <ProjectReference Include="..\GRC.Shared.Core\GRC.Shared.Core.csproj" />
  <ProjectReference Include="..\GRC.Shared.UI\GRC.Shared.UI.csproj" />
  <ProjectReference Include="..\GRC.Shared.Resources\GRC.Shared.Resources.csproj" />
</ItemGroup>
```

### NuGet Dependencies
The solution uses the following key packages:
- **Avalonia** 11.3.11
- **CommunityToolkit.Mvvm** 8.4.0
- **Avalonia.Themes.Fluent** 11.3.11

## ?? Usage Examples

### Using the TrafficLight Control

```xaml
<Window xmlns:controls="using:GRC.Shared.UI.Controls">
    <!-- Numeric mode: calculates color from value -->
    <controls:TrafficLight 
        Mode="Numeric"
        Value="{Binding MarginPercentage}"
        GreenUpperBound="50"
        YellowUpperBound="85"
        Size="24" />
    
    <!-- String mode: direct status -->
    <controls:TrafficLight 
        Mode="String"
        Status="{Binding StatusColor}"
        Size="32" />
</Window>
```

### Creating a ViewModel

```csharp
using GRC.Shared.UI.ViewModels;
using CommunityToolkit.Mvvm.Input;

public class MyViewModel : ValidatableViewModelBase
{
    private readonly IDataService _dataService;

    public MyViewModel(IMessenger messenger, IDataService dataService) 
        : base(messenger, new[] { "Engagements" })
    {
        _dataService = dataService;
        SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
    }

    public override async Task LoadDataAsync()
    {
        Items = await _dataService.GetItemsAsync();
    }

    private bool CanSave() => !HasErrors;

    private async Task SaveAsync()
    {
        await _dataService.SaveAsync(CurrentItem);
        Messenger.Send(new RefreshViewMessage("Engagements"));
        ToastService.ShowSuccess("Item saved successfully!");
    }

    public IAsyncRelayCommand SaveCommand { get; }
}
```

### Showing Toast Notifications

```csharp
using GRC.Shared.UI.Services;

// Success notification
ToastService.ShowSuccess("Operation completed successfully!");

// Warning with custom duration
ToastService.ShowWarning("Please review the changes", TimeSpan.FromSeconds(5));

// Error with formatting
ToastService.ShowError("Failed to save {0}: {1}", itemName, errorMessage);
```

### Creating Modal Dialogs

Modal dialogs are created through `IModalDialogService` using `ModalDialogOptions`.

```csharp
using GRC.Shared.UI.Dialogs;

var dialogService = new ModalDialogService();
var session = dialogService.Create(
    owner: mainWindow,
    view: confirmationControl,
    title: "Confirm Action",
    options: new ModalDialogOptions
    {
        ContentSizeRatio = 0.4,
        DimBackground = true,
        Layout = ModalDialogLayout.CenteredOverlay
    });

await session.ShowAsync();
```

**Available options:**
- `Layout` (`ModalDialogLayout`): `CenteredOverlay` or `OwnerAligned`.
- `SizeRatioResourceKey`: Resource key for a tokenized dialog size ratio.
- `ContentSizeRatio`: Numeric fallback when `SizeRatioResourceKey` is null.
- `Title`: Overrides the dialog window title when the call does not pass a title.
- `ShowWindowControls`: Shows or hides system window chrome (close button/title bar).
- `DimBackground`: Enables or disables the dimmed glass overlay.
- `ContainerMargin`: Optional padding around dialog content.

**Example:**
Create options for a compact dialog:
- `SizeRatioResourceKey = "DialogContentRatioCompact"`
- `ShowWindowControls = false`
- `DimBackground = true`

The dialog service enforces fail-fast resource lookups for size tokens and brushes, so missing keys will throw.

### Using the File Picker Service

```csharp
using GRC.Shared.UI.Services;

var filePickerService = new FilePickerService(mainWindow);

// Open file
var filePath = await filePickerService.OpenFileAsync(
    title: "Select Excel File",
    defaultExtension: ".xlsx",
    allowedPatterns: new[] { "*.xlsx", "*.xls" });

if (filePath != null)
{
    await ProcessFileAsync(filePath);
}

// Save file
var savePath = await filePickerService.SaveFileAsync(
    defaultFileName: "export.txt",
    title: "Export Data");
```

### Exporting Data

```csharp
using GRC.Shared.Core.Services;
using GRC.Shared.UI.Services;

var exportService = new TabDelimitedExportService();
await exportService.ExportAsync(
    filePath: outputPath,
    headers: new[] { "ID", "Name", "Value", "Status" },
    rows: items.Select(item => new[] 
    { 
        item.Id.ToString(), 
        item.Name, 
        item.Value.ToString("N2"), 
        item.Status 
    }));

ToastService.ShowSuccess("Data exported successfully!");
```

### Currency Formatting

```xaml
<!-- In your XAML resources -->
<MultiBinding Converter="{StaticResource CurrencyDisplayConverter}">
    <Binding Path="Amount" />
    <Binding Path="CurrencyCode" />
</MultiBinding>
```

```csharp
// In code
using GRC.Shared.UI.Services;

var formatted = CurrencyDisplayHelper.Format(12345.67m, "USD");
// Output: $12,345.67
```

## ?? Custom Controls

### TrafficLight
Visual status indicator with automatic color coding based on numeric thresholds or direct status values.

**Modes:**
- **Numeric**: Automatically calculates color from value and boundaries
- **String**: Uses direct status strings (Green/Yellow/Red)

**Key Properties:**
- `Value`, `GreenUpperBound`, `YellowUpperBound`
- `GreenBrush`, `YellowBrush`, `RedBrush`
- `Symbol`, `Size`

### SearchBox
Search input control with integrated clear button and placeholder support.

### RadialGauge
Circular gauge control for displaying metrics and KPIs.

### LoadingIndicator
Animated loading spinner for async operations.

### StatusBar
Application status bar with customizable content.

### EmptyState
Placeholder control for empty data states.

### SidebarHost
Container control for sidebar navigation.

## ??? Architecture

### Design Patterns
- **MVVM**: Clean separation with ViewModel base classes
- **Messenger Pattern**: Decoupled component communication
- **Repository Pattern**: Service interfaces for data access
- **Dependency Injection**: Interface-based services

### Base Classes

#### ObservableViewModelBase
```csharp
public abstract class ObservableViewModelBase : ObservableObject
{
    protected IMessenger Messenger { get; }
    public virtual Task LoadDataAsync();
    protected static void NotifyCommandCanExecute(IRelayCommand? command);
}
```

#### ValidatableViewModelBase
```csharp
public abstract class ValidatableViewModelBase : ObservableValidator, IRecipient<RefreshViewMessage>
{
    protected IMessenger Messenger { get; }
    public virtual Task LoadDataAsync();
    public virtual void Receive(RefreshViewMessage message);
}
```

## ?? Localization

Add support for additional languages:

1. Create new resource file: `Strings.[culture].resx`
2. Add translations for all keys
3. Update project file if needed

Example:
```xml
<EmbeddedResource Include="Localization\Strings.es-ES.resx">
    <DependentUpon>Strings.resx</DependentUpon>
</EmbeddedResource>
```

## ?? Theming

Required theme resources:

```xaml
<Application.Resources>
    <SolidColorBrush x:Key="BrushOverlay" Color="#80000000" />
    <SolidColorBrush x:Key="BrushSurface" Color="#FFFFFF" />
    <SolidColorBrush x:Key="BrushBorder" Color="#E0E0E0" />
</Application.Resources>
```

## ?? Documentation

For comprehensive documentation, see [SOLUTION_DOCUMENTATION.md](SOLUTION_DOCUMENTATION.md) which includes:
- Detailed architecture overview
- Complete API reference
- All domain models and relationships
- Advanced usage patterns
- Extensibility guidelines

## ?? Development

### Building the Solution

```bash
dotnet build GRC.Shared.sln
```

### Running Tests

```bash
dotnet test
```

### Code Style
- Follow C# naming conventions
- Use nullable reference types
- Prefer async/await for I/O operations
- Document public APIs with XML comments

## ?? Contributing

Contributions are welcome! Please follow these guidelines:

1. **Fork** the repository
2. **Create** a feature branch (`git checkout -b feature/amazing-feature`)
3. **Commit** your changes (`git commit -m 'Add amazing feature'`)
4. **Push** to the branch (`git push origin feature/amazing-feature`)
5. **Open** a Pull Request

### Coding Standards
- Follow existing code style and patterns
- Add XML documentation for public APIs
- Include unit tests for new features
- Update documentation as needed

## ?? Domain Models

### Key Entities

**Engagement** - Client engagement with financial tracking
- Properties: EngagementId, Description, Currency, Margins, Status
- Relationships: Customer, Managers, Budgets, Allocations

**Customer** - Client organization information

**Employee** - Employee records and details

**Manager** - Manager information and assignments

**Financial Models** - Revenue tracking, fiscal years, actuals

**Allocation Models** - Hour tracking and resource allocation

For complete model documentation, see [SOLUTION_DOCUMENTATION.md](SOLUTION_DOCUMENTATION.md#domain-model-relationships).

## ??? Services

### UI Services
- **ModalDialogService** - Create themed modal dialogs
- **ToastService** - Show toast notifications
- **FilePickerService** - Open/save file dialogs
- **TabDelimitedExportService** - Export data to text files
- **CurrencyDisplayHelper** - Format currency values

### Service Interfaces
All services implement interfaces for easy testing and dependency injection:
- `IModalDialogService`
- `ITabDelimitedExportService`

## ?? Best Practices

### Thread Safety
```csharp
// Always dispatch UI updates
if (Dispatcher.UIThread.CheckAccess())
{
    command.NotifyCanExecuteChanged();
}
else
{
    Dispatcher.UIThread.Post(command.NotifyCanExecuteChanged);
}
```

### Message Handling
```csharp
// Register for specific messages
public class MyViewModel : ValidatableViewModelBase, IRecipient<RefreshViewMessage>
{
    public MyViewModel(IMessenger messenger) : base(messenger)
    {
        Messenger.RegisterAll(this);
    }

    public void Receive(RefreshViewMessage message)
    {
        if (message.Matches("MyTarget"))
        {
            _ = LoadDataAsync();
        }
    }
}
```

### Validation
```csharp
// Use data annotations
public class MyModel
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Range(0, 100)]
    public decimal Percentage { get; set; }
}

// Check before save
if (HasErrors)
{
    ToastService.ShowError("Please correct validation errors");
    return;
}
```

## ?? Project Statistics

- **3** Projects
- **30+** Domain Models
- **12+** UI Controls
- **11+** Value Converters
- **8+** Services
- **2** Supported Languages

## ?? Troubleshooting

### Common Issues

**Issue**: Controls not rendering correctly
- **Solution**: Ensure theme resources are properly loaded and required brushes are defined

**Issue**: Messenger not delivering messages
- **Solution**: Verify `Messenger.RegisterAll(this)` is called and implement `IRecipient<T>`

**Issue**: Toast notifications not showing
- **Solution**: Bind the `ToastService.Notifications` collection to your UI overlay

**Issue**: File picker returns null on Linux/macOS
- **Solution**: Temp file fallback is automatic; check file permissions

## ?? License

This project is licensed under the MIT License - see the LICENSE file for details.

## ?? Links

- **Documentation**: [SOLUTION_DOCUMENTATION.md](SOLUTION_DOCUMENTATION.md)
- **Repository**: https://github.com/cjordaoc/GRC.Shared
- **Avalonia UI**: https://avaloniaui.net/
- **CommunityToolkit.Mvvm**: https://github.com/CommunityToolkit/dotnet

## ?? Authors

- **Caio Calisto** - Initial work

## ?? Acknowledgments

- Built with [Avalonia UI](https://avaloniaui.net/)
- Powered by [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet)
- Inspired by modern MVVM best practices

---

Made with ?? for GRC applications
