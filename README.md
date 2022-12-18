# Web Storage
This library provides access to the browser's local and session storage using javascript APIs for the Blazor applications.

## Installing

1. To directly install the package, add the below snippet to the application `.csproj` file 

```
<PackageReference Include="Web.Storage.Core" Version="x.x.x" />
```

2. Install via the .NET CLI

```
dotnet add package Web.Storage.Core
```

3. Install via the built-in NuGet package manager. Click on the project dependencies and search for `Web.Storage.Core`.

## Setup

Register the storage services with the service collection in the application `Program.cs` or `Startup.cs` (if any). 

```c#
builder.Services.AddClientStorage(); //By default the service is registered as a LocalStorage
builder.Services.AddClientStorage(option => option.StorageType = WebStorageType.LocalStorage); // Manually choose from Local or Session Storage
```
If using legacy way to configure services.

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddClientStorage();
    //services.AddClientStorage(option => option.StorageType = WebStorageType.LocalStorage);
}
``` 

## Usage (Blazor Server)




