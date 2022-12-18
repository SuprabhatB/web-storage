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
builder.Services.AddClientStorage(); //By default the service is registered as a _LocalStorage_
//builder.Services.AddClientStorage(option => option.StorageType = WebStorageType.LocalStorage); //Service is registered as a LocalStorage
//builder.Services.AddClientStorage(option => option.StorageType = WebStorageType.SessionStorage); //Service is registered as a SessionStorage
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

In the razor component use the below to inject storage service. 

```c#
@using WebStorage

@inject IClientStorage clientStorage
```

1. Setting key and value to the storage

```c#
await clientStorage.WriteAsync("Name", "John Doe");
```

2. Read value from the storage by key

```c#
var keyValue = await clientStorage.ReadAsync("Name");
```

3. Remove key and value from the storage

```c#
await clientStorage.RemoveAsync("Name");
```
### Storage Demo

| Local Storage  | Session Storage |
| ------------- | ------------- |
| ![LocalStorageSingle](https://i.stack.imgur.com/jJBlt.gif)  | ![SessionStorageSingle](https://i.stack.imgur.com/OmsY6.gif)  |

### Avoid excessively fine-grained calls

Since each call involves some overhead, it can be valuable to reduce the number of calls. rolling individual JS interop calls into a single call usually only improves performance significantly if the component makes a large number of JS interop calls ([see docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/performance?view=aspnetcore-6.0#avoid-excessively-fine-grained-calls-1)).

4. Setting multiple key and value to the storage

```c#
var payload = new Dictionary<string, object> {
    { "Name", "John Doe" },
    { "Age", 35 },
    { "IsMarried", true },
    { "DateTime", DateTime.UtcNow }
}; 

await clientStorage.WriteAsync(payload);
```

5. Read multiple key and value to the storage

```c#
var payload = new Dictionary<string, object> {
    { "Name", default },
    { "Age", default },
    { "IsMarried", default },
    { "DateTime", default }
};

var keyValues = await clientStorage.ReadAsync(payload);
```

6. Remove multiple key and value from the storage

```c#
var keys = new[] { "Name", "Age", "IsMarried", "DateTime" };
//var keys = new List<string> { "Name", "Age", "IsMarried", "DateTime" };

await clientStorage.RemoveAsync(keys);
```

7. Remove all key and value from the storage

```c#
await clientStorage.RemoveAll();
```
### Storage Demo

| Local Storage  | Session Storage |
| ------------- | ------------- |
| ![LocalStorageMultiple](https://i.stack.imgur.com/NxEBC.gif)  | ![SessionStorageMultiple](https://i.stack.imgur.com/54ERk.gif)  |